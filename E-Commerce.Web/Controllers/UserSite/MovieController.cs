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

            var movieResponse = _mapper.Map<MovieResponse>(movie);
            var feedbackResponses = _mapper.Map<List<MovieFeedBackResponse>>(reviews);

            var viewModel = new MovieDetailViewModel
            {
                Movie = movieResponse,
                Feedbacks = feedbackResponses
            };

            return View(viewModel);
        }


    }
}
