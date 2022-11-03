using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VivesRental.Services;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Requests;

namespace VivesRental.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleReservationController : ControllerBase
    {
        private readonly ArticleReservationService _articleReservationService;

        public ArticleReservationController(ArticleReservationService articleReservationService)
        {
            _articleReservationService = articleReservationService;
        }

        [HttpGet]
        public async Task<IActionResult> FindAsync([FromQuery]ArticleReservationFilter? filter)
        {
            var reservations = await _articleReservationService.FindAsync(filter);
            return Ok(reservations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var reservation = await _articleReservationService.GetAsync(id);
            return Ok(reservation);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync([FromBody]ArticleReservationRequest request)
        {
            var newreservation = _articleReservationService.CreateAsync(request);
            return Ok(newreservation);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            var delete = await _articleReservationService.RemoveAsync(id);
            return Ok(delete);
        }
    }
}
