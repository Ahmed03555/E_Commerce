using E_Commerce.Domain.Common;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Domain.Contarct
{
    public interface IGeneriRepository<TEntity ,TKey> where TEntity : BaseEntity<TKey>
    {
        public void Add(TEntity entity);
        public void Update(TEntity entity);
        public void Remove(TEntity entity);

        Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);
    }
}
