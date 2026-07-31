using AutoMapper;
using E_Commerce.Application.DTOs.Orders;
using E_Commerce.Domain.Entity.Orders;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Profiles
{
  
    
        public class OrderItemPictureUrlResolver(IOptions<UrlSettings> options) : IValueResolver<OrderItems, OrderItemsDto, string>
        {
            private readonly UrlSettings settings = options.Value;

            public string Resolve(OrderItems source, OrderItemsDto destination, string destMember, ResolutionContext context)
            {
                if (string.IsNullOrEmpty(source.productItemOrder.PictureUrl))
                    return string.Empty;

                return $"{settings.BaseUrl}/Files/{source.productItemOrder.PictureUrl}";
            }
        }
    
}
