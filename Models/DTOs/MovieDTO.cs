using System.ComponentModel.DataAnnotations;

namespace vm.prueba.cinesalas.api.Models.DTOs
{
    public class MovieDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la película es obligatorio")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "La fecha de publicación es obligatoria")]
        public DateTime ReleaseDate { get; set; }

        public string Director { get; set; }

        public int Duration { get; set; }

        public string Genre { get; set; }

    }
    public class MovieSearchDTO
    {
        public string Name { get; set; }
        public DateTime? ReleaseDate { get; set; }
    }

}
