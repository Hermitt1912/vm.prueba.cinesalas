namespace vm.prueba.cinesalas.api.Models.Entities
{
    public class MovieCinemaRoom
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int CinemaRoomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Propiedades de navegación
        public virtual Movie Movie { get; set; }
        public virtual CinemaRoom CinemaRoom { get; set; }

    }
}
