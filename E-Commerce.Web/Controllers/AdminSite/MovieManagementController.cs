using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers.AdminSite
{
    public class MovieManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
