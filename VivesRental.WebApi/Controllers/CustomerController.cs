using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VivesRental.Services;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Requests;
using VivesRental.Services.Model.Results;

namespace VivesRental.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> Find([FromQuery] CustomerFilter? filter)
        {
            var customers = await _customerService.FindAsync(filter);
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]Guid id)
        {
            var customer = await _customerService.GetAsync(id);
            return Ok(customer);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody]CustomerRequest request)
        {
            var newcustomer = await _customerService.CreateAsync(request);
            return Ok(newcustomer);
        }

        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> Edit([FromRoute]Guid id, [FromBody]CustomerRequest request)
        {
            var customer = await _customerService.EditAsync(id, request);
            return Ok(customer);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            var delete = await _customerService.RemoveAsync(id);
            return Ok(delete);
        }
    }
}
