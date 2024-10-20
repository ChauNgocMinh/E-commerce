using AutoMapper;
using E_Commerce.Application.Response.MovieResponse;
using E_Commerce.Controllers.UserSite;
using E_Commerce.Domain.Entities.Movies;
using E_Commerce.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Controllers.AdminSite
{
    public class ManagementController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Movie> _MovieRepository;

        public ManagementController(ILogger<HomeController> logger, IMapper mapper, IGenericRepository<Movie> MovieRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _MovieRepository = MovieRepository;
        }
        public async Task<IActionResult> MovieManagement()
        {
            var listMovie = await _MovieRepository.GetAllAsync(x => true, include: db => db.Include(x => x.MovieTags));
            var MovieResponses = _mapper.Map<List<MovieResponse>>(listMovie);
            return View(MovieResponses);
        }
    }
}
