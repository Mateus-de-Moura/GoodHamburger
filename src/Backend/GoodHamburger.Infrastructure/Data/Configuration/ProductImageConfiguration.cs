using GoodHamburger.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoodHamburger.Infrastructure.Data.Configuration
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImage");

            builder.HasKey(pi => pi.Id);

            builder.Property(pi => pi.ImageBytes)
                .IsRequired(false);

            builder.Property(pi => pi.FileName)
                .IsRequired(false);

            builder.Property(pi => pi.ContentType)
                .IsRequired(false);

            builder.Property(pi => pi.ProductId)
                .IsRequired();

            builder.HasData(
                new ProductImage
                {
                    Id = SeedData.XBurgerImageId,
                    ProductId = SeedData.XBurgerProductId,
                    FileName = "x-burger-placeholder.txt",
                    ContentType = "application/octet-stream",
                    ImageBytes = Array.Empty<byte>(),
                    Active = true,
                    CreatedAt = SeedData.FixedDate,
                    UpdatedAt = SeedData.FixedDate
                },
                new ProductImage
                {
                    Id = SeedData.XEggImageId,
                    ProductId = SeedData.XEggProductId,
                    FileName = "x-egg-placeholder.txt",
                    ContentType = "application/octet-stream",
                    ImageBytes = Array.Empty<byte>(),
                    Active = true,
                    CreatedAt = SeedData.FixedDate,
                    UpdatedAt = SeedData.FixedDate
                },
                new ProductImage
                {
                    Id = SeedData.XBaconImageId,
                    ProductId = SeedData.XBaconProductId,
                    FileName = "x-bacon-placeholder.txt",
                    ContentType = "application/octet-stream",
                    ImageBytes = Array.Empty<byte>(),
                    Active = true,
                    CreatedAt = SeedData.FixedDate,
                    UpdatedAt = SeedData.FixedDate
                },
                new ProductImage
                {
                    Id = SeedData.FriesImageId,
                    ProductId = SeedData.FriesProductId,
                    FileName = "batata-frita-placeholder.txt",
                    ContentType = "application/octet-stream",
                    ImageBytes = Array.Empty<byte>(),
                    Active = true,
                    CreatedAt = SeedData.FixedDate,
                    UpdatedAt = SeedData.FixedDate
                },
                new ProductImage
                {
                    Id = SeedData.SodaImageId,
                    ProductId = SeedData.SodaProductId,
                    FileName = "refrigerante-placeholder.txt",
                    ContentType = "application/octet-stream",
                    ImageBytes = Array.Empty<byte>(),
                    Active = true,
                    CreatedAt = SeedData.FixedDate,
                    UpdatedAt = SeedData.FixedDate
                }
            );
        }
    }
}
