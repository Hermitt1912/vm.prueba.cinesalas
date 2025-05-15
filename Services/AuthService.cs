using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using vm.prueba.cinesalas.api.Models.DTOs;
using vm.prueba.cinesalas.api.Models.Responses;
using vm.prueba.cinesalas.api.Services.Interfaces;

namespace vm.prueba.cinesalas.api.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<ApiResponse<TokenDTO>> LoginAsync(LoginDTO loginDto)
        {
            // Para simplificar, usamos credenciales fijas como se solicita en la prueba
            if (loginDto.Username == "admin" && loginDto.Password == "admin")
            {
                var token = GenerateJwtToken(loginDto.Username);

                return ApiResponse<TokenDTO>.Ok(new TokenDTO
                {
                    Token = token,
                    Username = loginDto.Username
                }, "Inicio de sesión exitoso");
            }

            return ApiResponse<TokenDTO>.Error("Credenciales inválidas");
        }

        private string GenerateJwtToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpiryMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}