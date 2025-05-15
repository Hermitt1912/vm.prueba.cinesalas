using AutoMapper;
using vm.prueba.cinesalas.api.Models.DTOs;
using vm.prueba.cinesalas.api.Models.Entities;
using vm.prueba.cinesalas.api.Models.Responses;
using vm.prueba.cinesalas.api.Repository.Interfaces;
using vm.prueba.cinesalas.api.Services.Interfaces;

namespace vm.prueba.cinesalas.api.Services
{
    public class CinemaRoomService : ICinemaRoomService
    {
        private readonly ICinemaRoomRepository _cinemaRoomRepository;
        private readonly IRepositoryBase<MovieCinemaRoom> _movieCinemaRoomRepository;
        private readonly IRepositoryBase<Movie> _movieRepository;
        private readonly IMapper _mapper;

        public CinemaRoomService(
            ICinemaRoomRepository cinemaRoomRepository,
            IRepositoryBase<MovieCinemaRoom> movieCinemaRoomRepository,
            IRepositoryBase<Movie> movieRepository,
            IMapper mapper)
        {
            _cinemaRoomRepository = cinemaRoomRepository;
            _movieCinemaRoomRepository = movieCinemaRoomRepository;
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<CinemaRoomDTO>>> GetAllCinemaRoomsAsync()
        {
            try
            {
                var rooms = await _cinemaRoomRepository.GetAllCinemaRoomsWithStatusAsync();
                return ApiResponse<IEnumerable<CinemaRoomDTO>>.Ok(rooms);
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<CinemaRoomDTO>>.Error($"Error al obtener las salas: {ex.Message}");
            }
        }

        public async Task<ApiResponse<CinemaRoomDTO>> GetCinemaRoomByIdAsync(int id)
        {
            try
            {
                var room = await _cinemaRoomRepository.GetByIdAsync(id);
                if (room == null)
                    return ApiResponse<CinemaRoomDTO>.Error("Sala no encontrada");

                var movieCount = await _cinemaRoomRepository.GetMovieCountByCinemaRoomIdAsync(id);

                var roomDto = _mapper.Map<CinemaRoomDTO>(room);
                roomDto.MovieCount = movieCount;

                // Establecer el estado según el conteo de películas
                if (movieCount < 3)
                    roomDto.Status = "Sala disponible";
                else if (movieCount >= 3 && movieCount <= 5)
                    roomDto.Status = $"Sala con {movieCount} películas asignadas";
                else
                    roomDto.Status = "Sala no disponible";

                return ApiResponse<CinemaRoomDTO>.Ok(roomDto);
            }
            catch (Exception ex)
            {
                return ApiResponse<CinemaRoomDTO>.Error($"Error al obtener la sala: {ex.Message}");
            }
        }

        public async Task<ApiResponse<CinemaRoomDTO>> GetCinemaRoomByNameAsync(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    return ApiResponse<CinemaRoomDTO>.Error("El nombre de la sala es requerido");

                var roomDto = await _cinemaRoomRepository.GetCinemaRoomWithStatusAsync(name);
                if (roomDto == null)
                    return ApiResponse<CinemaRoomDTO>.Error("Sala no encontrada");

                return ApiResponse<CinemaRoomDTO>.Ok(roomDto);
            }
            catch (Exception ex)
            {
                return ApiResponse<CinemaRoomDTO>.Error($"Error al buscar sala por nombre: {ex.Message}");
            }
        }

        public async Task<ApiResponse<CinemaRoomDTO>> CreateCinemaRoomAsync(CinemaRoomDTO cinemaRoomDto)
        {
            try
            {
                var room = _mapper.Map<CinemaRoom>(cinemaRoomDto);
                room = await _cinemaRoomRepository.CreateAsync(room);

                var createdRoomDto = _mapper.Map<CinemaRoomDTO>(room);
                createdRoomDto.MovieCount = 0;
                createdRoomDto.Status = "Sala disponible"; // Nueva sala, sin películas asignadas

                return ApiResponse<CinemaRoomDTO>.Ok(createdRoomDto, "Sala creada con éxito");
            }
            catch (Exception ex)
            {
                return ApiResponse<CinemaRoomDTO>.Error($"Error al crear la sala: {ex.Message}");
            }
        }

