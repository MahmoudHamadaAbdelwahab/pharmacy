using Dominos.Models;
namespace Dominos.Bl
{
    public interface ISupplier
    {
        public List<SupplierModel> GetAll();
        public SupplierModel GetById(int id);
        public bool Save(SupplierModel category);
        public bool Delete(int id);
    }
	
    public class ClsSuppliers : ISupplier
	{
        PharmacyContext context;
        public ClsSuppliers(PharmacyContext ctx)
        {
            context = ctx;
        }
        // should be use curentState = 1; becouse show ele result = 1 ,
        // but the curentState result 0 it's deleted  
        public List<SupplierModel> GetAll()
        {
            try
            {
                var lstCategories = context.TbSupplier.Where(a => a.CurrentState == 1).ToList();
                return lstCategories;
            }
            catch
            {
                return new List<SupplierModel>();
            }
        }

        public SupplierModel GetById(int id)
        {
            try
            {
                var category = context.TbSupplier.FirstOrDefault(a => a.SupplierId == id && a.CurrentState == 1);
                return category;
            }
            catch
            {
                var suppliers = new SupplierModel();
                return suppliers;
            }
        }

        public bool Save(SupplierModel suppliers)
        {
            try
            {
                // category new
                if (suppliers.SupplierId == 0)
                {
                    // complute the category not complute in veiw 
                    suppliers.CurrentState = 1;
                    suppliers.CreatedDate = DateTime.Now;
                    suppliers.CreatedBy = "1";
                    suppliers.UpdatedBy = "Admin";  
                    suppliers.UpdatedDate = DateTime.Now;
                    context.TbSupplier.Add(suppliers);
                }
                else // edit existe category
                {
                    suppliers.CurrentState = 1;
                    suppliers.CreatedBy = "1";
                    suppliers.UpdatedBy = "Admin";
                    suppliers.CreatedDate = DateTime.Now;
                    suppliers.UpdatedDate = DateTime.Now;
                    context.Entry(suppliers).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var suppliers = GetById(id);
                suppliers.CurrentState = 0;
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
