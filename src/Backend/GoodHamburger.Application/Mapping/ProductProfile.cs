using AutoMapper;
using GoodHamburger.Application.Dtos;
using GoodHamburger.Application.Operations.Products.CreateProducts;
using GoodHamburger.Application.Operations.Products.UpdateProducts;
using GoodHamburger.Application.Shared;
using GoodHamburger.Domain.Entity;

namespace GoodHamburger.Application.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductsCommand, Product>()
           .ForMember(dest => dest.ProductImage, opt => opt.MapFrom(src =>
               src.Image == null
                   ? null
                   : new ProductImage
                   {
                       FileName = src.Image.FileName,
                       ContentType = src.Image.ContentType
                   }));

            CreateMap<UpdateProductCommand, Product>()
                .ForMember(dest => dest.ProductImage, opt => opt.MapFrom(src =>
                    src.Image == null
                        ? null
                        : new ProductImage
                        {
                            FileName = src.Image.FileName,
                            ContentType = src.Image.ContentType
                        }));

            CreateMap<Product, ProductDto>()
                .ForMember(x => x.CategoryDescription,
                    opt => opt.MapFrom(src => src.Category != null ? src.Category.Description : string.Empty))
                .ForMember(x => x.ImageBase64,
                   opt => opt.MapFrom(src =>
                       src.ProductImage != null && src.ProductImage.ImageBytes != null
                           ? Converts.ByteArrayToBase64(src.ProductImage.ImageBytes)
                           : null));
        }
    }
}
