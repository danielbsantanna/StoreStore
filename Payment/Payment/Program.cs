using Payment.Application;
using Payment.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.WebHost.UseUrls("http://*:5003");
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



#region Manual Testing

//var serviceProvider = builder.Services.BuildServiceProvider();
//var repo = serviceProvider.GetRequiredService<IPaymentRepository>();
//var result = await repo.GetAllAsync();


#endregion


app.Run();
