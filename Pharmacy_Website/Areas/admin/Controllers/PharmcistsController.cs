using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dominos.Bl;
using Pharmacy_Website.Models;
using Dominos.Models;
using Microsoft.AspNetCore.Authorization;

namespace Pharmacy_Website.Areas.admin.Controllers
{
    [Authorize(Roles = "Admin , Pharmcists")]

    [Area("admin")]
    public class PharmcistsController : Controller
    {

        IPharmicsts _pharm;
        public PharmcistsController(IPharmicsts pharm) {
            _pharm = pharm;
        }

        [AllowAnonymous]
        public IActionResult List()
        {
            return View(_pharm.GetAll());
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int ? pharmcistId )
        {
            var pharmcist = new PharmcistsModel();
            if (pharmcistId != null)
                pharmcist = _pharm.GetById(Convert.ToInt32(pharmcistId));

            return View(pharmcist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(PharmcistsModel model)
        {
            if (!ModelState.IsValid)
                return View("Edit", model); // Return to the edit view with validation messages
            model.Role = "manager";
            _pharm.Save(model);
            return RedirectToAction("List");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int pharmcistId){
            _pharm.Delete(pharmcistId);
            return RedirectToAction("List");
        }
    }
}
