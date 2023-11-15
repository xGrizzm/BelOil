using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.services.contracts;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _beloilService;

        public GameController(IGameService beloilService)
        {
            _beloilService = beloilService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetGame()
        {
            return Ok(await _beloilService.GetGameAsync());
        }

        [Authorize]
        [HttpPut]
        [Route("fields/{fieldId}/oilpumps/{oilPumpId}/collect")]
        public async Task<IActionResult> CollectOilPump(int fieldId, int oilPumpId)
        {
            try
            {
                return Ok(await _beloilService.CollectOilPumpAsync(fieldId, oilPumpId));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPost]
        [Route("fields/buy")]
        public async Task<IActionResult> BuyField()
        {
            try
            {
                return Ok(await _beloilService.BuyFieldAsync());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPost]
        [Route("fields/{fieldId}/oilpumps/buy")]
        public async Task<IActionResult> BuyOilPump(int fieldId)
        {
            try
            {
                return Ok(await _beloilService.BuyOilPumpAsync(fieldId));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
