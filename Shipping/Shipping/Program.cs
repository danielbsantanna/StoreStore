using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Order.Application;
using Shipping.Application;
using Shipping.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.WebHost.UseUrls("http://*:5005");
builder.Services.AddControllers();
builder.Services.AddOpenApi();

#region Dependency Injection 

builder.Services.AddSingleton<IMongoClient>(sp => new MongoClient("mongodb+srv://admin:admin@cluster0.v5ge4kh.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0"));
builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase("store");
});

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ShippingConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://storestore-rabbitmq-1");
        cfg.ReceiveEndpoint("shipping-order-status", e =>
        {
            e.ConfigureConsumer<ShippingConsumer>(context);
            e.ConfigureConsumeTopology = false;
        });
    });

});

builder.Services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBus>());
builder.Services.AddSingleton<MessagePublisher>();


builder.Services.AddTransient<ShippingService>();
builder.Services.AddScoped<IShippingRepository, ShippingRepository>();


#endregion



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



#region Manual Testing

var serviceProvider = builder.Services.BuildServiceProvider();
var repo = serviceProvider.GetRequiredService<IShippingRepository>();
var result = await repo.GetAllAsync();


#endregion


app.Run();
