using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Application;
using Order.Domain;

namespace Order.API.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _orderService.GetOrdersAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]OrderEntity order)
        { 
            return Ok(await _orderService.CreateOrderAsync(order));
        }

    }
}
