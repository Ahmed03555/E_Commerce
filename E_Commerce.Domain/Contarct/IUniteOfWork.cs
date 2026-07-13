using E_Commerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Domain.Contarct
{
    public interface IUniteOfWork
    {
        Task<int> SaveChangeAsync(CancellationToken ct = default);

        IGeneriRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;
    }
}
