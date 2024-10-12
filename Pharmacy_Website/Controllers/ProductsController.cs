using Microsoft.AspNetCore.Mvc;

namespace Pharmacy_Website.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}
