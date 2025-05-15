using vm.prueba.cinesalas.api.Models.DTOs;
using vm.prueba.cinesalas.api.Models.Responses;

namespace vm.prueba.cinesalas.api.Services.Interfaces
{
    public interface ICinemaRoomService
    {
        Task<ApiResponse<IEnumerable<CinemaRoomDTO>>> GetAllCinemaRoomsAsync();
        Task<ApiResponse<CinemaRoomDTO>> GetCinemaRoomByIdAsync(int id);
        Task<ApiResponse<CinemaRoomDTO>> GetCinemaRoomByNameAsync(string name);
        Task<ApiResponse<CinemaRoomDTO>> CreateCinemaRoomAsync(CinemaRoomDTO cinemaRoomDto);
        Task<ApiResponse<CinemaRoomDTO>> UpdateCinemaRoomAsync(int id, CinemaRoomDTO cinemaRoomDto);
        Task<ApiResponse<bool>> DeleteCinemaRoomAsync(int id);
        Task<ApiResponse<IEnumerable<MovieCinemaRoomDTO>>> GetMoviesByCinemaRoomIdAsync(int cinemaRoomId);
        Task<ApiResponse<MovieCinemaRoomDTO>> AssignMovieToCinemaRoomAsync(MovieCinemaRoomDTO movieCinemaRoomDto);
    }
}
