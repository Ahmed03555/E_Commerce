using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contarct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Specifications
{
    internal abstract class BaseSpecifications<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public ICollection<Expression<Func<TEntity, object>>> IncludeExperssions { get; } = [];

        public Expression<Func<TEntity, bool>> Criteria { get; private set; }

        public Expression<Func<TEntity, object>>? OrderBy { get; private set; }

        public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }

        public int Take { get; private set; }

        public int Skip  {get; private set; }

        public bool IsPaginated { get; private set; }

        protected BaseSpecifications(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }
        //protected void AddCondition(Expression<Func<TEntity, bool>> condition)
        //{
        //    Criteria = condition;  ممكن دي عادي
        //}

        protected void AddInclude(Expression<Func<TEntity, object>> include)
        {
            IncludeExperssions.Add(include);
        }
        protected void ApplyPagination(int pageIndex,int pageSize)
        {
            IsPaginated = true;
            Take= pageIndex;
            Skip = (pageIndex -1) * pageSize;
        }

        protected void AddOrder(Expression<Func<TEntity, object>>? OrderByExp)
        {
            OrderBy = OrderByExp;
        }
        protected void AddOrderByDescending(Expression<Func<TEntity, object>>? OrderByDescendingExp)
        {
            OrderBy = OrderByDescendingExp;
        }
    }
}
