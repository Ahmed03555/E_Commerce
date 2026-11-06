using AutoMapper;
using E_Commerce.Application.DTOs.Products;
using E_Commerce.Domain.Entity.Products;
using Microsoft.Extensions.Options;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Profiles
{
    internal class PictureUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly UrlSettings _options;

        public PictureUrlResolver(IOptions<UrlSettings> options)
        {
            _options=options.Value;
        }
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            var baseUrl = _options.BaseUrl.TrimEnd('/');
            var path = source.PictureUrl.TrimStart('/');
            return $"{baseUrl}/Files/{path}";
        }
    }
    public class UrlSettings
    {
        public string BaseUrl { get; set; }
    }
}
