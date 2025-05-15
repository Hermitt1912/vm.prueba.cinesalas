using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vm.prueba.cinesalas.api.Models.DTOs;
using vm.prueba.cinesalas.api.Services.Interfaces;

namespace vm.prueba.cinesalas.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            var response = await _movieService.GetAllMoviesAsync();

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var response = await _movieService.GetMovieByIdAsync(id);

            if (response.Success)
                return Ok(response);

            return NotFound(response);
        }

        [HttpGet("search/name")]
        public async Task<IActionResult> GetMoviesByName([FromQuery] string name)
        {
            var response = await _movieService.GetMoviesByNameAsync(name);

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpGet("search/releaseDate")]
        public async Task<IActionResult> GetMoviesByReleaseDate([FromQuery] DateTime releaseDate)
        {
            if (releaseDate == DateTime.MinValue)
                return BadRequest("La fecha de publicación es requerida");

            var response = await _movieService.GetMoviesByReleaseDateAsync(releaseDate);

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromBody] MovieDTO movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _movieService.CreateMovieAsync(movieDto);

            if (response.Success)
                return CreatedAtAction(nameof(GetMovieById), new { id = response.Data.Id }, response);

            return BadRequest(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] MovieDTO movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _movieService.UpdateMovieAsync(id, movieDto);

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var response = await _movieService.DeleteMovieAsync(id);

            if (response.Success)
                return Ok(response);

            return NotFound(response);
        }
    }
}