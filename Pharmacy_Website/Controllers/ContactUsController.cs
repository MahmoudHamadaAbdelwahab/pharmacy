using Microsoft.AspNetCore.Mvc;

namespace Pharmacy_Website.Controllers
{
    public class ContactUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
