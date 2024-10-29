using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dominos.Bl;
using Dominos.Models;
using System.Diagnostics;
using Pharmacy_Website.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pharmacy_Website.Controllers
{
    public class HomeController : Controller
    {
		IProducts _IProd;
		IClassification _class;
		IPharmicsts _pharm;
		ISupplier _supp;
		public HomeController(IProducts pro, IClassification classifi, IPharmicsts pharm, ISupplier supp)
		{
            _IProd = pro; _pharm = pharm;
			_supp = supp; _class = classifi;
		}

        public IActionResult Index()
        {
            VmHomePage Vm = new VmHomePage();
            Vm.lstProducts = _IProd.GetAllWithImages();
            Vm.lstPharmcists = _pharm.GetAll();
            Vm.lstSuppliers = _supp.GetAll();

            ViewBag.Classifications = _class.GetAll();
            ViewBag.Products = _IProd.GetAll();

            return View(Vm);
        }

        public IActionResult Search(SearchModel searchModel)
        {
            VmHomePage Vm = new VmHomePage();

            // Filter products by name if ProductName is not null or empty
            Vm.lstProducts = string.IsNullOrEmpty(searchModel.ProductName)
                             ? _IProd.GetAllWithImages()
                             : _IProd.SearchProductsByName(searchModel.ProductName);

            Vm.lstPharmcists = _pharm.GetAll();
            Vm.lstSuppliers = _supp.GetAll();

            ViewBag.Classifications = _class.GetAll();
            ViewBag.Products = Vm.lstProducts;

            ViewBag.SearchName = searchModel.ProductName;
            return View(Vm);
        }

        public IActionResult Data() { 
			return View();
		}
    }
}
