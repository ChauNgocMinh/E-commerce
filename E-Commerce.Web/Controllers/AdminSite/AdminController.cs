using AutoMapper;
using E_Commerce.Application.Response.MovieResponse;
using E_Commerce.Controllers.UserSite;
using E_Commerce.Domain.Entities.Movies;
using E_Commerce.Domain.Interfaces;
using E_Commerce.Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Controllers.AdminSite
{
    [Route("Admin/[controller]/[action]")]
    public class MovieManagementController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Movie> _MovieRepository;
        private readonly IGenericRepository<MovieCategory> _MovieCategoryRepository;

        public MovieManagementController(ILogger<HomeController> logger, IMapper mapper, IGenericRepository<Movie> MovieRepository, IGenericRepository<MovieCategory> movieCategoryRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _MovieRepository = MovieRepository;
            _MovieCategoryRepository = movieCategoryRepository;
        }
        public async Task<IActionResult> MovieManagement()
        {
            var listMovie = await _MovieRepository.GetAllAsync(x => true, include: db => db.Include(x => x.MovieTags));
            var MovieResponses = _mapper.Map<List<MovieResponse>>(listMovie);
            return View(MovieResponses);
        }
        [HttpGet]
        public async Task<IActionResult> CreateMovie()
        {
            var categories = await _MovieCategoryRepository.GetAllAsync();
            ViewBag.Categories = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Title
            }).ToList();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMovie(MovieViewModel model)
        {
            if (ModelState.IsValid)
            {
                var movie = _mapper.Map<Movie>(model);
                if (model.VideoFile != null && model.VideoFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/videos");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.VideoFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.VideoFile.CopyToAsync(fileStream);
                    }
                    movie.UrlMedia = "/videos/" + fileName;
                }
                await _MovieRepository.AddAsync(movie);
                return RedirectToAction("MovieManagement");
            }

            return View(model);
        }

    }
}
