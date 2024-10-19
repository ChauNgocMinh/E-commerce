using E_Commerce.Domain.Entities.Movies;
using E_Commerce.Domain.Interfaces;

namespace E_Commerce.Infastructure.Services.Movieervice
{
    internal class Movieervice
    {
        private readonly IGenericRepository<Movie> _MovieRepository;

        public Movieervice(IGenericRepository<Movie> MovieRepository)
        {
            _MovieRepository = MovieRepository;
        }

        public async Task<IEnumerable<Movie>> GetAllMovieAsync()
        {
            return await _MovieRepository.GetAllAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(Guid id)
        {
            return await _MovieRepository.GetByIdAsync(id);
        }

        public async Task AddMovieAsync(Movie Movie)
        {
            await _MovieRepository.AddAsync(Movie);
        }

        public async Task UpdateMovieAsync(Movie Movie)
        {
            await _MovieRepository.UpdateAsync(Movie);
        }

        public async Task DeleteMovieAsync(Guid id)
        {
            await _MovieRepository.DeleteAsync(id);
        }
    }
}
