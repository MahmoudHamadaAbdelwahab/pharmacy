using Dominos.Models;
using Microsoft.EntityFrameworkCore;

namespace Dominos.Bl
{
    public interface IClassification
	{
        public List<ProdcutsClassificationModel> GetAll();
        public ProdcutsClassificationModel GetById(int id);
        public bool Save(ProdcutsClassificationModel category);
        public bool Delete(int id);
    }
    public class ClsClassification : IClassification
	{
        PharmacyContext context;
        public ClsClassification(PharmacyContext ctx)
        {
            context = ctx;
        }
        // should be use curentState = 1; becouse show ele result = 1 ,
        // but the curentState result 0 it's deleted  
        public List<ProdcutsClassificationModel> GetAll()
        {
            try
            {
                var lstCategories = context.TbProdcutsClassification.Where(a => a.CurrentState == 1).ToList();
                return lstCategories;
            }
            catch
            {
                return new List<ProdcutsClassificationModel>();
            }
        }

        public ProdcutsClassificationModel GetById(int id)
        {
            try
            {
                var category = context.TbProdcutsClassification.FirstOrDefault(a => a.ProdcutsClassificationId == id && a.CurrentState == 1);
                return category;
            }
            catch
            {
                var customer = new ProdcutsClassificationModel();
                return customer;
            }
        }

        public bool Save(ProdcutsClassificationModel customer)
        {
            try
            {
                // category new
                if (customer.ProdcutsClassificationId == 0)
                {
                    // complute the category not complute in veiw 
                    customer.CurrentState = 1;
                    customer.CreatedBy = "1";
                    customer.UpdatedDate = DateTime.Now;
                    context.TbProdcutsClassification.Add(customer);
                }
                else // edit existe category
                {
                    customer.CurrentState = 1;
                    customer.CreatedBy = "1";
                    customer.UpdatedDate = DateTime.Now;
                    context.Entry(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
                var customer = GetById(id);
                customer.CurrentState = 0;
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
