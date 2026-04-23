using GoodHamburger.Api.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace GoodHamburger.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            services.AddAutoMapper(_ => { }, typeof(ProductMappingProfile).Assembly);

            return services;
        }
    }
}