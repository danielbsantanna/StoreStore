using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Order.Application;
using Product.Application;
using Product.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.WebHost.UseUrls("http://*:5004");
builder.Services.AddControllers();
builder.Services.AddOpenApi();

#region Dependency Injection 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API DOCS",
        Version = "v1"
    });
});


builder.Services.AddSingleton<IMongoClient>(sp => new MongoClient("mongodb+srv://admin:admin@cluster0.v5ge4kh.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0"));
builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase("store");
});

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ProductConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://storestore-rabbitmq-1");
        cfg.ReceiveEndpoint("product-order-status", e =>
        {
            e.ConfigureConsumer<ProductConsumer>(context);
            e.ConfigureConsumeTopology = false;
        });
    });

});

builder.Services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBus>());
builder.Services.AddSingleton<MessagePublisher>();


builder.Services.AddTransient<ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();


#endregion



var app = builder.Build();

app.MapOpenApi();
app.UseWebSockets();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.UseReDoc(c =>
{
    c.DocumentTitle = "API Documentation";
    c.SpecUrl = "/swagger/v1/swagger.json";
});
app.UseStaticFiles();

app.Run();
