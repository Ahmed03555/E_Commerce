using E_Commerce.Application.Common;
using E_Commerce.Domain.Entity.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Specifications
{
    internal class ProductWithBrandandTypeSpec :BaseSpecifications<Product,int>
    {
        public ProductWithBrandandTypeSpec(ProductQueryParam queryParam) :
            base(p => (!queryParam.BrandId.HasValue || p.BrandId==queryParam.BrandId.Value) && (!queryParam.TypeId.HasValue||p.TypeId == queryParam.TypeId.Value)
            && (string.IsNullOrEmpty(queryParam.SearchValue) || p.Name.ToLower().Contains(queryParam.SearchValue.ToLower())))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            switch (queryParam.Sort)
            {
                case ProductSortOption.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;

                case ProductSortOption.NameAsc:
                    AddOrder(p => p.Name);
                    break;

                case ProductSortOption.PriceDesc:
                    AddOrderByDescending(P => P.Price);
                    break;

                case ProductSortOption.PriceAsc:
                    AddOrder(P => P.Price);
                    break;
                default:
                    AddOrder(p => p.Id); break;
            }
            ApplyPagination(queryParam.PageIndex, queryParam.PageSize);
        }
        
        public ProductWithBrandandTypeSpec(int id):base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
