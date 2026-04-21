using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GoodHamburger.Infrastructure.Data.Context
{
    public class GoodHamburgerDbContextFactory : IDesignTimeDbContextFactory<GoodHamburgerDbContext>
    {
        public GoodHamburgerDbContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (string.IsNullOrWhiteSpace(environment))
            {
                environment = "Development";
            }

            var apiProjectPath = ResolveApiProjectPath();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(apiProjectPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: false)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    "Connection string 'DefaultConnection' was not found in appsettings/environment configuration.");
            }

            var optionsBuilder = new DbContextOptionsBuilder<GoodHamburgerDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new GoodHamburgerDbContext(optionsBuilder.Options);
        }

        private static string ResolveApiProjectPath()
        {
            var currentDirectory = Directory.GetCurrentDirectory();

            var directCandidates = new[]
            {
                Path.Combine(currentDirectory, "..", "GoodHamburger.Api"),
                Path.Combine(currentDirectory, "..", "..", "GoodHamburger.Api"),
                Path.Combine(currentDirectory, "src", "Backend", "GoodHamburger.Api")
            };

            foreach (var candidate in directCandidates)
            {
                var fullPath = Path.GetFullPath(candidate);
                if (File.Exists(Path.Combine(fullPath, "appsettings.json")))
                {
                    return fullPath;
                }
            }

            var directory = new DirectoryInfo(currentDirectory);
            while (directory is not null)
            {
                var candidate = Path.Combine(directory.FullName, "src", "Backend", "GoodHamburger.Api");
                if (File.Exists(Path.Combine(candidate, "appsettings.json")))
                {
                    return candidate;
                }

                directory = directory.Parent;
            }

            throw new InvalidOperationException(
                "Could not locate 'src/Backend/GoodHamburger.Api/appsettings.json' for design-time DbContext creation.");
        }
    }
}
