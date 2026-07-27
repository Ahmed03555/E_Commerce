using E_Commerce.Domain.Contarct;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Repsitories
{
    internal class CacheRepository : ICacheRepository
    {
        private readonly IDatabase database;
        public CacheRepository(IConnectionMultiplexer connection)
        {
            database = connection.GetDatabase();
        }
        public async Task<string?> GetAsync(string cacheValue, CancellationToken cancellationToken)
        {
            var value = await database.StringGetAsync(cacheValue);
            return value.IsNullOrEmpty ? null : value.ToString();
        }

        public async Task SetAsync(string cacheKey, string cacheValue, TimeSpan timeToLive, CancellationToken cancellationToken)
        {
            await database.StringSetAsync(cacheKey, cacheValue, timeToLive);
        }
    }
}
