using GoodHamburger.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoodHamburger.Infrastructure.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");
            builder.HasKey(c => c.Id);

            builder.HasData(
                new Category
                {
                    Id = SeedData.SandwichCategoryId,
                    CreatedAt = SeedData.FixedDate,
                    UpdatedAt = SeedData.FixedDate,
                    Description = "Lanche"
                },
                new Category
                {
                    Id = SeedData.SideCategoryId,
                    CreatedAt = SeedData.FixedDate,
                    UpdatedAt = SeedData.FixedDate,
                    Description = "Acompanhamento"
                },
                new Category
                {
                    Id = SeedData.DrinkCategoryId,
                    CreatedAt = SeedData.FixedDate,
                    UpdatedAt = SeedData.FixedDate,
                    Description = "Bebida"
                }
            );
        }
    }
}
