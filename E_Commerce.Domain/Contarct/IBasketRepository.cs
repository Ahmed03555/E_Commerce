using E_Commerce.Domain.Entity.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contarct
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string Id, CancellationToken cancellationToken = default);
        Task<CustomerBasket?> CreateOrUpdateBasket(CustomerBasket basket,TimeSpan? timeSpan =default,CancellationToken cancellationToken =default);

        Task<bool> DeleteBasket(string Id, CancellationToken cancellationToken = default);
    }
}
