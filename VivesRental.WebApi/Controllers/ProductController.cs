using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VivesRental.Services;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Requests;

namespace VivesRental.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Find([FromQuery]ProductFilter? filter)
        {
            var Products = await _productService.FindAsync(filter);
            return Ok(Products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var products = await _productService.GetAsync(id);
            return Ok(products);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ProductRequest request)
        {
            var product = await _productService.CreateAsync(request);
            return Ok(product);
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> Edit([FromQuery] Guid id, [FromBody] ProductRequest request)
        {
            var product = await _productService.EditAsync(id, request);
            return Ok(product);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Remove([FromRoute]Guid id)
        {
            var succes = await _productService.RemoveAsync(id);
            return Ok(succes);

        }
    }
}
