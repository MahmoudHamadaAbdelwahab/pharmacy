using Microsoft.AspNetCore.Mvc;
using Dominos.Bl;
using Dominos.Models;
using Microsoft.AspNetCore.Authorization;

namespace Pharmacy_Website.Areas.admin.Controllers
{
    [Authorize]
   
    [Area("admin")] 
    public class ProductsController : Controller
    {
        PharmacyContext context;
        IProducts _IProd;
        IClassification _class;
        IPharmicsts _pharm;
        ISupplier _supp;
        public ProductsController(PharmacyContext con , IProducts pro, IClassification classifi, IPharmicsts pharm, ISupplier supp)
        {
            context = con;
            _IProd = pro; _pharm = pharm;
            _supp = supp; _class = classifi;
        }

        [AllowAnonymous]
        public async Task<IActionResult> List()
        {
            return View(_IProd.GetAllWithImages());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(ProductsModel product, List<IFormFile> uploadedImages)
        {
            try
            {
                // Check if model is valid
                if (!ModelState.IsValid)
                {
                    // Re-populate necessary data for dropdowns, etc.
                    ViewBag.lstPharmicts = _pharm.GetAll();
                    ViewBag.lstSuppliers = _supp.GetAll();
                    ViewBag.lstClassific = _class.GetAll();

                    // Return the same form with validation errors
                    return View("Edit", product);
                }
       
                var saveResult = _IProd.Save(product);
                if (!saveResult)
                {
                    ModelState.AddModelError("", "Product could not be saved.");
                    return View("Edit", product);
                }

                // Handle image uploads only after product is saved
                if (uploadedImages != null && uploadedImages.Count > 0)
                {
                    foreach (var image in uploadedImages)
                    {
                        if (image.Length > 0)
                        {
                            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploads/Products");
                            if (!Directory.Exists(uploadsFolder))
                            {
                                Directory.CreateDirectory(uploadsFolder);
                            }

                            var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(image.FileName);
                            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await image.CopyToAsync(fileStream);
                            }

                            // Now save image information in the database
                            var newImage = new ImagesModel
                            {
                                ProductId = product.ProductId,  // Ensure this is saved after the product
                                ImageName = uniqueFileName
                            };
                            context.TbImages.Add(newImage);
                        }
                    }
                    await context.SaveChangesAsync();
                }

                // Redirect after a successful save
                return RedirectToAction(nameof(List));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while saving the product.");
                return View("Edit", product);
            }
        }

        public IActionResult Edit(int? editId)
        {
           var product =  new ProductsModel();
            ViewBag.lstPharmicts = _pharm.GetAll();
            ViewBag.lstSuppliers = _supp.GetAll();
            ViewBag.lstClassific = _class.GetAll();

            if (editId != null)
            {
                product = _IProd.GetByIdWithImages(Convert.ToInt32(editId));
            }

            return View(product);
        }

        public IActionResult Search(int id)
        {
            ViewBag.lstPharmcist = _pharm.GetAll();
            var pharm = _IProd.GetAllItemsData(id);
            return View("List", pharm);
        }
       
        public IActionResult Delete(int deleteId)
        {
             _IProd.Delete(deleteId);

            return RedirectToAction("List");
        }

        private bool ProductExists(int id)
        {
            return context.TbProducts.Any(e => e.ProductId == id);
        }
    }

    //public class productController : Controller
    //{
    //    private readonly PharmacyContext context;

    //    public productController(PharmacyContext context)
    //    {
    //        context = context;
    //    }

    //    // GET: product
    //    public async Task<IActionResult> Index()
    //    {
    //        var product = await context.Tbproduct.Include(p => p.TbImages).ToListAsync();
    //        return View(product);
    //    }

    //    // GET: product/Create
    //    public IActionResult Create()
    //    {
    //        return View();
    //    }

    //    // POST: product/Create
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Create(productModel product, List<IFormFile> uploadedImages)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            // Add product
    //            context.Add(product);
    //            await context.SaveChangesAsync();

    //            // Add images
    //            if (uploadedImages != null)
    //            {
    //                foreach (var image in uploadedImages)
    //                {
    //                    var newImage = new ImagesModel
    //                    {
    //                        ProductId = product.ProductId,
    //                        ImageName = image.FileName // Replace with proper file handling
    //                    };
    //                    context.TbImages.Add(newImage);
    //                }
    //                await context.SaveChangesAsync();
    //            }

    //            return RedirectToAction(nameof(Index));
    //        }
    //        return View(product);
    //    }

    //    // GET: product/Edit/5
    //    public async Task<IActionResult> Edit2(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return NotFound();
    //        }

    //        var product = await context.Tbproduct.Include(p => p.TbImages).FirstOrDefaultAsync(p => p.ProductId == id);
    //        if (product == null)
    //        {
    //            return NotFound();
    //        }
    //        return View(product);
    //    }

    //    // POST: product/Edit/5
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Edit2(int id, productModel product, List<IFormFile> uploadedImages)
    //    {
    //        if (id != product.ProductId)
    //        {
    //            return NotFound();
    //        }

    //        if (ModelState.IsValid)
    //        {
    //            try
    //            {
    //                context.Update(product);
    //                await context.SaveChangesAsync();

    //                // Update images if new ones are uploaded
    //                if (uploadedImages != null)
    //                {
    //                    foreach (var image in uploadedImages)
    //                    {
    //                        var newImage = new ImagesModel
    //                        {
    //                            ProductId = product.ProductId,
    //                            ImageName = image.FileName
    //                        };
    //                        context.TbImages.Add(newImage);
    //                    }
    //                    await context.SaveChangesAsync();
    //                }
    //            }
    //            catch (DbUpdateConcurrencyException)
    //            {
    //                if (!ProductExists(product.ProductId))
    //                {
    //                    return NotFound();
    //                }
    //                else
    //                {
    //                    throw;
    //                }
    //            }
    //            return RedirectToAction(nameof(Index));
    //        }
    //        return View(product);
    //    }

    //    // GET: product/Delete/5
    //    public async Task<IActionResult> Delete(int? id)
    //    {
    //        if (id == null)
    //        {
    //            return NotFound();
    //        }

    //        var product = await context.Tbproduct.Include(p => p.TbImages).FirstOrDefaultAsync(p => p.ProductId == id);
    //        if (product == null)
    //        {
    //            return NotFound();
    //        }

    //        return View(product);
    //    }

    //    // POST: product/Delete/5
    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> DeleteConfirmed(int id)
    //    {
    //        var product = await context.Tbproduct.Include(p => p.TbImages).FirstOrDefaultAsync(p => p.ProductId == id);
    //        if (product != null)
    //        {
    //            // Remove associated images
    //            var images = context.TbImages.Where(i => i.ProductId == id).ToList();
    //            context.TbImages.RemoveRange(images);

    //            // Remove the product
    //            context.Tbproduct.Remove(product);
    //            await context.SaveChangesAsync();
    //        }

    //        return RedirectToAction(nameof(Index));
    //    }

    //    private bool ProductExists(int id)
    //    {
    //        return context.Tbproduct.Any(e => e.ProductId == id);
    //    }
    //}
}