        public async Task<ApiResponse<CinemaRoomDTO>> UpdateCinemaRoomAsync(int id, CinemaRoomDTO cinemaRoomDto)
        {
            try
            {
                var existingRoom = await _cinemaRoomRepository.GetByIdAsync(id);
                if (existingRoom == null)
                    return ApiResponse<CinemaRoomDTO>.Error("Sala no encontrada");

                _mapper.Map(cinemaRoomDto, existingRoom);
                existingRoom.UpdatedAt = DateTime.Now;

                await _cinemaRoomRepository.UpdateAsync(existingRoom);

                var movieCount = await _cinemaRoomRepository.GetMovieCountByCinemaRoomIdAsync(id);
                var updatedRoomDto = _mapper.Map<CinemaRoomDTO>(existingRoom);
                updatedRoomDto.MovieCount = movieCount;

                // Establecer el estado según el conteo de películas
                if (movieCount < 3)
                    updatedRoomDto.Status = "Sala disponible";
                else if (movieCount >= 3 && movieCount <= 5)
                    updatedRoomDto.Status = $"Sala con {movieCount} películas asignadas";
                else
                    updatedRoomDto.Status = "Sala no disponible";

                return ApiResponse<CinemaRoomDTO>.Ok(updatedRoomDto, "Sala actualizada con éxito");
            }
            catch (Exception ex)
            {
                return ApiResponse<CinemaRoomDTO>.Error($"Error al actualizar la sala: {ex.Message}");
            }
        }

        public async Task<ApiResponse<bool>> DeleteCinemaRoomAsync(int id)
        {
            try
            {
                var result = await _cinemaRoomRepository.DeleteAsync(id);
                if (!result)
                    return ApiResponse<bool>.Error("Sala no encontrada");

                return ApiResponse<bool>.Ok(true, "Sala eliminada con éxito");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.Error($"Error al eliminar la sala: {ex.Message}");
            }
        }

        public async Task<ApiResponse<IEnumerable<MovieCinemaRoomDTO>>> GetMoviesByCinemaRoomIdAsync(int cinemaRoomId)
        {
            try
            {
                var movieCinemaRooms = await _movieCinemaRoomRepository.FindAsync(mcr => mcr.CinemaRoomId == cinemaRoomId);
                var movieCinemaRoomDtos = new List<MovieCinemaRoomDTO>();

                foreach (var mcr in movieCinemaRooms)
                {
                    var movie = await _movieRepository.GetByIdAsync(mcr.MovieId);
                    var cinemaRoom = await _cinemaRoomRepository.GetByIdAsync(mcr.CinemaRoomId);

                    var dto = _mapper.Map<MovieCinemaRoomDTO>(mcr);
                    dto.MovieName = movie?.Name;
                    dto.CinemaRoomName = cinemaRoom?.Name;

                    movieCinemaRoomDtos.Add(dto);
                }

                return ApiResponse<IEnumerable<MovieCinemaRoomDTO>>.Ok(movieCinemaRoomDtos);
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<MovieCinemaRoomDTO>>.Error($"Error al obtener películas por sala: {ex.Message}");
            }
        }

        public async Task<ApiResponse<MovieCinemaRoomDTO>> AssignMovieToCinemaRoomAsync(MovieCinemaRoomDTO movieCinemaRoomDto)
        {
            try
            {
                // Verificar si la película existe
                var movie = await _movieRepository.GetByIdAsync(movieCinemaRoomDto.MovieId);
                if (movie == null)
                    return ApiResponse<MovieCinemaRoomDTO>.Error("Película no encontrada");

                // Verificar si la sala existe
                var cinemaRoom = await _cinemaRoomRepository.GetByIdAsync(movieCinemaRoomDto.CinemaRoomId);
                if (cinemaRoom == null)
                    return ApiResponse<MovieCinemaRoomDTO>.Error("Sala no encontrada");

                // Verificar si la sala está disponible (tiene menos de 6 películas)
                var movieCount = await _cinemaRoomRepository.GetMovieCountByCinemaRoomIdAsync(movieCinemaRoomDto.CinemaRoomId);
                if (movieCount >= 6)
                    return ApiResponse<MovieCinemaRoomDTO>.Error("La sala no está disponible, ya tiene 6 o más películas asignadas");

                // Crear la relación
                var movieCinemaRoom = _mapper.Map<MovieCinemaRoom>(movieCinemaRoomDto);
                movieCinemaRoom = await _movieCinemaRoomRepository.CreateAsync(movieCinemaRoom);

                // Preparar la respuesta
                var createdDto = _mapper.Map<MovieCinemaRoomDTO>(movieCinemaRoom);
                createdDto.MovieName = movie.Name;
                createdDto.CinemaRoomName = cinemaRoom.Name;

                return ApiResponse<MovieCinemaRoomDTO>.Ok(createdDto, "Película asignada a sala con éxito");
            }
            catch (Exception ex)
            {
                return ApiResponse<MovieCinemaRoomDTO>.Error($"Error al asignar película a sala: {ex.Message}");
            }
        }
    }
}
