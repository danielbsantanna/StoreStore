using Customer.Application;
using Customer.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://*:5002");
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API DOCS",
        Version = "v1"
    });
});


#region Dependency Injection 

builder.Services.AddSingleton<IMongoClient>(sp => new MongoClient("mongodb+srv://admin:admin@cluster0.v5ge4kh.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0"));


builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase("store");
});

builder.Services.AddTransient<CustomerService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();


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
