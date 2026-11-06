using E_Commerce.Application.Common;
using E_Commerce.Application.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Contracts
{
    public interface IProductServices
    {
        Task<Result<PaginatedResult<ProductDto>>> GetAllProductAsync(ProductQueryParam queryParam, CancellationToken cancellationToken = default);
        Task<Result<IReadOnlyList<BrandDto>>> GetAllBrandAsync(CancellationToken cancellationToken = default);
        Task<Result<IReadOnlyList<TypeDto>>> GetAllTypeAsync(CancellationToken cancellationToken = default);

        Task<Result<ProductDto>> GetProductById(int id,CancellationToken cancellationToken=default);
    }
}
