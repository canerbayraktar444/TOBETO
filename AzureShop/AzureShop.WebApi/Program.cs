using AzureShop.WebApi.Infrastructure;
using AzureShop.WebApi.Infrastructure.Entities;
using AzureShop.WebApi.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IServiceBusService, ServiceBusService>();

builder.Services.AddApplicationInsightsTelemetry(/*instrumentationKey: ""*/);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    //options.UseInMemoryDatabase("AzureShop");
    var connectionString = builder.Configuration.GetConnectionString("SqlServer");
    options.UseSqlServer(connectionString, options =>
    {
        options.UseAzureSqlDefaults(true);
    });

    options.EnableDetailedErrors();

#if DEBUG
    options.EnableSensitiveDataLogging();
#endif
});

builder.Services.AddAzureClients(clients =>
{
    clients
        .AddServiceBusClient(builder.Configuration.GetConnectionString("ServiceBus"))
        .WithName("servicebus_client");
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/products", async ([FromServices] AppDbContext context) =>
{
    return await context.Products.ToListAsync();
});

app.MapGet("/orders", async ([FromServices] AppDbContext context) =>
{
    return await context.Orders.Include(i => i.Products).ToListAsync();
});

app.MapPost("/products", async ([FromServices] AppDbContext context, [FromBody] ProductEntity product) =>
{
    context.Products.Add(product);
    await context.SaveChangesAsync();
    return Results.Created($"/products/{product.Id}", product);
});

app.MapPost("/orders", async ([FromServices] AppDbContext context, [FromBody] OrderEntity order) =>
{
    context.Orders.Add(order);
    await context.SaveChangesAsync();
    return Results.Created($"/orders/{order.Id}", order);
});

app.MapPost("/cart", async ([FromServices] IServiceBusService serviceBusService, [FromBody] ProductEntity product) =>
{
    serviceBusService.ProductAdded(product);
    return Results.Accepted();
});

// create inmemory database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (dbContext.Database.IsInMemory())
        dbContext.Database.EnsureCreated();
}

app.Run();




