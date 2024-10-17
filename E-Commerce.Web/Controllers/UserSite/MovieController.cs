using AutoMapper;
using E_Commerce.Application.Response.ProductResponse;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Controllers.UserSite
{
    public class MovieController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Product> _productRepository;

        public MovieController(ILogger<HomeController> logger, IMapper mapper, IGenericRepository<Product> productRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _productRepository = productRepository;
        }
        public async Task<IActionResult> DetailMovie(Guid Id)
        {
            var products = await _productRepository.GetByIdAsync(Id, include: query => query.Include(o => o.ProductImages));
            var productResponses = _mapper.Map<ProductResponse>(products);
            return View(productResponses);
        }
    }
}
