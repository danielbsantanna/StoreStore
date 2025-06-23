var builder = WebApplication.CreateBuilder(args);


builder.WebHost.UseUrls("http://*:5000");
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

app.UseCors("AllowAllOrigins"); 
app.MapOpenApi();
app.UseAuthorization();
app.MapControllers();
app.MapReverseProxy();
app.UseSwagger();
app.UseSwaggerUI();

app.UseReDoc(c =>
{
    c.DocumentTitle = "API Documentation";
    c.SpecUrl = "/swagger/v1/swagger.json";
});
app.UseStaticFiles();
app.UseRouting();

//app.MapGet("/docs", async context =>
//{
//    const string html = """
//    <!DOCTYPE html>
//    <html>
//      <head>
//        <title>API Docs</title>
//        <script src="https://cdn.redoc.ly/redoc/latest/bundles/redoc.standalone.js"></script>
//      </head>
//      <body>
//        <redoc spec-url="http://localhost:5001/swagger/v1/swagger.json"></redoc>
//      </body>
//    </html>
//    """;

//    context.Response.ContentType = "text/html";
//    await context.Response.WriteAsync(html);
//});




app.Run();
