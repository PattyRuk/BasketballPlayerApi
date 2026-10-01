using BasketballPlayerApi.BLL.DTOs;
using BasketballPlayerApi.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace BasketballPlayerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class StatsController : ControllerBase
    {
        private readonly ILeagueService _service;

        public StatsController(ILeagueService service)
        {
            _service = service;
        }

        // POST - Performance Stats
        [HttpPost]
        public async Task<IActionResult> PostStat([FromBody] PlayerGameStatInputDto payload)
        {
            var success = await _service.RecordStatsAsync(payload);
            if (!success) return BadRequest("Unable to assign stats. Check if Player and Game constraints are valid.");
            return Ok("Performance records logged successfully.");
        }

        // GET - Leaderboard Analytics 
        [HttpGet("leaderboard")]
        public async Task<ActionResult<IEnumerable<TeamLeaderboardDto>>> GetLeaderboard()
        {
            var analytics = await _service.GetTeamLeaderboardAsync();
            return Ok(analytics);
        }
    }
}
