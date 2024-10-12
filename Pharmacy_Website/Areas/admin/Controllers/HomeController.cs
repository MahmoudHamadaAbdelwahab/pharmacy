using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pharmacy_Website.Areas.admin.Controllers
{
    [Area("admin")]
    public class HomeController : Controller
    {
        [Authorize(Roles ="Admin")]
        public IActionResult Index()
        {
            return View();
        }
 
    }
}
