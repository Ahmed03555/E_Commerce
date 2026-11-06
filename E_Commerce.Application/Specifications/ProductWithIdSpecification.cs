using E_Commerce.Domain.Entity.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Specifications
{
    internal class ProductWithIdSpecification : BaseSpecifications<Product, int>
    {
        public ProductWithIdSpecification(HashSet<int> ids) : base(p => ids.Contains(p.Id))
        {
        }
    }
}
