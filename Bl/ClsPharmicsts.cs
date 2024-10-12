using Microsoft.EntityFrameworkCore;
using Dominos.Models;
using Dominos.Bl;
using static System.Net.Mime.MediaTypeNames;

namespace Dominos.Bl
{
    public interface IPharmicsts
    {
        public List<PharmcistsModel> GetAll();
        public PharmcistsModel GetById(int id);
        public bool Save(PharmcistsModel pharmcsts);
        public bool Delete(int id);
    }

    public class ClsPharmicsts : IPharmicsts
	{
        PharmacyContext context;
        public ClsPharmicsts(PharmacyContext ctx)
        {
            context = ctx;
        }
        // should be use curentState = 1; becouse show ele result = 1 ,
        // but the curentState result 0 it's deleted  
        public List<PharmcistsModel> GetAll()
        {
            try
            {
                var lstCategories = context.TbPharmcists.Where(a => a.CurrentState == 1).ToList();
                return lstCategories;
            }
            catch
            {
                return new List<PharmcistsModel>();
            }
        }
        public PharmcistsModel GetById(int id)
        {
            try
            {
                var category = context.TbPharmcists.FirstOrDefault(a => a.PharmcistId == id && a.CurrentState == 1);
                return category;
            }
            catch
            {
                var products = new PharmcistsModel();
                return products;
            }
        }

        public bool Save(PharmcistsModel products)
        {
            try
            {
                // category new
                if (products.PharmcistId == 0)
                {
                    // complute the category not complute in veiw 
                    products.CurrentState = 1;
                    products.CreatedBy = "1";
                    products.UpdatedBy = "Admin";
                    products.UpdatedDate = DateTime.Now;
                    context.TbPharmcists.Add(products);
                }
                else // edit existe category
                {
                    products.CurrentState = 1;
                    products.CreatedBy = "1";
                    products.UpdatedBy = "Admin";
                    products.UpdatedDate = DateTime.Now;
                    context.Entry(products).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
                var products = GetById(id);
                products.CurrentState = 0;
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
