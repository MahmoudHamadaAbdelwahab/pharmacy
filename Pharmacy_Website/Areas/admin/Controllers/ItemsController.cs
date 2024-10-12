using Microsoft.AspNetCore.Mvc;

namespace Pharmacy_Website.Areas.admin.Controllers
{
    [Area("admin")]
    public class ItemsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
