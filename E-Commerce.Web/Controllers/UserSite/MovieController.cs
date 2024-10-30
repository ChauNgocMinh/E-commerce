using AutoMapper;
using E_Commerce.Application.Response.MovieResponse;
using E_Commerce.Domain.Entities.Movies;
using E_Commerce.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_Commerce.Controllers.UserSite
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
