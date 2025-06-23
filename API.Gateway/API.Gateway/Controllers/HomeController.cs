using Microsoft.AspNetCore.Mvc;

namespace API.Gateway.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/docs")]
        public IActionResult Index()
        {
            const string html = @"
        <!DOCTYPE html>
        <html>
        <head>
            <title>Swagger UI</title>
            <link href='https://cdn.jsdelivr.net/npm/swagger-ui-dist/swagger-ui.css' rel='stylesheet' />
        </head>
        <body>
        <div id='swagger-ui'></div>
        <script src='https://cdn.jsdelivr.net/npm/swagger-ui-dist/swagger-ui-bundle.js'></script>
        <script>
        const ui = SwaggerUIBundle({
            urls: [
                { url: '/order/swagger/v1/swagger.json', name: 'Order Service' },
                { url: '/product/swagger/v1/swagger.json', name: 'Product Service' },
                { url: '/payment/swagger/v1/swagger.json', name: 'Payment Service' },
                { url: '/shipping/swagger/v1/swagger.json', name: 'Shipping Service' },
                { url: '/customer/swagger/v1/swagger.json', name: 'Customer Service' }
            ],
            dom_id: '#swagger-ui',
            deepLinking: true
        });
        </script>
        </body>
        </html>
        ";

            return Content(html, "text/html");
        }
    }

}
