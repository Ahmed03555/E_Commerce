using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contarct;
using E_Commerce.Infrastructure.Date;
using E_Commerce.Infrastructure.Specification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure.Repsitories
{
    internal class GenericRepository<TEntity, TKey> : IGeneriRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext dbContext;
        public GenericRepository(StoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(TEntity entity)
        {
            dbContext.Set<TEntity>().Add(entity);
        }

        public async Task<int> CountAsync(ISpecification<TEntity, TKey> specs, CancellationToken cancellationToken)
        {
            var query = SpecificationEvaluator.CreateQuery(dbContext.Set<TEntity>(), specs);
            return await query.CountAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.Set<TEntity>().ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecification<TEntity, TKey> specs, CancellationToken cancellationToken = default)
        {
            var query = SpecificationEvaluator.CreateQuery(dbContext.Set<TEntity>(),specs);
            return await query.ToListAsync(cancellationToken);
        }

        public async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
        {
            return await dbContext.Set<TEntity>().FindAsync(id, cancellationToken);
        }

        public async Task<TEntity?> GetByIdAsync(ISpecification<TEntity, TKey> specs, CancellationToken cancellationToken = default)
        {
            var query = SpecificationEvaluator.CreateQuery(dbContext.Set<TEntity>(), specs);
            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public void Remove(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);
        }

        public void Update(TEntity entity)
        {
            dbContext.Set<TEntity>().Update(entity);
        }
    }
}
