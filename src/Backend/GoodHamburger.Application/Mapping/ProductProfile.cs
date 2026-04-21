using AutoMapper;
using GoodHamburger.Application.Dtos;
using GoodHamburger.Application.Shared;
using GoodHamburger.Domain.Entity;

namespace GoodHamburger.Application.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(x => x.ImageBase64,
                   opt => opt.MapFrom(src =>
                       src.ProductImage != null && src.ProductImage.ImageBytes != null
                           ? Converts.ByteArrayToBase64(src.ProductImage.ImageBytes)
                           : null));
        }
    }
}
