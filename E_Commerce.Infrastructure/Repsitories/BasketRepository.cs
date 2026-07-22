using E_Commerce.Domain.Contarct;
using E_Commerce.Domain.Entity.Baskets;
using E_Commerce.Infrastructure.Date;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Repsitories
{
    internal class BasketRepository : IBasketRepository
    {
        
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer multiplexer ) {

            _database = multiplexer.GetDatabase();
        }
        public async Task<CustomerBasket?> CreateOrUpdateBasket(CustomerBasket basket, TimeSpan? timeSpan = null, CancellationToken cancellationToken = default)
        {
            var value = JsonSerializer.Serialize(basket);
            var result =  await _database.StringSetAsync(basket.Id, value, timeSpan ?? TimeSpan.FromDays(7));
            return result ? basket : null;
        }

        public async Task<bool> DeleteBasket(string Id, CancellationToken cancellationToken = default)
        {
            return await _database.KeyDeleteAsync(Id);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string Id, CancellationToken cancellationToken = default)
        {
            var basket = await _database.StringGetAsync(Id);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket!);
        }
    }
}
