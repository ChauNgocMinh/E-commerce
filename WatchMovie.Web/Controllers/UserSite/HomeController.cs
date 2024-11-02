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

    public HomeController(ILogger<HomeController> logger, IMapper mapper, IGenericRepository<Movie> MovieRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _MovieRepository = MovieRepository;
    }

    public async Task<IActionResult> Index()
    {
        var Movies = await _MovieRepository.GetAllAsync(x => true, include: query => query.Include(o => o.MovieImages).Include(db => db.MovieTags));
        var MovieResponses = _mapper.Map<List<MovieResponse>>(Movies);
        return View(MovieResponses);
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View();
    }
}
