using MassTransit;
using MessageContracts;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Order.Application;
using Order.Infrastructure;
using System.Collections.Concurrent;
using System.Net;
using System.Net.WebSockets;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.WebHost.UseUrls("http://*:5001");

#region Dependency Injection 

builder.Services.AddSingleton<IMongoClient>(sp => new MongoClient("mongodb+srv://admin:admin@cluster0.v5ge4kh.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0"));
builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase("store");
});


builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://storestore-rabbitmq-1");
        cfg.ReceiveEndpoint("order-update-status", e =>
        {
            e.ConfigureConsumer<OrderConsumer>(context);
        });

        cfg.Publish<OrderUpdateEvent>(x =>
        {
            x.ExchangeType = "headers";
        });
    });

});

builder.Services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBus>());
builder.Services.AddSingleton<MessagePublisher>();
builder.Services.AddSingleton<NotificationService>();

builder.Services.AddTransient<OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddSignalR();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAllOrigins",
//        policy => policy.AllowAnyOrigin()
//                        .AllowAnyMethod()
//                        .AllowAnyHeader());
//});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") 
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

#endregion


var app = builder.Build();

app.UseCors("AllowAllOrigins");

app.MapHub<NotificationHub>("/notifications").RequireCors("AllowFrontend");


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseWebSockets();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
