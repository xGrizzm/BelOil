using Microsoft.AspNetCore.Mvc;
using webapi.core.Models.Requests;
using webapi.services.contracts;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;

        public AuthorizationController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest loginModel)
        {
            try
            {
                return Ok(await _authorizationService.LoginAsync(loginModel));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
