using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VivesRental.Enums;
using VivesRental.Services;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Requests;

namespace VivesRental.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly ArticleService _articleService;

        public ArticleController(ArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        public async Task<IActionResult> FindAsync([FromQuery]ArticleFilter? filter)
        {
            var articles = await _articleService.FindAsync(filter);

            return Ok(articles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
        {
            var article = await _articleService.GetAsync(id);

            return Ok(article);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody]ArticleRequest article)
        {
            var newarticle = await _articleService.CreateAsync(article);
            var check = await GetAsync(article.ProductId);

            return Ok(check);
        }

        [HttpPut("Edit/{id}")] // need to check how values are coming in.
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody]ArticleStatus status)
        {
            var article = await _articleService.UpdateStatusAsync(id, status);

            return Ok(article);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var delete = _articleService.RemoveAsync(id);
            return Ok(delete);

        }

    }
}
