using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using vm.prueba.cinesalas.api.Models.Entities;

namespace vm.prueba.cinesalas.api.Database
{
    public class CinemaDbContext : DbContext
    {
        public CinemaDbContext(DbContextOptions<CinemaDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<CinemaRoom> CinemaRooms { get; set; }
        public DbSet<MovieCinemaRoom> MovieCinemaRooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Eliminación lógica para películas
            modelBuilder.Entity<Movie>().HasQueryFilter(m => !m.IsDeleted);

            // Relación muchos a muchos Movie <-> CinemaRoom
            modelBuilder.Entity<MovieCinemaRoom>()
                .HasKey(mcr => new { mcr.MovieId, mcr.CinemaRoomId });

            modelBuilder.Entity<MovieCinemaRoom>()
                .HasOne(mcr => mcr.Movie)
                .WithMany(m => m.MovieCinemaRooms)
                .HasForeignKey(mcr => mcr.MovieId);

            modelBuilder.Entity<MovieCinemaRoom>()
                .HasOne(mcr => mcr.CinemaRoom)
                .WithMany(cr => cr.MovieCinemaRooms)
                .HasForeignKey(mcr => mcr.CinemaRoomId);
        }
    }
}
