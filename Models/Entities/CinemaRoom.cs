namespace vm.prueba.cinesalas.api.Models.Entities
{
    public class CinemaRoom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public bool IsVIP { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Relaciones de navegación
        public virtual ICollection<MovieCinemaRoom> MovieCinemaRooms { get; set; }

    }
}
