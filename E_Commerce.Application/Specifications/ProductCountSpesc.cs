using E_Commerce.Application.Common;
using E_Commerce.Domain.Entity.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Specifications
{
    internal class ProductCountSpesc : BaseSpecifications<Product,int>
    {
        public ProductCountSpesc(ProductQueryParam queryParam):base(p => (!queryParam.BrandId.HasValue || p.BrandId==queryParam.BrandId.Value) && (!queryParam.TypeId.HasValue||p.TypeId == queryParam.TypeId.Value)
            && (string.IsNullOrEmpty(queryParam.SearchValue) || p.Name.ToLower().Contains(queryParam.SearchValue.ToLower())))
        {
            
        }
    }
}
