using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagementSystem.DataAccess.Data.Config
{
    internal class ItemConfiguration : IEntityTypeConfiguration<TbItem>
    {
        public void Configure(EntityTypeBuilder<TbItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(x => x.Quantity)
                .IsRequired();

            builder.Property(x => x.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            // Add check constraint to ensure Quantity is non-negative
            builder.HasCheckConstraint("CK_TbItem_Quantity_NonNegative", "[Quantity] >= 0");

            // Add check constraint to ensure Price is non-negative
            builder.HasCheckConstraint("CK_TbItem_Price_NonNegative", "[Price] >= 0");

            builder.ToTable("Items");
        }
    }
}
