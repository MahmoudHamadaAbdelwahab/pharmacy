using Microsoft.AspNetCore.Mvc;
using Dominos.Models;
using Dominos.Bl;

namespace Pharmacy_Website.Controllers
{
    public class DrugClassificationsController : Controller
    {
        IClassification _Class;
        public DrugClassificationsController(IClassification cont) {
            _Class = cont;
        }
        public IActionResult Index()
        {
            var classifications = _Class.GetAll();
            return View(classifications);
        }
    }
}
