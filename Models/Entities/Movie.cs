namespace vm.prueba.cinesalas.api.Models.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? Director { get; set; }
        public int Duration { get; set; }
        public string? Genre { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Relaciones de navegación
        public virtual ICollection<MovieCinemaRoom> MovieCinemaRooms { get; set; }
        public bool IsDeleted { get; internal set; }
    }
}
