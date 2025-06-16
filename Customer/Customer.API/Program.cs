using Customer.Application;
using Customer.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.WebHost.UseUrls("http://*:5002");
builder.Services.AddControllers();
builder.Services.AddOpenApi();

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseAuthorization();

app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});




#region Manual Testing

var serviceProvider = builder.Services.BuildServiceProvider();
var customerRepository = serviceProvider.GetRequiredService<ICustomerRepository>();
var customers = await customerRepository.GetAllAsync();


#endregion


app.Run();
