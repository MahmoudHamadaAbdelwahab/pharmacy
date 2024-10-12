using Microsoft.AspNetCore.Mvc;

namespace Pharmacy_Website.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
