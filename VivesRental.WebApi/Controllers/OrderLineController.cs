using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using VivesRental.Services;
using VivesRental.Services.Model.Filters;

namespace VivesRental.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderLineController : ControllerBase
    {
        private readonly OrderLineService _orderLineService;

        public OrderLineController(OrderLineService orderLineService)
        {
            _orderLineService = orderLineService;
        }

        [HttpGet]
        public async Task<IActionResult> Find([FromQuery] OrderLineFilter? orderline)
        {
            var orderlines = await _orderLineService.FindAsync(orderline);
            return Ok(orderlines);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]Guid id)
        {
            var orderline = await _orderLineService.GetAsync(id);
            return Ok(orderline);
        }

        [HttpGet("Rent")]
        public async Task<IActionResult> Rent([FromQuery] Guid orderid, [FromQuery] Guid articleId)
        {
            var succes = await _orderLineService.RentAsync(orderid, articleId);
            return Ok(succes);
        }

        [HttpPost("RentList")]
        public async Task<IActionResult> RentList([FromQuery] Guid orderId, [FromBody] IList<Guid> articleIds)
        {
            var succes = await _orderLineService.RentAsync(orderId, articleIds);
            return Ok(succes);
        }

        [HttpPost("Return")]
        public async Task<IActionResult> Return([FromQuery]Guid orderlineId, [FromBody] DateTime returnedAt)
        {
            var succes = await _orderLineService.ReturnAsync(orderlineId, returnedAt);
            return Ok(succes);
        }
    }
}
