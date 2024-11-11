using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagementSystem.DataAccess.Data.Config
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<TbCategory>
    {
        public void Configure(EntityTypeBuilder<TbCategory> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(150)
                .IsRequired();

            builder.ToTable("Categories");
        }
    }
}
