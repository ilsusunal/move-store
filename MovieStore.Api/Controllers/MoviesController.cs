using Microsoft.AspNetCore.Mvc;
using MovieStore.Api.Models.Requests;
using MovieStore.Api.Services.Interfaces;

namespace MovieStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _movieService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _movieService.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMovieRequest request)
        {
            var result = await _movieService.CreateAsync(request);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMovieRequest request)
        {
            var updated = await _movieService.UpdateAsync(id, request);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _movieService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
