using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VivesRental.Services;
using VivesRental.Services.Model.Filters;

namespace VivesRental.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> Find([FromQuery] OrderFilter? filter)
        {
            var orders = await _orderService.FindAsync(filter);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get( Guid id)
        {
            var order = await _orderService.GetAsync(id);
            return Ok(order);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Guid CustomerId)
        {
            var order = await _orderService.CreateAsync(CustomerId);
            return Ok(order);
        }

        [HttpGet("Return")]
        public async Task<IActionResult> Return([FromQuery] Guid orderId, [FromQuery] DateTime returnedAt)
        {
            var succes = await _orderService.ReturnAsync(orderId, returnedAt);
            return Ok(succes);
        }
    }
}
