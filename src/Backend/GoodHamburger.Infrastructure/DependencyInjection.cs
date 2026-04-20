using GoodHamburger.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GoodHamburger.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<GoodHamburgerDbContext>(options =>
                options.UseInMemoryDatabase("GoodHamburgerDb"));

            return services;
        }
    }
}
