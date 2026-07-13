using AutoMapper;
using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Products;
using E_Commerce.Domain.Contarct;
using E_Commerce.Domain.Entity.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Services
{
    public class ProductService : IProductServices
    {
        private readonly IUniteOfWork uniteOfWork;
        private readonly IMapper mapper;
        public ProductService(IUniteOfWork uniteOfWork, IMapper mapper)
        {
            this.uniteOfWork = uniteOfWork;
            this.mapper = mapper;
        }
        public async Task<Result<IReadOnlyList<BrandDto>>> GetAllBrandAsync(CancellationToken cancellationToken = default)
        { 
            var brands = await uniteOfWork.GetRepository<ProductBrand,int>().GetAllAsync(cancellationToken);
            var date = mapper.Map<IReadOnlyList<BrandDto>>(brands);
            return Result<IReadOnlyList<BrandDto>>.Ok(date);
        }

        public async Task<Result<IReadOnlyList<ProductDto>>> GetAllProductAsync(CancellationToken cancellationToken = default)
        {
            var product = await uniteOfWork.GetRepository<Product,int>().GetAllAsync(cancellationToken);
            return Result<IReadOnlyList<ProductDto>>.Ok(mapper.Map<IReadOnlyList<ProductDto>>(product));
        }

        public async Task<Result<IReadOnlyList<TypeDto>>> GetAllTypeAsync(CancellationToken cancellationToken = default)
        {
            var type =await uniteOfWork.GetRepository<ProductType,int>().GetAllAsync(cancellationToken);
            return Result<IReadOnlyList<TypeDto>>.Ok(mapper.Map<IReadOnlyList<TypeDto>>(type));
        }

        public async Task<Result<ProductDto>> GetProductById(int id, CancellationToken cancellationToken = default)
        {
            var product = await uniteOfWork.GetRepository<Product,int>().GetByIdAsync(id, cancellationToken);
            if(product is null)
            
                return Result<ProductDto>.Fail(Error.NotFound("Product Not Found", $"Product with: {id} is notFound"));
            return Result<ProductDto>.Ok(mapper.Map<ProductDto>(product));
            
        }
    }
}
