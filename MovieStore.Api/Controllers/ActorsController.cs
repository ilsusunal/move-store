using Microsoft.AspNetCore.Mvc;
using MovieStore.Api.Models.Requests;
using MovieStore.Api.Services.Interfaces;

namespace MovieStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActorsController : ControllerBase
    {
        private readonly IActorService _actorService;

        public ActorsController(IActorService actorService)
        {
            _actorService = actorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _actorService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _actorService.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateActorRequest request)
        {
            var result = await _actorService.CreateAsync(request);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateActorRequest request)
        {
            var updated = await _actorService.UpdateAsync(id, request);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _actorService.DeleteAsync(id);
            return deleted ? NoContent() : Conflict("Bu oyuncunun ilişkili olduğu filmler var.");
        }
    }
}
