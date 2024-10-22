using Microsoft.EntityFrameworkCore;
using Dominos.Models;

namespace Dominos.Bl
{
    public interface IProducts
    {
        public List<ProductsModel> GetAll();
        public List<ProductsModel> GetAllWithImages();
        public VwProducts GetAllItemsData(int cateId);
        public List<VwProducts> GetAllProducts();

        public ProductsModel GetById(int id);
        public ProductsModel GetByIdWithImages(int id);
        public bool Save(ProductsModel category);
        public bool Delete(int id);
    }

    public class ClsProducts : IProducts
	{
        PharmacyContext context;
        public ClsProducts(PharmacyContext ctx)
        {
            context = ctx;
        }
        // should be use curentState = 1; becouse show ele result = 1 ,
        // but the curentState result 0 it's deleted  
        public List<ProductsModel> GetAll()
        {
            try
            {
                var lstCategories = context.TbProducts.Where(a => a.CurrentState == 1).ToList();
     
                return lstCategories;
            }
            catch
            {
                return new List<ProductsModel>();
            }
        }
        public List<ProductsModel> GetAllWithImages()
        {
            try
            {
                var lstProduct = context.TbProducts
                           .Include(p => p.TbImages) // Include related images
                           .Where(a => a.CurrentState == 1 && a.TbImages.Any()) // Check if any images exist
                           .ToList();

                return lstProduct;
            }
            catch
            {
                return new List<ProductsModel>();
            }
        }
        
        public VwProducts GetAllItemsData(int productId) // VwItemCategory   , int? itemId
        {
            try
            {
                // var lstItems = context.VmItemProducts.Where(a => (a.PharmcistId == pharmId && pharmId == null && pharmId == 0)
                // && a.CurrentState == 1 ).OrderByDescending(a => a.CreatedDate).ToList();
                var lstItems = context.VmProductsImages.FirstOrDefault(a => a.ProductId == productId);
                return lstItems;
            }
            catch
            {
                return new VwProducts();
            }
        }

        public List<VwProducts> GetAllProducts()
        {
            try
            {
                var lstProducst = context.VmProductsImages.ToList();
                return lstProducst;
            }
            catch
            {
                return new List<VwProducts>();
            }
        }

        public ProductsModel GetById(int id)
        {
            try {
               var category = context.TbProducts.FirstOrDefault(a => a.ProductId == id && a.CurrentState == 1);
                return category;
            }
            catch {
                var products = new ProductsModel();
                return products;
            }
        }

        public ProductsModel GetByIdWithImages(int id){
            try
            {
                var products = context.TbProducts.Include(p => p.TbImages)
                    .Where(a => a.CurrentState == 1 && a.TbImages.Any())
                    .FirstOrDefault(p => p.ProductId == id);
                return products;
            }
            catch
            {
                var products = new ProductsModel();
                return products;
            }
        }

        public bool Save(ProductsModel products)
        {
            try {
                // category new
                if (products.ProductId == 0)
                {
                    // complute the category not complute in veiw 
                    products.CurrentState = 1;
                    products.UpdatedBy = "admin";
                    products.CreatedBy = "admin";
                    products.UpdatedDate = DateTime.Now;
                    products.CreatedDate = DateTime.Now;
                    context.TbProducts.Add(products);
                }
                else // Updating Existing Product
                {
                    var existingProduct = context.TbProducts.FirstOrDefault(p => p.ProductId == products.ProductId);
                    if (existingProduct != null)
                    {
                        // Manually update properties (or use automapper if available)
                        existingProduct.ProductName = products.ProductName;
                        existingProduct.Description = products.Description;
                        existingProduct.SalesPrice = products.SalesPrice;
                        existingProduct.PurchasePrice = products.PurchasePrice;
                        existingProduct.StockQuentity = products.StockQuentity;
                        existingProduct.SupplierId = products.SupplierId;
                        existingProduct.PharmcistId = products.PharmcistId;
                        existingProduct.ProdcutsClassificationId = products.ProdcutsClassificationId;

                        existingProduct.CurrentState = 1;
                        existingProduct.UpdatedBy = "admin";
                        existingProduct.CreatedBy = "admin";
                        existingProduct.UpdatedDate = DateTime.Now;
                        existingProduct.CreatedDate = DateTime.Now;
                        context.TbProducts.Update(existingProduct);
                    }
                }
                // Save the changes to the database
                context.SaveChanges();
                return true;
            }
            catch {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try { 
                var products = GetByIdWithImages(id);
                products.CurrentState = 0;
                context.SaveChanges();
                return true;
            }catch { 
                return false;
            }
        }
        
    }
}
