using Application.Authentication.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("Authentication")]
    public class Authentication : ApiController
    {
        private ISender _mediatior;

        public Authentication(ISender mediatior)
        {
            _mediatior = mediatior;
        }

        [HttpPost("login")]
        public async Task<IActionResult> login(LoginComand comand) {
            var loginResult = await _mediatior.Send(comand);

            if (loginResult.IsError)
            {
                return BadRequest(new {
                    loginResult.FirstError.Code,
                    loginResult.FirstError.Description
                });
            }

            return Ok(loginResult.Value);
        }
    }
}
