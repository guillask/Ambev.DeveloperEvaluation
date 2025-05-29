using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id);

        builder.Property(u => u.creationDate).IsRequired();
        builder.Property(u => u.alterationDate);
        builder.Property(u => u.Name).HasMaxLength(100);
        builder.Property(u => u.Price).IsRequired();
    }
}
