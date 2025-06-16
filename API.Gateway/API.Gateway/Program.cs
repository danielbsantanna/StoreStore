var builder = WebApplication.CreateBuilder(args);


builder.WebHost.UseUrls("http://*:5000");
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

app.UseCors("AllowAllOrigins"); 

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();
app.MapControllers();
app.MapReverseProxy(); 


app.Run();
