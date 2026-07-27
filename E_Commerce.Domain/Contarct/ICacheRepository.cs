using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contarct
{
    public interface ICacheRepository
    {
        Task<string?> GetAsync(string cacheValue, CancellationToken cancellationToken);
        Task SetAsync(string cacheKey,string cacheValue,TimeSpan timeToLive, CancellationToken cancellationToken);
    }
}
