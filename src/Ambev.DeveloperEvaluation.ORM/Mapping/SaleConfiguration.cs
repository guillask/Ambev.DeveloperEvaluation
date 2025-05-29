using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id);

        builder.Property(u => u.SaleDate).IsRequired();
        builder.Property(u => u.Customer).HasMaxLength(50);
        builder.Property(u => u.Branch).IsRequired().HasMaxLength(10);

        builder.Property(u => u.TotalSaleAmount).IsRequired();

        builder.Property(u => u.Cancelled).IsRequired();

        builder.Property(u => u.ProductsDiscount)
                       .HasConversion(
                           v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                           v => JsonSerializer.Deserialize<List<ProductsDiscountModel>>(v, (JsonSerializerOptions)null))
                       .HasColumnType("nvarchar(max)");
    }
}
