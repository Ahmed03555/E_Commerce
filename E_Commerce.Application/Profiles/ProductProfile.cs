using AutoMapper;
using E_Commerce.Application.DTOs.Products;
using E_Commerce.Domain.Entity.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Profiles
{
    internal class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductBrand , BrandDto>();
            CreateMap<ProductType , TypeDto>();
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.ProductBrand, opti => opti.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.ProductType, opti => opti.MapFrom(src => src.ProductType.Name))
                .ForMember(dest => dest.PictureUrl, optio => optio.MapFrom<PictureUrlResolver>());
                
        }
    }
}
