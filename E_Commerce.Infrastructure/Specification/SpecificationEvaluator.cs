using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contarct;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Specification
{
    internal static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity,TKey>(IQueryable<TEntity> InputQuery ,ISpecification<TEntity,TKey> Specs) where TEntity : BaseEntity<TKey>
        {
            var query = InputQuery;
            if(Specs.Criteria is not  null)
            {
                query = query.Where(Specs.Criteria);
            }
            if (Specs.IncludeExperssions.Any())
            {
                //foreach(var exprssion in Specs.IncludeExperssions)
                //{
                //    query = query.Include(exprssion);
                //}
                query = Specs.IncludeExperssions.Aggregate(query, (current, nextExp) => current.Include(nextExp));
            }
            if(Specs.OrderBy is not null)
            {
                query = query.OrderBy(Specs.OrderBy);
            }else if(Specs.OrderByDescending is not null) {
                
                query = query.OrderByDescending(Specs.OrderByDescending);

            }

            if (Specs.IsPaginated)
            {
                query = query.Skip(Specs.Skip).Take(Specs.Take);

            }
            return query;
        }
    }
}
