using E_Commerce.Application.Common;
using E_Commerce.Application.DTOs.Baskets;
using E_Commerce.Domain.Entity.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Contracts
{
    public interface IBasketServices
    {
        Task<Result<BasketDto?>> GetBasketAsync(string BasketId, CancellationToken cancellationToken = default);
        Task<Result<BasketDto?>> CreateOrUpdateBasket(BasketDto basket, TimeSpan? timeSpan = null, CancellationToken cancellationToken = default);

        Task<Result<bool>> DeleteBasketAsync(string BasketId, CancellationToken cancellationToken = default);
    }
}
