using Azure.Messaging.ServiceBus;
using AzureShop.WebApi.Infrastructure.Entities;
using Microsoft.Extensions.Azure;
using System.Text;
using System.Text.Json;

namespace AzureShop.WebApi.Infrastructure;

public class ServiceBusService(IAzureClientFactory<ServiceBusClient> serviceBusClientFactory) : IServiceBusService
{
    public void ProductAdded(ProductEntity product)
    {
        var client = serviceBusClientFactory.CreateClient("servicebus_client");
        var sender = client.CreateSender(queueOrTopicName: "product-added");

        var message = new ServiceBusMessage(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(product)));
        sender.SendMessageAsync(message);
    }
}
