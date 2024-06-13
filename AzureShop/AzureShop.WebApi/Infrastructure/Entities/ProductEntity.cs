using System.Text.Json.Serialization;

namespace AzureShop.WebApi.Infrastructure.Entities;

public class ProductEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Guid OrderId { get; set; }

    [JsonIgnore]
    public virtual OrderEntity Order { get; set; }
}
