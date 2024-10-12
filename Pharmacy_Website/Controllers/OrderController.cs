using Microsoft.AspNetCore.Mvc;
using Pharmacy_Website.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Bl;
using Dominos.Bl;
using Microsoft.AspNetCore.Http;
namespace Pharmacy_Website.Controllers
{
    public class OrderController : Controller
    {
        IProducts ProdService;
        public OrderController(IProducts service) {
            ProdService = service;
        }

        public IActionResult Cart()
        {
            // convert data inside session from string to object 
            // HttpContext.Session.GetString("Cart")
            string sessionCart = string.Empty;
            if (HttpContext.Request.Cookies["Cart"] != null)
                sessionCart = HttpContext.Request.Cookies["Cart"];

            var cart = JsonConvert.DeserializeObject<ShoppingCart>(sessionCart);
            return View(cart);
        }

        public IActionResult MyOrders() { 
         return View();
        }
        public IActionResult AddToCart(int itemId)
        {
            ShoppingCart cart;
            //if (HttpContext.Session.GetString("Cart") != null)
                    //cart = JsonConvert.DeserializeObject<ShoppingCart>(HttpContext.Session.GetString("Cart"));

            if (HttpContext.Request.Cookies["Cart"] != null)  
                cart = JsonConvert.DeserializeObject<ShoppingCart>(HttpContext.Request.Cookies["Cart"]);
            else
                cart = new ShoppingCart();

            var item = ProdService.GetById(itemId);

            // This item has already been added. Quantity 1 has been added. If there is no item added to the cart
            var itemInList = cart.lstProducts.Where(a => a.ProductId == itemId).FirstOrDefault();
            if (itemInList != null)
            {
                itemInList.Qty++;
                itemInList.Total = itemInList.Qty * itemInList.Price;
            }
            else
            {
                cart.lstProducts.Add(new ShoppingCartProduct
                {
                    ProductId = itemId,
                    ProductName = item.ProductName,
                    Price = item.SalesPrice,
                    Qty = 1,
                    Total = item.SalesPrice
                });
            }
            // convert data from object to string data 
            cart.TotalAllProd = cart.lstProducts.Sum(a => a.Total);
            // JsonConvert.SerializeObject(cart) , it's fron NewtonSoft.json
            //HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
            HttpContext.Response.Cookies.Append("Cart", JsonConvert.SerializeObject(cart));
            return RedirectToAction("Cart");
        }

        // He prevents access to this page unless he is registered
        [Authorize]
        public ActionResult OrderSuccess()
        {
            //string sesstionCart = string.Empty;
            //if (HttpContext.Request.Cookies["Cart"] != null)
            //    sesstionCart = HttpContext.Request.Cookies["Cart"];
            //var cart = JsonConvert.DeserializeObject<ShoppingCart>(sesstionCart);
            //await SaveOrder(cart);

            return View();
        }

        //public IActionResult AddCart(int itemId)
        //{
        //    ShoppingCart cart;

        //    if (HttpContext.Request.Cookies["Cart"] != null)
        //        cart = JsonConvert.DeserializeObject<ShoppingCart>(HttpContext.Request.Cookies["Cart"]);
        //    else
        //        cart = new ShoppingCart();

        //    var item = ProdService.getById(itemId);

        //    var itemInList = cart.lstProducts.Where(a => a.ItemId == itemId).FirstOrDefault();

        //    if (itemInList != null)
        //    {
        //        itemInList.Qty++;
        //        itemInList.Total = itemInList.Qty * itemInList.Price;
        //    }
        //    else
        //    {
        //        cart.lstProducts.Add(new ShoppingCartItem
        //        {
        //            ItemId = item.ItemId,
        //            ItemName = item.ItemName,
        //            Price = item.SalesPrice,
        //            Qty = 1,
        //            Total = item.SalesPrice
        //        });
        //    }
        //    cart.Total = cart.lstProducts.Sum(a => a.Total);

        //    HttpContext.Response.Cookies.Append("Cart", JsonConvert.SerializeObject(cart));

        //    return RedirectToAction("Cart");
        //}

        //async Task SaveOrder(ShoppingCart oShopingCart)
        //{
        //    try
        //    {
        //        List<TbSalesInvoiceItem> lstInvoiceItems = new List<TbSalesInvoiceItem>();
        //        foreach (var item in oShopingCart.lstProducts)
        //        {
        //            lstInvoiceItems.Add(new TbSalesInvoiceItem()
        //            {
        //                ItemId = item.ItemId,
        //                Qty = item.Qty,
        //                InvoicePrice = item.Price
        //            });
        //        }

        //        var user = await _userManage.GetUserAsync(User);

        //        TbSalesInvoice oSalesInvoice = new TbSalesInvoice()
        //        {
        //            InvoiceDate = DateTime.Now,
        //            CustomerId = Guid.Parse(user.Id),
        //            DelivryDate = DateTime.Now.AddDays(5),
        //            CreatedBy = user.Id,
        //            CreatedDate = DateTime.Now
        //        };

        //        _salesInvoice.Save(oSalesInvoice, lstInvoiceItems, true);
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
    }
}
