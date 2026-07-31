using AutoMapper;
using E_Commerce.Application.DTOs.Authentication;
using E_Commerce.Application.DTOs.Orders;
using E_Commerce.Domain.Entity.Orders;

namespace E_Commerce.Application.Profiles
{
    internal class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<DeliveryMethod, DeliveryMethodDto>();

            CreateMap<OrderAddress, AddressDto>().ReverseMap();

            CreateMap<Order, OrderToResultDto>()
                .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod.ShortName))
                .ForMember(dest => dest.DeliveryCost, opt => opt.MapFrom(src => src.DeliveryMethod.Price))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.GetTOtal()));


            CreateMap<OrderItems, OrderItemsDto>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.productItemOrder.ProductId))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.productItemOrder.ProductName))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<OrderItemPictureUrlResolver>());
        }
    }
}