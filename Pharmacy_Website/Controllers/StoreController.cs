using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pharmacy_Website.Models;
using Dominos.Models;
using Dominos.Bl;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pharmacy_Website.Controllers
{
    public class StoreController : Controller
    {
       ISupplier _ISupplier;
       IPharmicsts _IPharmcists;
       IClassification _IProdClassification;
        IProducts _IProd;
        public StoreController( IProducts prod,ISupplier supp , IPharmicsts pharmicst ,
           IClassification classifi )
        {
            _IProd = prod;
            _IProdClassification = classifi;
            _ISupplier = supp;
            _IPharmcists = pharmicst;
        }

        //public IActionResult Index()
        //{
        //    return View(_IProd.GetAllWithImages());
        //}

        public IActionResult Index(SearchModel searchModel)
        {
            var products = _IProd.GetAllWithImages();
           
            // Apply filtering based on searchModel
            if (!string.IsNullOrEmpty(searchModel.SearchNameProducts))
            {
                products = products.Where(p => p.ProductName.Contains(searchModel.SearchNameProducts, StringComparison.OrdinalIgnoreCase)).ToList();
            }

   
            if (searchModel.SelectedClassificationId.HasValue)
            {
                products = products.Where(p => p.ProdcutsClassificationId == searchModel.SelectedClassificationId.Value).ToList();
            }

            var classification = _IProdClassification.GetAll();
            ViewBag.ClassificationList = new SelectList(classification, "Id", "Name");

            return View((products, searchModel));
        }

        public IActionResult Details(int prodId)
        {
            var product = new ProductsModel();

            if (product == null)
                return NotFound();
            else 
                product = _IProd.GetByIdWithImages(prodId);
            

            return View(product);
        }
        public IActionResult Shooping( )
        {
            return View();
        }
        
    }
}
