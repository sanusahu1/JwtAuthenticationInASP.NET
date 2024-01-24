using JwtAuthenticationDemo.DTOs;
using JwtAuthenticationDemo.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static JwtAuthenticationDemo.Responses.CustomResponses;

namespace JwtAuthenticationDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccount _accountrepo;

        public AccountController(IAccount accountrepo)
        {
            _accountrepo = accountrepo;
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> RegisterAsync(RegisterDTO mode)
        {
            var result = await _accountrepo.RegisterAsync(mode);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResopnse>> LoginAsync (LoginDTO model)
        {
            var result = await _accountrepo.LoginAsync(model);
            return Ok(result);
        }
    }
}
