using Dominos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dominos.Models;
using Dominos.Bl;
using Pharmacy_Website.Models;

namespace Pharmacy_Website.Areas.admin.Controllers
{
    [Area("admin")]
    public class DrugClassificationsController : Controller
    {
        IClassification _classification;
        public DrugClassificationsController(IClassification classific) {
            _classification = classific;
        }
        public IActionResult List()
        {
            return View(_classification.GetAll());
        }

        public IActionResult Edit(int ? drugId)
        {
            var pharmcist = new ProdcutsClassificationModel();
            if (drugId != null)
                pharmcist = _classification.GetById(Convert.ToInt32(drugId));

            return View(pharmcist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(ProdcutsClassificationModel model)
        {
            if (!ModelState.IsValid)
                return View("Edit", model); // Return to the edit view with validation messages

            _classification.Save(model);
            return RedirectToAction("List");
        }

        public IActionResult Delete(int drugId)
        {
            _classification.Delete(drugId);
            return RedirectToAction("List");
        }
    }
}
