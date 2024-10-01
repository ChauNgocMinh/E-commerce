using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
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
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View();
    }
}
