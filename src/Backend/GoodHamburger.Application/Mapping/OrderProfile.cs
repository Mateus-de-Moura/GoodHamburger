using AutoMapper;
using GoodHamburger.Application.Dtos;
using GoodHamburger.Domain.Entity;

namespace GoodHamburger.Application.Mapping
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<Order, OrderDto>();
        }
    }
}
