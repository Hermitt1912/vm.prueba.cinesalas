using vm.prueba.cinesalas.api.Models.DTOs;
using vm.prueba.cinesalas.api.Models.Entities;

namespace vm.prueba.cinesalas.api.Repository.Interfaces
{
    public interface ICinemaRoomRepository : IRepositoryBase<CinemaRoom>
    {
        Task<CinemaRoomDTO> GetCinemaRoomWithStatusAsync(string name);
        Task<IEnumerable<CinemaRoomDTO>> GetAllCinemaRoomsWithStatusAsync();
        Task<int> GetMovieCountByCinemaRoomIdAsync(int cinemaRoomId);
    }


}
