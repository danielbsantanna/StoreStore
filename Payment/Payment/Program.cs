using Payment.Application;
using Payment.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MassTransit;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.WebHost.UseUrls("http://*:5003");


#region Dependency Injection 

builder.Services.AddSingleton<IMongoClient>(sp => new MongoClient("mongodb+srv://admin:admin@cluster0.v5ge4kh.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0"));
builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase("store");
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API DOCS",
        Version = "v1"
    });
});

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<PaymentConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {

        cfg.Host("rabbitmq://storestore-rabbitmq-1");
        cfg.ReceiveEndpoint("payment-new", e =>
        {
            e.ConfigureConsumer<PaymentConsumer>(context);
        });
    });
});

builder.Services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBus>());
builder.Services.AddSingleton<MessagePublisher>();

builder.Services.AddTransient<PaymentService>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();


#endregion



var app = builder.Build();

app.MapOpenApi();
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
