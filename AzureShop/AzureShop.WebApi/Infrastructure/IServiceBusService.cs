using AzureShop.WebApi.Infrastructure.Entities;

namespace AzureShop.WebApi.Infrastructure;
public interface IServiceBusService
{
    void ProductAdded(ProductEntity product);
}