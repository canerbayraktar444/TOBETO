namespace AzureShop.WebApi.Infrastructure.Entities;

public class OrderEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public ICollection<ProductEntity> Products { get; set; }
}
