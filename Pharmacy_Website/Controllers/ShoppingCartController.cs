using Microsoft.AspNetCore.Mvc;

namespace Pharmacy_Website.Controllers
{
    public class ShoppingCartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Checkout()
        {
            return View();
        }
        public IActionResult ThinkYou()
        {
            return View();
        }
    }
}
