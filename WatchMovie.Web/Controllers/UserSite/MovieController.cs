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
        private readonly IGenericRepository<MovieCategory> _movieCategoryRepository;

        public MovieController(ILogger<HomeController> logger, IMapper mapper, IGenericRepository<Movie> MovieRepository, IGenericRepository<MovieFeedBack> MovieFeedBackcontext, IGenericRepository<MovieCategory> movieCategoryRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _MovieRepository = MovieRepository;
            _MovieFeedBackcontext = MovieFeedBackcontext;
            _movieCategoryRepository = movieCategoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetMovieByCategory(List<Guid> categoryIds, string filmType)
        {
            var movies = await _MovieRepository.GetAllAsync(x => true);
            if (categoryIds != null && categoryIds.Any())
            {
                movies = movies.Where(movie => categoryIds.Contains(movie.CategoryId)).ToList();
            }
            if (!string.IsNullOrEmpty(filmType))
            {
                movies = movies.Where(movie =>
                    (filmType == "free" && movie.IsFree) || (filmType == "paid" && !movie.IsFree)
                ).ToList();
            }
            var movieResponses = _mapper.Map<List<MovieResponse>>(movies);

            var categories = await _movieCategoryRepository.GetAllAsync();
            var categoryResponses = _mapper.Map<List<MovieCategoryResponse>>(categories);
            ViewData["Categories"] = categoryResponses;
            return View(movieResponses);
        }

        [HttpGet]
        public async Task<IActionResult> SearchMovie(string query)
        {
            var searchedMovies = await _MovieRepository.GetAllAsync(
                x => x.Name.ToLower().Contains(query.ToLower()),
                include: q => q.Include(o => o.MovieImages)
            );
            var searchedMovieResponses = _mapper.Map<List<MovieResponse>>(searchedMovies);
            var categories = await _movieCategoryRepository.GetAllAsync();
            var categoryResponses = _mapper.Map<List<MovieCategoryResponse>>(categories);
            ViewData["Categories"] = categoryResponses;
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
