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
        PharmacyContext _context;
       ISupplier _ISupplier;
       IPharmicsts _IPharmcists;
       IClassification _IProdClassification;
        IProducts _IProd;
        public StoreController(PharmacyContext context , IProducts prod,ISupplier supp , IPharmicsts pharmicst ,
           IClassification classifi )
        {
            _context = context;
            _IProd = prod;
            _IProdClassification = classifi;
            _ISupplier = supp;
            _IPharmcists = pharmicst;
        }

        //public IActionResult Index()
        //{
        //    return View(_IProd.GetAllWithImages());
        //}

        // This action handles the filtering
        public IActionResult Index(SearchModel searchModel)
        {
            // Get all products initially
            var products = _context.TbProducts.Include(p => p.TbSupplier)
                                            .Include(p => p.TbPharmcists)
                                            .Include(p => p.TbProdcutsClassification)
                                            .Include(p => p.TbImages)
                                            .AsQueryable();

            // Apply filters based on user input
            if (!string.IsNullOrEmpty(searchModel.ProductName))
            {
                products = products.Where(p => p.ProductName.Contains(searchModel.ProductName));
            }

            if (searchModel.ProdcutsClassificationId.HasValue)
            {
                products = products.Where(p => p.ProdcutsClassificationId == searchModel.ProdcutsClassificationId);
            }

            if (searchModel.SupplierId.HasValue)
            {
                products = products.Where(p => p.SupplierId == searchModel.SupplierId);
            }

            if (searchModel.PharmcistId.HasValue)
            {
                products = products.Where(p => p.PharmcistId == searchModel.PharmcistId);
            }

            // Populate filter dropdowns
            searchModel.ProductClassifications = new SelectList(_context.TbProdcutsClassification, "ProdcutsClassificationId", "ProdcutsClassificationName");
            searchModel.Suppliers = new SelectList(_context.TbSupplier, "SupplierId", "SupplierName");
            searchModel.Pharmcists = new SelectList(_context.TbPharmcists, "PharmcistId", "PharmcistName");

            // Pass the filtered products and search model to the view
            ViewBag.Products = products.ToList();
            return View(searchModel);
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
