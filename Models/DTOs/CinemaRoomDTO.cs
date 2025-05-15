using System.ComponentModel.DataAnnotations;

namespace vm.prueba.cinesalas.api.Models.DTOs
{
    public class CinemaRoomDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la sala es obligatorio")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La capacidad de la sala es obligatoria")]
        [Range(1, int.MaxValue, ErrorMessage = "La capacidad debe ser mayor a 0")]
        public int Capacity { get; set; }

        public bool IsVIP { get; set; }

        public string Status { get; set; }

        public int MovieCount { get; set; }

    }
}
