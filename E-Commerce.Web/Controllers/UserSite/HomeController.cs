using AutoMapper;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.Application.Response.ProductResponse;
namespace E_Commerce.Controllers.UserSite;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Product> _productRepository;

    public HomeController(ILogger<HomeController> logger, IMapper mapper, IGenericRepository<Product> productRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productRepository.GetAllAsync();
        var productResponses = _mapper.Map<List<ProductResponse>>(products);
        return View(productResponses);
    }

    public IActionResult GetProductByCategory()
    {
        return View();
    }
    public IActionResult Rank()
    {
        return View();
    }
    public IActionResult Account()
    {
        return View();
    }
    public IActionResult Login()
    {
        return View();
    }
    public IActionResult ProductDetail()
    {
        return View();
    }
    public IActionResult MovieManager()
    {
        return View();
    }
    public IActionResult UserManager()
    {
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View();
    }
}
