using AutoMapper;
using E_Commerce.Application.Response.MovieResponse;
using E_Commerce.Domain.Entities.Movies;
using E_Commerce.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Controllers.UserSite
{
    public class MovieController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Movie> _MovieRepository;

        public MovieController(ILogger<HomeController> logger, IMapper mapper, IGenericRepository<Movie> MovieRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _MovieRepository = MovieRepository;
        }
        public async Task<IActionResult> DetailMovie(Guid Id)
        {
            var Movies = await _MovieRepository.GetByIdAsync(Id, include: query => query.Include(o => o.MovieImages));
            var MovieResponses = _mapper.Map<MovieResponse>(Movies);
            return View(MovieResponses);
        }
    }
}
