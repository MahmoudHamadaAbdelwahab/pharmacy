using Dominos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dominos.Bl;
using Pharmacy_Website.Models;
using Microsoft.AspNetCore.Authorization;

namespace Pharmacy_Website.Areas.admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("admin")]
    public class SuppliersController : Controller
    {
        IProducts _prod;
        ISupplier _suppliers;
        public SuppliersController(ISupplier supp , IProducts prod) {
            _suppliers = supp; _prod = prod;
        }
        [AllowAnonymous]
        public IActionResult List()
        {
            return View(_suppliers.GetAll());
        }

        public IActionResult Edit(int ? supplierId)
        {
            var pharmcist = new SupplierModel();
            ViewBag.ClsProducts = _prod.GetAll();
            if (supplierId != null)
                pharmcist = _suppliers.GetById(Convert.ToInt32(supplierId));

            return View(pharmcist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(SupplierModel model)
        {
            if (!ModelState.IsValid)
                return View("Edit", model); // Return to the edit view with validation messages

            _suppliers.Save(model);
            return RedirectToAction("List");
        }

        public IActionResult Delete(int supplierId)
        {
            _suppliers.Delete(supplierId);
            return RedirectToAction("List");
        }
    }
}
