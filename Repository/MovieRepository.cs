using Microsoft.EntityFrameworkCore;
using vm.prueba.cinesalas.api.Database;
using vm.prueba.cinesalas.api.Models.Entities;
using vm.prueba.cinesalas.api.Repository.Interfaces;

namespace vm.prueba.cinesalas.api.Repository
{
    public class MovieRepository : RepositoryBase<Movie>, IMovieRepository
    {
        public MovieRepository(CinemaDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Movie>> GetByNameAsync(string name)
        {
            return await _dbSet
                .Where(m => m.Name.Contains(name))
                .ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetByReleaseDateAsync(DateTime releaseDate)
        {
            return await _dbSet
                .Where(m => m.ReleaseDate.Date == releaseDate.Date)
                .ToListAsync();
        }

    }
}
