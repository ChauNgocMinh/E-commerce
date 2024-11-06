using AutoMapper;
using WatchMovie.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WatchMovie.Application.Response.MovieResponse;
using Microsoft.EntityFrameworkCore;
using WatchMovie.Domain.Entities.Movies;
namespace WatchMovie.Controllers.UserSite;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Movie> _MovieRepository;
    private readonly IGenericRepository<MovieCategory> _movieCategoryRepository;

    public HomeController(
        ILogger<HomeController> logger,
        IMapper mapper,
        IGenericRepository<Movie> MovieRepository,
        IGenericRepository<MovieCategory> movieCategoryRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _MovieRepository = MovieRepository;
        _movieCategoryRepository = movieCategoryRepository;
    }

    public async Task<IActionResult> Index()
    {
        var Movies = await _MovieRepository.GetAllAsync(x => true, include: query => query.Include(o => o.MovieImages).Include(db => db.MovieTags));
        var MovieResponses = _mapper.Map<List<MovieResponse>>(Movies);

        var categories = await _movieCategoryRepository.GetAllAsync();
        var categoryResponses = _mapper.Map<List<MovieCategoryResponse>>(categories);
        ViewData["Categories"] = categoryResponses;
        return View(MovieResponses);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View();
    }
}
