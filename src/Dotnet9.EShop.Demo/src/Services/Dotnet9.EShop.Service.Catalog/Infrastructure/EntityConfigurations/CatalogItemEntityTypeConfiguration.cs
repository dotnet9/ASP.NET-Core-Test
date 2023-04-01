using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dotnet9.EShop.Service.Catalog.Infrastructure.EntityConfigurations;

public class CatalogItemEntityTypeConfiguration : IEntityTypeConfiguration<CatalogItem>
{
    public void Configure(EntityTypeBuilder<CatalogItem> builder)
    {
        builder.ToTable("Catalog");

        builder.Property(catalogItem => catalogItem.Id).IsRequired();

        builder.Property(catalogItem => catalogItem.Name).IsRequired().HasMaxLength(50);

        builder.Property(catalogItem => catalogItem.Price).IsRequired();

        builder.Property(catalogItem => catalogItem.PictureFileName).IsRequired(false);

        builder.Property<Guid>("_catalogBrandId").UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("CatalogBrandId")
            .IsRequired();

        builder.Property<int>("_catalogTypeId").UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("CatalogTypeId").IsRequired();

        builder.HasOne(catalogItem => catalogItem.CatalogBrand).WithMany().HasForeignKey("_catalogBrandId");

        builder.HasOne(catalogItem => catalogItem.CatalogType).WithMany().HasForeignKey("_catalogTypeId");
    }
}