using E_Commerce.Application.Contracts;
using E_Commerce.Domain.Contarct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services
{
    public class CacheServices : ICacheServices
    {
        private readonly ICacheRepository _cacheRepository;
        public CacheServices(ICacheRepository cacheRepository)
        {
            _cacheRepository = cacheRepository;
        }
        public async Task<string?> GetAsync(string cacheValue, CancellationToken cancellationToken)
        {
            return await _cacheRepository.GetAsync(cacheValue,cancellationToken);
        }

        public async Task SetAsync(string cacheKey, object cacheValue, TimeSpan timeToLive, CancellationToken cancellationToken)
        {
            var json = JsonSerializer.Serialize(cacheValue, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            await _cacheRepository.SetAsync(cacheKey, json, timeToLive, cancellationToken);
        }
    }
}
