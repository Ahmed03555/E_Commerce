using AutoMapper;
using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Baskets;
using E_Commerce.Domain.Contarct;
using E_Commerce.Domain.Entity.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services
{
    public class BasketServices : IBasketServices
    {
        private readonly IBasketRepository basketRepository;
        private readonly IMapper mapper;
        public BasketServices(IBasketRepository _basketRepository ,IMapper _mapper)
        {
            basketRepository = _basketRepository;
            mapper = _mapper;
        }
        public async Task<Result<BasketDto?>> CreateOrUpdateBasket(BasketDto basket, TimeSpan? timeSpan = null, CancellationToken cancellationToken = default)
        {
            var customerbasket =  mapper.Map<CustomerBasket>(basket);
            var result = await basketRepository.CreateOrUpdateBasket(customerbasket, timeSpan, cancellationToken);
            return result == null ? Result<BasketDto?>.Fail(Error.Failure("CreateOrUpdateBasket.isfailuer", "Cannot create or update")) :
                Result<BasketDto?>.Ok(basket);
        }

        public async Task<Result<bool>> DeleteBasketAsync(string BasketId, CancellationToken cancellationToken = default)
        {
           var Basket = await basketRepository.DeleteBasket(BasketId, cancellationToken);
            return Basket ? Result<bool>.Ok(Basket) : Result<bool>.Fail(Error.Failure("DeleteBasketAsync.IS FAILUER","Cannot Delete Basket"));
        }

        public async Task<Result<BasketDto?>> GetBasketAsync(string BasketId, CancellationToken cancellationToken = default)
        {
           var basket = await basketRepository.GetBasketAsync(BasketId, cancellationToken);
           var result = mapper.Map<BasketDto>(basket);
            return result == null ? Result<BasketDto?>.Fail(Error.NotFound("Basket is not found", $"basket with Id:{BasketId}")) : Result<BasketDto?>.Ok(result);
        }
    }
}
