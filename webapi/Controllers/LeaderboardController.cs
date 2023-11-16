using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.services.contracts;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeaderboardController : ControllerBase
    {
        private readonly ILeaderboardService _leaderboardService;

        public LeaderboardController(ILeaderboardService leaderboardService)
        {
            _leaderboardService = leaderboardService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetLeaderboard()
        {
            return Ok(await _leaderboardService.GetLeaderboardAsync());
        }
    }
}
