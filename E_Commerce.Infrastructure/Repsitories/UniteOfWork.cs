using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contarct;
using E_Commerce.Infrastructure.Date;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure.Repsitories
{
    internal class UniteOfWork : IUniteOfWork
    {
        private readonly StoreDbContext _storeContext;
        private readonly Dictionary<string, object> repositories = [];

        public UniteOfWork(StoreDbContext _storeContext)
        {
            this._storeContext = _storeContext;
        }
        public IGeneriRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var typeName = typeof(TEntity).Name;

            if(repositories.TryGetValue(typeName, out var repository))

                return (IGeneriRepository<TEntity, TKey>) repository;

            var repo = new GenericRepository<TEntity, TKey>(_storeContext);

            repositories[typeName] = repo;

            return repo;
        }

        public async Task<int> SaveChangeAsync(CancellationToken ct = default)
        {
            return await _storeContext.SaveChangesAsync(ct);
        }
    }
}
