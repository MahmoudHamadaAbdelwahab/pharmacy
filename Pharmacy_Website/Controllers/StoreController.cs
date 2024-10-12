using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pharmacy_Website.Models;
using Dominos.Models;
using Dominos.Bl;

namespace Pharmacy_Website.Controllers
{
    public class StoreController : Controller
    {
        IProducts _IProd;
        public StoreController(IProducts prod) {
            _IProd = prod;
        }
        public IActionResult Index()
        {
            return View(_IProd.GetAllWithImages());
        }

        public IActionResult Details(int prodId)
        {
            var product = new ProductsModel();

            if (product == null)
                return NotFound();
            else 
                product = _IProd.GetById(prodId);
            

            return View(product);
        }
        public IActionResult Shooping( )
        {
            return View();
        }
        
    }
}
