using vm.prueba.cinesalas.api.Models.DTOs;
using vm.prueba.cinesalas.api.Models.Responses;

namespace vm.prueba.cinesalas.api.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<TokenDTO>> LoginAsync(LoginDTO loginDto);
    }
}
