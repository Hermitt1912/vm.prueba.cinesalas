using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using vm.prueba.cinesalas.api.Database;
using vm.prueba.cinesalas.api.Models.DTOs;
using vm.prueba.cinesalas.api.Models.Entities;
using vm.prueba.cinesalas.api.Repository.Interfaces;
using Microsoft.Extensions.Configuration;

namespace vm.prueba.cinesalas.api.Repository
{
    public class CinemaRoomRepository : RepositoryBase<CinemaRoom>, ICinemaRoomRepository
    {
        private readonly IConfiguration _configuration;

        // Recibe solo el contexto y la configuración
        public CinemaRoomRepository(CinemaDbContext context, IConfiguration configuration) : base(context)
        {
            _configuration = configuration;
        }

        public async Task<CinemaRoomDTO> GetCinemaRoomWithStatusAsync(string name)
        {
            var cinemaRoom = await _dbSet
                .FirstOrDefaultAsync(c => c.Name == name);

            if (cinemaRoom == null)
                return null;

            var movieCount = await GetMovieCountByCinemaRoomIdAsync(cinemaRoom.Id);

            var status = GetStatusByMovieCount(movieCount);

            return new CinemaRoomDTO
            {
                Id = cinemaRoom.Id,
                Name = cinemaRoom.Name,
                Capacity = cinemaRoom.Capacity,
                IsVIP = cinemaRoom.IsVIP,
                MovieCount = movieCount,
                Status = status
            };
        }

        public async Task<IEnumerable<CinemaRoomDTO>> GetAllCinemaRoomsWithStatusAsync()
        {
            var result = new List<CinemaRoomDTO>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("CinemaDbConnection")))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("GetCinemaRoomsWithMovieCount", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new CinemaRoomDTO
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                MovieCount = reader.GetInt32(2),
                                Status = reader.GetString(3),
                                // Capacity e IsVIP no vienen del SP, se establecen valores por defecto
                                Capacity = 0,
                                IsVIP = false
                            });
                        }
                    }
                }
            }

            return result;
        }

        public async Task<int> GetMovieCountByCinemaRoomIdAsync(int cinemaRoomId)
        {
            return await _context.MovieCinemaRooms
                .CountAsync(mcr => mcr.CinemaRoomId == cinemaRoomId && mcr.IsActive);
        }

        private string GetStatusByMovieCount(int movieCount)
        {
            if (movieCount < 3)
                return "Sala disponible";
            else if (movieCount >= 3 && movieCount <= 5)
                return $"Sala con {movieCount} películas asignadas";
            else
                return "Sala no disponible";
        }
    }
}
