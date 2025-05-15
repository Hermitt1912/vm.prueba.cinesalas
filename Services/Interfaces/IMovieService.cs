using vm.prueba.cinesalas.api.Models.DTOs;
using vm.prueba.cinesalas.api.Models.Responses;

namespace vm.prueba.cinesalas.api.Services.Interfaces
{
    public interface IMovieService
    {
        Task<ApiResponse<IEnumerable<MovieDTO>>> GetAllMoviesAsync();
        Task<ApiResponse<MovieDTO>> GetMovieByIdAsync(int id);
        Task<ApiResponse<IEnumerable<MovieDTO>>> GetMoviesByNameAsync(string name);
        Task<ApiResponse<IEnumerable<MovieDTO>>> GetMoviesByReleaseDateAsync(DateTime releaseDate);
        Task<ApiResponse<MovieDTO>> CreateMovieAsync(MovieDTO movieDto);
        Task<ApiResponse<MovieDTO>> UpdateMovieAsync(int id, MovieDTO movieDto);
        Task<ApiResponse<bool>> DeleteMovieAsync(int id);
    }
}
