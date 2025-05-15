using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vm.prueba.cinesalas.api.Models.DTOs;
using vm.prueba.cinesalas.api.Services.Interfaces;

namespace vm.prueba.cinesalas.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemaRoomController : ControllerBase
    {
        private readonly ICinemaRoomService _cinemaRoomService;

        public CinemaRoomController(ICinemaRoomService cinemaRoomService)
        {
            _cinemaRoomService = cinemaRoomService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllCinemaRooms()
        {
            var response = await _cinemaRoomService.GetAllCinemaRoomsAsync();

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCinemaRoomById(int id)
        {
            var response = await _cinemaRoomService.GetCinemaRoomByIdAsync(id);

            if (response.Success)
                return Ok(response);

            return NotFound(response);
        }

        [HttpGet("search/name")]
        public async Task<IActionResult> GetCinemaRoomByName([FromQuery] string name)
        {
            var response = await _cinemaRoomService.GetCinemaRoomByNameAsync(name);

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCinemaRoom([FromBody] CinemaRoomDTO cinemaRoomDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _cinemaRoomService.CreateCinemaRoomAsync(cinemaRoomDto);

            if (response.Success)
                return CreatedAtAction(nameof(GetCinemaRoomById), new { id = response.Data.Id }, response);

            return BadRequest(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCinemaRoom(int id, [FromBody] CinemaRoomDTO cinemaRoomDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _cinemaRoomService.UpdateCinemaRoomAsync(id, cinemaRoomDto);

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCinemaRoom(int id)
        {
            var response = await _cinemaRoomService.DeleteCinemaRoomAsync(id);

            if (response.Success)
                return Ok(response);

            return NotFound(response);
        }

        [HttpGet("{id}/movies")]
        public async Task<IActionResult> GetMoviesByCinemaRoomId(int id)
        {
            var response = await _cinemaRoomService.GetMoviesByCinemaRoomIdAsync(id);

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpPost("assign-movie")]
        public async Task<IActionResult> AssignMovieToCinemaRoom([FromBody] MovieCinemaRoomDTO movieCinemaRoomDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _cinemaRoomService.AssignMovieToCinemaRoomAsync(movieCinemaRoomDto);

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }
    }
}