using E_Commerce.Domain.Entities.Products;
using E_Commerce.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers.AdminSite
{
    public class ProductsController : Controller
    {
        private readonly IGenericRepository<Product> _productRepository;

        public ProductsController(IGenericRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepository.GetAllAsync();
            return View(products);
        }
    }
}
