using GiftShop.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GiftShop.DataAccess.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(e => e.Id).HasColumnName("ID");

            builder.Property(e => e.Name).HasMaxLength(20);

            builder.HasMany(e => e.Products)
                .WithOne(p => p.Category)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
