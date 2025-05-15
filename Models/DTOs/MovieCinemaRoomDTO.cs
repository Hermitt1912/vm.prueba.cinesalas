using System.ComponentModel.DataAnnotations;

namespace vm.prueba.cinesalas.api.Models.DTOs
{
    public class MovieCinemaRoomDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El ID de la película es obligatorio")]
        public int MovieId { get; set; }

        [Required(ErrorMessage = "El ID de la sala es obligatorio")]
        public int CinemaRoomId { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "La fecha de fin es obligatoria")]
        public DateTime EndDate { get; set; }

        // Propiedades adicionales para mostrar información
        public string MovieName { get; set; }
        public string CinemaRoomName { get; set; }

    }
}
