using AzureShop.WebApi.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzureShop.WebApi.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        var orders = new List<OrderEntity>
        {
            new() { Id =  Guid.Parse("a70bbfc6-3cf8-45e1-97b4-2fc56606d87e"), Name = "Order 1", Price = 100 },
            new() { Id =  Guid.Parse("801868d3-184b-4d4b-91e4-dbe4a5fcacfd"), Name = "Order 2", Price = 200 },
            new() { Id =  Guid.Parse("715697c0-b901-40be-a1c5-4d9f29cb60ce"), Name = "Order 3", Price = 300 }
        };

        modelBuilder.Entity<OrderEntity>().HasData(orders);

        var products = new List<ProductEntity>
        {
            new() { Id = Guid.NewGuid(), Name = "Product 1", OrderId = Guid.Parse("a70bbfc6-3cf8-45e1-97b4-2fc56606d87e") },
            new() { Id =  Guid.NewGuid(), Name = "Product 2", OrderId = Guid.Parse("801868d3-184b-4d4b-91e4-dbe4a5fcacfd") },
            new() { Id =  Guid.NewGuid(), Name = "Product 3", OrderId = Guid.Parse("715697c0-b901-40be-a1c5-4d9f29cb60ce") }
        };

        modelBuilder.Entity<ProductEntity>().HasData(products);
    }
}


public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.Name).IsRequired();

        builder.HasOne(p => p.Order)
               .WithMany(o => o.Products)
               .HasForeignKey(p => p.OrderId);
    }
}

public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).ValueGeneratedOnAdd();
        builder.Property(o => o.Name).IsRequired();
        builder.Property(o => o.Price).HasColumnType("decimal(18,2)");
    }
}
