using AutoMapper;
using GoodHamburger.Application.Dtos;
using GoodHamburger.Domain.Entity;

namespace GoodHamburger.Application.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}
