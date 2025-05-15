using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vm.prueba.cinesalas.api.Models.DTOs;
using vm.prueba.cinesalas.api.Services.Interfaces;

namespace vm.prueba.cinesalas.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var response = await _authService.LoginAsync(loginDto);

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }
    }
}