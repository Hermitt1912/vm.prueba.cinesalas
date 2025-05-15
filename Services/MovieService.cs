using AutoMapper;
using vm.prueba.cinesalas.api.Models.DTOs;
using vm.prueba.cinesalas.api.Models.Entities;
using vm.prueba.cinesalas.api.Models.Responses;
using vm.prueba.cinesalas.api.Repository.Interfaces;
using vm.prueba.cinesalas.api.Services.Interfaces;

namespace vm.prueba.cinesalas.api.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MovieService(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<MovieDTO>>> GetAllMoviesAsync()
        {
            try
            {
                var movies = await _movieRepository.GetAllAsync();
                var moviesDto = _mapper.Map<IEnumerable<MovieDTO>>(movies);
                return ApiResponse<IEnumerable<MovieDTO>>.Ok(moviesDto);
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<MovieDTO>>.Error($"Error al obtener las películas: {ex.Message}");
            }
        }

        public async Task<ApiResponse<MovieDTO>> GetMovieByIdAsync(int id)
        {
            try
            {
                var movie = await _movieRepository.GetByIdAsync(id);
                if (movie == null)
                    return ApiResponse<MovieDTO>.Error("Película no encontrada");

                var movieDto = _mapper.Map<MovieDTO>(movie);
                return ApiResponse<MovieDTO>.Ok(movieDto);
            }
            catch (Exception ex)
            {
                return ApiResponse<MovieDTO>.Error($"Error al obtener la película: {ex.Message}");
            }

        }
        public async Task<ApiResponse<IEnumerable<MovieDTO>>> GetMoviesByNameAsync(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    return ApiResponse<IEnumerable<MovieDTO>>.Error("El nombre de la película es requerido");

                var movies = await _movieRepository.GetByNameAsync(name);
                var moviesDto = _mapper.Map<IEnumerable<MovieDTO>>(movies);
                return ApiResponse<IEnumerable<MovieDTO>>.Ok(moviesDto);
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<MovieDTO>>.Error($"Error al buscar películas por nombre: {ex.Message}");
            }
        }

        public async Task<ApiResponse<IEnumerable<MovieDTO>>> GetMoviesByReleaseDateAsync(DateTime releaseDate)
        {
            try
            {
                if (releaseDate == DateTime.MinValue)
                    return ApiResponse<IEnumerable<MovieDTO>>.Error("La fecha de publicación es requerida");

                var movies = await _movieRepository.GetByReleaseDateAsync(releaseDate);
                var moviesDto = _mapper.Map<IEnumerable<MovieDTO>>(movies);
                return ApiResponse<IEnumerable<MovieDTO>>.Ok(moviesDto);
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<MovieDTO>>.Error($"Error al buscar películas por fecha de publicación: {ex.Message}");
            }
        }

        public async Task<ApiResponse<MovieDTO>> CreateMovieAsync(MovieDTO movieDto)
        {
            try
            {
                var movie = _mapper.Map<Movie>(movieDto);
                movie = await _movieRepository.CreateAsync(movie);
                var createdMovieDto = _mapper.Map<MovieDTO>(movie);
                return ApiResponse<MovieDTO>.Ok(createdMovieDto, "Película creada con éxito");
            }
            catch (Exception ex)
            {
                return ApiResponse<MovieDTO>.Error($"Error al crear la película: {ex.Message}");
            }
        }

        public async Task<ApiResponse<MovieDTO>> UpdateMovieAsync(int id, MovieDTO movieDto)
        {
            try
            {
                var existingMovie = await _movieRepository.GetByIdAsync(id);
                if (existingMovie == null)
                    return ApiResponse<MovieDTO>.Error("Película no encontrada");

                _mapper.Map(movieDto, existingMovie);
                existingMovie.UpdatedAt = DateTime.Now;

                await _movieRepository.UpdateAsync(existingMovie);
                var updatedMovieDto = _mapper.Map<MovieDTO>(existingMovie);
                return ApiResponse<MovieDTO>.Ok(updatedMovieDto, "Película actualizada con éxito");
            }
            catch (Exception ex)
            {
                return ApiResponse<MovieDTO>.Error($"Error al actualizar la película: {ex.Message}");
            }
        }

        public async Task<ApiResponse<bool>> DeleteMovieAsync(int id)
        {
            try
            {
                var result = await _movieRepository.DeleteAsync(id);
                if (!result)
                    return ApiResponse<bool>.Error("Película no encontrada");

                return ApiResponse<bool>.Ok(true, "Película eliminada con éxito");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.Error($"Error al eliminar la película: {ex.Message}");
            }
        }
    }
}