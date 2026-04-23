using AutoMapper;
using GoodHamburger.Api.Endpoints.Orders.CreateOrder;
using GoodHamburger.Api.Endpoints.Orders.PreviewOrder;
using GoodHamburger.Api.Endpoints.Orders.UpdateOrder;
using GoodHamburger.Application.Operations.Orders.CreateOrder;
using GoodHamburger.Application.Operations.Orders.PreviewOrder;
using GoodHamburger.Application.Operations.Orders.UpdateOrder;

namespace GoodHamburger.Api.Mappings
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<CreateOrderRequest, CreateOrderCommand>().ReverseMap();
            CreateMap<PreviewOrderRequest, PreviewOrderCommand>().ReverseMap();
            CreateMap<UpdateOrderRequest, UpdateOrderCommand>().ReverseMap();
        }
    }
}
