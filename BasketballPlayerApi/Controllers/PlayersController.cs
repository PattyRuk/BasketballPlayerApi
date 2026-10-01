using BasketballPlayerApi.BLL.DTOs;
using BasketballPlayerApi.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace BasketballPlayerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PlayersController : ControllerBase
    {
        private readonly ILeagueService _service;

        public PlayersController(ILeagueService service)
        {
            _service = service;
        }

        // GET Page, Filtered List
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerOutputDto>>> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? search = null,
            [FromQuery] string? position = null)
        {
            if (pageNumber < 1 || pageSize < 1) return BadRequest("Page number must exceed zero.");
            var records = await _service.GetPlayersPagedAsync(pageNumber, pageSize, search, position);
            return Ok(records);
        }

        // GET By ID
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PlayerOutputDto>> GetById(int id)
        {
            var player = await _service.GetPlayerByIdAsync(id);
            if (player == null) return NotFound($"Player with Id {id} was not found.");
            return Ok(player);
        }

        // POST Create
        [HttpPost]
        public async Task<ActionResult<PlayerOutputDto>> Create([FromBody] PlayerInputDto payload)
        {
            try
            {
                var result = await _service.CreatePlayerAsync(payload);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT Update
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] PlayerInputDto payload)
        {
            try
            {
                var success = await _service.UpdatePlayerAsync(id, payload);
                if (!success) return NotFound($"Target player {id} doesn't exist.");
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE 
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeletePlayerAsync(id);
            if (!success) return NotFound($"Target player {id} doesn't exist.");
            return NoContent();
        }
    }
}
