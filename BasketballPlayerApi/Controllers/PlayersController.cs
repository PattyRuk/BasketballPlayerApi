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

        // 1. GET Page, Filtered List
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerOutputDto>>> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? search = null,
            [FromQuery] string? position = null)
        {
            if (pageNumber < 1 || pageSize < 1) return BadRequest("Pagination bounds must exceed zero.");
            var records = await _service.GetPlayersPagedAsync(pageNumber, pageSize, search, position);
            return Ok(records);
        }

        // 2. GET By ID
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PlayerOutputDto>> GetById(int id)
        {
            var player = await _service.GetPlayerByIdAsync(id);
            if (player == null) return NotFound($"Player with Identifier {id} was not found.");
            return Ok(player);
        }


    }
}
