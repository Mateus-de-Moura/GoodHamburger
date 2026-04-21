using GoodHamburger.Domain.Interfaces;
using GoodHamburger.Infrastructure.Data.Context;
using GoodHamburger.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoodHamburger.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IProductRepository, ProductRepository>();

            AddDatabase(services, configuration);

            return services;
        }

        private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var provider = configuration["Database:Provider"];

            if (string.Equals(provider, "InMemory", StringComparison.OrdinalIgnoreCase))
            {
                var inMemoryDatabaseName = configuration["Database:InMemoryDatabaseName"];
                if (string.IsNullOrWhiteSpace(inMemoryDatabaseName))
                {
                    inMemoryDatabaseName = "GoodHamburgerDb";
                }

                services.AddDbContext<GoodHamburgerDbContext>(options =>
                    options.UseInMemoryDatabase(inMemoryDatabaseName));

                return;
            }

            if (!string.IsNullOrWhiteSpace(provider) &&
                !string.Equals(provider, "SqlServer", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    $"Unsupported database provider '{provider}'. Use 'SqlServer' or 'InMemory'.");
            }

            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "Connection string 'DefaultConnection' was not found.");

            services.AddDbContext<GoodHamburgerDbContext>(options =>
                options.UseSqlServer(connectionString));
        }
    }
}
