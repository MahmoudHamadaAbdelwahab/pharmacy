using Microsoft.AspNetCore.Mvc;
using Dominos.Bl;

namespace Pharmacy_Website.Areas.admin.Controllers
{
    [Area("admin")]
    public class CustomersController : Controller
    {
        Icustomer _cust;
        public CustomersController(Icustomer cust){ 
            _cust = cust;
        }
        public IActionResult List()
        {
            ViewBag.lstCustomer = _cust.GetAll();

            var order = _cust.GetAllItemsData(null);
            return View(order);
        }
        public IActionResult Search(int id)
        {
            ViewBag.lstCustomer = _cust.GetAll();
            var customer = _cust.GetAllItemsData(id);

            return View("List", customer);
        }
    }
}
