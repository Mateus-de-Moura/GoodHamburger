using AutoMapper;
using GoodHamburger.Api.Endpoints.Products.CreateProducts;
using GoodHamburger.Api.Endpoints.Products.UpdateProduct;
using GoodHamburger.Application.Operations.Products.CreateProducts;
using GoodHamburger.Application.Operations.Products.UpdateProducts;

namespace GoodHamburger.Api.Mappings
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<CreateProductsRequest, CreateProductsCommand>().ReverseMap();
            CreateMap<UpdateProductRequest, UpdateProductCommand>().ReverseMap();
        }
    }
}
