using AutoMapper;
using WatchMovie.Application.Response.MovieResponse;
using WatchMovie.Domain.Entities.Movies;
using WatchMovie.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace WatchMovie.Controllers.UserSite
{
    public class MovieController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Movie> _MovieRepository;
        private readonly IGenericRepository<MovieFeedBack> _MovieFeedBackcontext;

        public MovieController(ILogger<HomeController> logger, IMapper mapper, IGenericRepository<Movie> MovieRepository, IGenericRepository<MovieFeedBack> MovieFeedBackcontext)
        {
            _logger = logger;
            _mapper = mapper;
            _MovieRepository = MovieRepository;
            _MovieFeedBackcontext = MovieFeedBackcontext;
        }
        [HttpGet]
        public async Task<IActionResult> GetMovieByCategory(string IdCateglory)
        {
            var Movies = await _MovieRepository.GetAllAsync(x => x.CategoryId == Guid.Parse(IdCateglory), include: query => query.Include(o => o.MovieImages));
            var MovieResponses = _mapper.Map<List<MovieResponse>>(Movies);
            return View(MovieResponses);
        }
        [HttpGet]
        public async Task<IActionResult> SearchMovie(string query)
        {
            var searchedMovies = await _MovieRepository.GetAllAsync(
                x => x.Name.ToLower().Contains(query.ToLower()),
                include: q => q.Include(o => o.MovieImages)
            );
            var searchedMovieResponses = _mapper.Map<List<MovieResponse>>(searchedMovies);
            return View(searchedMovieResponses);
        }

        public async Task<IActionResult> DetailMovie(Guid Id)
        {
            var movie = await _MovieRepository.GetByIdAsync(Id, include: query => query.Include(o => o.MovieImages));
            if (movie == null)
            {
                return NotFound();
            }
            var reviews = await _MovieFeedBackcontext.GetAllAsync(x => x.MovieId == Id, query => query.Include(o => o.Users));
            bool userHasRated = false;
            if (User.Identity.IsAuthenticated)
            {
                var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (!string.IsNullOrEmpty(userIdClaim))
                {
                    var userId = Guid.Parse(userIdClaim);
                    userHasRated = reviews.Any(r => r.UserId == userId);
                }
            }
            var movieResponse = _mapper.Map<MovieResponse>(movie);
            var feedbackResponses = _mapper.Map<List<MovieFeedBackResponse>>(reviews);
            var viewModel = new MovieDetailViewModel
            {
                Movie = movieResponse,
                Feedbacks = feedbackResponses,
                UserHasRated = userHasRated
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> PostFeedback(Guid movieId, int rating, string comment)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var feedback = new MovieFeedBack
            {
                MovieId = movieId,
                Rate = rating,
                Comment = comment,
                UserId = userId,
                CreatedAt = DateTime.Now
            };
            await _MovieFeedBackcontext.AddAsync(feedback);
            return RedirectToAction("DetailMovie", new { Id = movieId });
        }
    }
}
