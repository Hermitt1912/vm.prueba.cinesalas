using vm.prueba.cinesalas.api.Models.Entities;

namespace vm.prueba.cinesalas.api.Repository.Interfaces
{
    public interface IMovieRepository : IRepositoryBase<Movie>
    {
        Task<IEnumerable<Movie>> GetByNameAsync(string name);
        Task<IEnumerable<Movie>> GetByReleaseDateAsync(DateTime releaseDate);
    }

}
