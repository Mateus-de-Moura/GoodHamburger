using GoodHamburger.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoodHamburger.Infrastructure.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Active)
                .IsRequired();

            builder.Property(p => p.CreatedAt)
                .IsRequired();

            builder.Property(p => p.UpdatedAt)
                .IsRequired();

            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.ProductImage)
                .WithOne(pi => pi.Product)
                .HasForeignKey<Product>(p => p.ProductImageId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(
                new Product
                {
                    Id = SeedData.XBurgerProductId,
                    Name = "X Burger",
                    Description = "X Burger",
                    Price = 5.00m,
                    CategoryId = SeedData.SandwichCategoryId,
                    ProductImageId = SeedData.XBurgerImageId,
                    Active = true,
                    CreatedAt = SeedData.FixedDate,
                    UpdatedAt = SeedData.FixedDate
                },
                new Product
                {
                    Id = SeedData.XEggProductId,
                    Name = "X Egg",
                    Description = "X Egg",
                    Price = 4.50m,
                    CategoryId = SeedData.SandwichCategoryId,
                    ProductImageId = SeedData.XEggImageId,
                    Active = true,
                    CreatedAt = SeedData.FixedDate,
                    UpdatedAt = SeedData.FixedDate
                },
                new Product
                {
                    Id = SeedData.XBaconProductId,
                    Name = "X Bacon",
                    Description = "X Bacon",
                    Price = 7.00m,
                    CategoryId = SeedData.SandwichCategoryId,
                    ProductImageId = SeedData.XBaconImageId,
                    Active = true,
                    CreatedAt = SeedData.FixedDate,
                    UpdatedAt = SeedData.FixedDate
                },
                new Product
                {
                    Id = SeedData.FriesProductId,
                    Name = "Batata frita",
                    Description = "Batata frita",
                    Price = 2.00m,
                    CategoryId = SeedData.SideCategoryId,
                    ProductImageId = SeedData.FriesImageId,
                    Active = true,
                    CreatedAt = SeedData.FixedDate,
                    UpdatedAt = SeedData.FixedDate
                },
                new Product
                {
                    Id = SeedData.SodaProductId,
                    Name = "Refrigerante",
                    Description = "Refrigerante",
                    Price = 2.50m,
                    CategoryId = SeedData.DrinkCategoryId,
                    ProductImageId = SeedData.SodaImageId,
                    Active = true,
                    CreatedAt = SeedData.FixedDate,
                    UpdatedAt = SeedData.FixedDate
                }
            );
        }
    }
}
