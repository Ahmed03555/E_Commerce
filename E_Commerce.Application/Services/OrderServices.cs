using AutoMapper;
using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Orders;
using E_Commerce.Application.Specifications;
using E_Commerce.Domain.Contarct;
using E_Commerce.Domain.Entity.Orders;
using E_Commerce.Domain.Entity.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IMapper _mapper;
        private readonly IUniteOfWork _uniteOf;
        private readonly IBasketRepository _basketRepository;

        public OrderServices(IMapper mapper,IUniteOfWork uniteOf,IBasketRepository basketRepository)
        {
            _mapper=mapper;
            _uniteOf=uniteOf;
            _basketRepository=basketRepository;
        }
        public async Task<Result<OrderToResultDto>> CreateOrderAsync(OrderDto orderDto,string email, CancellationToken cancellationToken = default)
        {
            var basket = await _basketRepository.GetBasketAsync(orderDto.BasketId, cancellationToken);

            if (basket is null)
                return Result<OrderToResultDto>.Fail(Error.NotFound("Basket.NotFound", $"Basket With Id {orderDto.BasketId} Not Found"));

            if (basket.Items.Count == 0)
                return Result<OrderToResultDto>.Fail(Error.Validation("Basket.Empty", $"Cannot Create Order With Empty Basket"));

            var productRepo = _uniteOf.GetRepository<Product, int>();

            var productIds = basket.Items.Select(BI => BI.Id).ToHashSet();
            var products = (await productRepo.GetAllAsync(new ProductWithIdSpecification(productIds), cancellationToken)).ToDictionary(P => P.Id);

            var orderItems = new List<OrderItems>(basket.Items.Count);
            foreach (var item in basket.Items)
            {
                if (!products.TryGetValue(item.Id, out var product))
                    return Result<OrderToResultDto>.Fail(Error.NotFound("Product.NotFound", $"Product With Id {item.Id} Not Found"));

                orderItems.Add(new OrderItems()
                {
                    Price = product.Price,
                    Quantity = item.Quantity,
                   productItemOrder  = new ProductItemOrder()
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        PictureUrl = product.PictureUrl
                    }
                });
            }
            var orderAddress = _mapper.Map<OrderAddress>(orderDto.ShipToAddress);

            var deliveryMethodRepo = _uniteOf.GetRepository<DeliveryMethod, int>();
            var deliveryMethod = await deliveryMethodRepo.GetByIdAsync(orderDto.DeliveryMethodId,cancellationToken);
            if (deliveryMethod is null)
                return Result<OrderToResultDto>.Fail(Error.NotFound("DeliveryMethod.NotFound", $"Delivery Method With Id {orderDto.DeliveryMethodId} Not Found"));

            var subTotal = orderItems.Sum(OI => OI.Price * OI.Quantity);

            var order = new Order(email, orderItems,  deliveryMethod, orderAddress, subTotal);

            var orderRepo = _uniteOf.GetRepository<Order, Guid>();
            orderRepo.Add(order);

            var result = await _uniteOf.SaveChangeAsync(cancellationToken);

            if (result < 1)
                return Result<OrderToResultDto>.Fail(Error.Failure("Order.Failure", "Cannot Create Order"));

            await _basketRepository.DeleteBasket(orderDto.BasketId, cancellationToken);

            return Result<OrderToResultDto>.Ok(_mapper.Map<OrderToResultDto>(order));
        }
    }
}
