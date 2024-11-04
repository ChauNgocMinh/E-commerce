using AutoMapper;
using WatchMovie.Application.Response.MovieResponse;
using WatchMovie.Controllers.UserSite;
using WatchMovie.Domain.Entities.Movies;
using WatchMovie.Domain.Interfaces;
using WatchMovie.Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace WatchMovie.Controllers.AdminSite
{
    [Route("Admin/[controller]/[action]")]
    [Authorize]
    public class MovieManagementController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Movie> _MovieRepository;
        private readonly IGenericRepository<MovieImage> _MovieImageRepository;
        private readonly IGenericRepository<MovieCategory> _MovieCategoryRepository;

        public MovieManagementController(IMapper mapper, IGenericRepository<Movie> MovieRepository, IGenericRepository<MovieCategory> movieCategoryRepository, IGenericRepository<MovieImage> movieImageRepository)
        {
            _mapper = mapper;
            _MovieRepository = MovieRepository;
            _MovieCategoryRepository = movieCategoryRepository;
            _MovieImageRepository = movieImageRepository;
        }

        [HttpGet]
        public async Task<IActionResult> MovieManagement(string search)
        {
            var listMovie = string.IsNullOrWhiteSpace(search)
                ? await _MovieRepository.GetAllAsync(x => true, include: db => db.Include(x => x.MovieTags).Include(x => x.MovieImages))
                : await _MovieRepository.GetAllAsync(
                    x => x.Name.ToLower().Contains(search.ToLower()),
                    include: db => db.Include(x => x.MovieTags).Include(x => x.MovieImages)
                  );

            var MovieResponses = _mapper.Map<List<MovieResponse>>(listMovie);
            return View(MovieResponses);
        }

        [HttpGet]
        public async Task<IActionResult> DetailMovie(Guid id)
        {
            var movie = await _MovieRepository.GetByIdAsync(id, db => db.Include(x => x.MovieImages).Include(x => x.MovieCategory));
            if (movie == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<MovieResponse>(movie);
            return View(model);
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
            // Ensure that a video URL has been provided
            if (string.IsNullOrWhiteSpace(model.VideoFile))
            {
                ModelState.AddModelError("VideoUrl", "Link video không được để trống.");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                var movie = _mapper.Map<Movie>(model);

                movie.UrlMedia = model.VideoFile;
                await _MovieRepository.AddAsync(movie);
                if (model.MovieImgBanner != null && model.MovieImgBanner.Length > 0)
                {
                    var imageFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    if (!Directory.Exists(imageFolder))
                    {
                        Directory.CreateDirectory(imageFolder);
                    }
                    var imageFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.MovieImgBanner.FileName);
                    var imageFilePath = Path.Combine(imageFolder, imageFileName);
                    using (var fileStream = new FileStream(imageFilePath, FileMode.Create))
                    {
                        await model.MovieImgBanner.CopyToAsync(fileStream);
                    }
                    var movieImage = new MovieImage
                    {
                        UrlImage = "/images/" + imageFileName,
                        IsMain = true,
                        MovieId = movie.Id
                    };
                    await _MovieImageRepository.AddAsync(movieImage);
                }

                return RedirectToAction("MovieManagement");
            }

            ModelState.AddModelError("Lỗi", "Có lỗi khi tạo mới phim");
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var movie = await _MovieRepository.GetByIdAsync(id);
            if (movie != null)
            {
                await _MovieRepository.DeleteAsync(id);
            }
            return RedirectToAction("MovieManagement");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var movie = await _MovieRepository.GetByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<MovieViewModel>(movie);
            var categories = await _MovieCategoryRepository.GetAllAsync();

            ViewBag.Categories = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Title
            }).ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MovieViewModel model)
        {
            if (ModelState.IsValid)
            {
                var movie = await _MovieRepository.GetByIdAsync(model.Id);
                if (movie == null)
                {
                    return NotFound();
                }
                movie.Name = model.Name;
                movie.Description = model.Description;
                movie.IsFree = model.IsFree;
                movie.CategoryId = model.CategoryId;
                if (!string.IsNullOrWhiteSpace(model.VideoFile))
                {
                    movie.UrlMedia = model.VideoFile;
                }

                await _MovieRepository.UpdateAsync(movie);
                return RedirectToAction("MovieManagement");
            }

            ModelState.AddModelError("Lỗi", "Có lỗi khi cập nhật phim");
            return View(model);
        }
    }
}
