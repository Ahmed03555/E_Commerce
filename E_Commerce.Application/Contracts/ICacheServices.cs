using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Contracts
{
    public interface ICacheServices
    {
        Task<string?> GetAsync(string cacheValue, CancellationToken cancellationToken = default);
        Task SetAsync(string cacheKey, object cacheValue, TimeSpan timeToLive, CancellationToken cancellationToken = default);
    }
}
