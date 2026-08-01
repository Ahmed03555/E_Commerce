using AutoMapper;
using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Baskets;
using E_Commerce.Application.Specifications;
using E_Commerce.Domain.Contarct;
using E_Commerce.Domain.Entity.Orders;
using E_Commerce.Domain.Entity.Products;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services
{
    internal class PaymentServices : IPaymentServices
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;
        private readonly IOptions<PaymentGatewaySettings> paymentGatewayOptions;
        private readonly PaymentGatewaySettings _paymentGatewayOptions;
        private readonly IUniteOfWork _uniteOfWork;
        private readonly IPaymentGateway _paymentGateway;

        public PaymentServices(IBasketRepository basketRepository,IMapper mapper,IOptions<PaymentGatewaySettings> paymentGatewayOptions,IUniteOfWork uniteOfWork,IPaymentGateway paymentGateway)
        {
            _basketRepository=basketRepository;
            _mapper=mapper;
            this.paymentGatewayOptions=paymentGatewayOptions;
            _paymentGatewayOptions=paymentGatewayOptions.Value;
            _uniteOfWork=uniteOfWork;
            _paymentGateway=paymentGateway;
        }
        public async Task<Result<BasketDto>> CreateOrUpdatePaymentIntentAsync(string basketId, CancellationToken cancellationToken = default)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId, cancellationToken);

            if (basket == null)
                return Result<BasketDto>.Fail(Error.NotFound("Basket.NotFound", $"Basket With Id: {basketId} Is Not Found"));

            if (basket.Items.Count == 0)
                return Result<BasketDto>.Fail(Error.Validation("Basket.Empty", $"Cannot Create Order With Empty Basket"));

            var productRepo = _uniteOfWork.GetRepository<Product, int>();

            var productIds = basket.Items.Select(BI => BI.Id).ToHashSet();
            var products = (await productRepo.GetAllAsync(new ProductWithIdSpecification(productIds), cancellationToken)).ToDictionary(P => P.Id);
            foreach (var item in basket.Items)
            {
                if (!products.TryGetValue(item.Id, out var product))
                    return Result<BasketDto>.Fail(Error.NotFound("Product.NotFound", $"Product With Id {item.Id} Not Found"));
                item.Price = product.Price;
            }

            var deliveryMethodRepo = _uniteOfWork.GetRepository<DeliveryMethod, int>();
            var deliveryMethod = await deliveryMethodRepo.GetByIdAsync(basket.DeliveryMethodId.Value, cancellationToken);
            if (deliveryMethod is null)
                return Result<BasketDto>.Fail(Error.NotFound("DeliveryMethod.NotFound", $"Delivery Method With Id {basket.DeliveryMethodId.Value} Is Not Found"));

            basket.ShippingPrice = deliveryMethod.Price;
            var subTotal = basket.Items.Sum(I => I.Quantity * I.Price);
            var amount = (long)((subTotal + deliveryMethod.Price) * 100m);
            if (!string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                await _paymentGateway.updatePaymentIntentAsync(basket.PaymentIntentId, amount, cancellationToken);
            }
            else
            {
                var result = await _paymentGateway.createPaymentIntentAsync(amount, _paymentGatewayOptions.DefaultCurrency, cancellationToken);
                basket.PaymentIntentId = result.PaymentIntentId;
                basket.ClientSecret = result.ClientSecret;
            }
            await _basketRepository.CreateOrUpdateBasket(basket, cancellationToken: cancellationToken);
            return Result<BasketDto>.Ok(_mapper.Map<BasketDto>(basket));
        }

        public async Task PaymentFailed(string PaymentIntentId)
        {
            var orderRepo = _uniteOfWork.GetRepository<Order, Guid>();

            var order = await orderRepo.GetByIdAsync(new PaymentIntentSpec(PaymentIntentId));

            if (order == null)
                return;

            order.MarkPaymentFailed();
            await _uniteOfWork.SaveChangeAsync();
        }

        public async Task PaymentSucceeded(string PaymentIntentId)
        {
            var orderRepo = _uniteOfWork.GetRepository<Order, Guid>();

            var order = await orderRepo.GetByIdAsync(new PaymentIntentSpec(PaymentIntentId));

            if (order == null)
                return;

            order.MarkPaymentFailed();
            await _uniteOfWork.SaveChangeAsync();
        }
    }
    public class PaymentGatewaySettings
    {
        public string SecretKey { get; set; } = default!;
        public string DefaultCurrency { get; set; } = "USD";
        public string WebhookSecret { get; set; } = default!;
    }
}
