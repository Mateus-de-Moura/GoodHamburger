using GoodHamburger.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Infrastructure.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");

            builder.HasKey(c => c.Id);

            var fixedDate = new DateTime(2026, 04, 20, 0, 0, 0, DateTimeKind.Utc);

            var categories = new Category[]
            {
                new Category
                {
                    Id = new Guid("071857e9-1dfb-4b81-8300-9b025b4fbf0a"),
                    CreatedAt = fixedDate,
                    UpdatedAt = fixedDate,
                    Description = "Hamburgers"
                },
                new Category
                {
                    Id = new Guid("13cecac6-961b-474a-9843-553a057a6c11"),
                    CreatedAt = fixedDate,
                    UpdatedAt = fixedDate,
                    Description = "Accompaniment"
                },

            };

            builder.HasData(categories);
        }
    }
}
