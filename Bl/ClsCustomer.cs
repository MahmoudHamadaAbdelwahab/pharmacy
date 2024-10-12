using Microsoft.EntityFrameworkCore;
using Dominos.Models;

namespace Dominos.Bl
{
    public interface Icustomer
    {
        public List<CustomerModel> GetAll();
        public List<VmOrder> GetAllItemsData(int? cateId);
        public CustomerModel GetById(int id);
        public bool Save(CustomerModel category);
        public bool Delete(int id);
    }

    public class ClsCustomer : Icustomer
	{
        PharmacyContext context;
        public ClsCustomer(PharmacyContext ctx)
        {
            context = ctx;
        }
        // should be use curentState = 1; becouse show ele result = 1 ,
        // but the curentState result 0 it's deleted  
        public List<CustomerModel> GetAll()
        {
            try
            {
                var lstCategories = context.TbCustomers.Where(a => a.CurrentState == 1).ToList();
                return lstCategories;
            }
            catch
            {
                return new List<CustomerModel>();
            }
        }

        public List<VmOrder> GetAllItemsData(int? customerId) // VwItemCategory   , int? itemId
        {
            try
            {
                // var lstItems = context.VmItemProducts.Where(a => (a.PharmcistId == pharmId && pharmId == null && pharmId == 0)
                // && a.CurrentState == 1 ).OrderByDescending(a => a.CreatedDate).ToList();
                var lstItems = context.VmItemOrder.Where(a => (a.CustomerId == customerId && customerId == null && customerId == 0)
                    && a.CurrentState == 1 && !string.IsNullOrEmpty(a.ProductName))
                   .OrderByDescending(a => a.CreatedDate).ToList();
                return lstItems;
            }
            catch
            {
                return new List<VmOrder>();
            }
        }


        public CustomerModel GetById(int id)
        {
            try
            {
                var category = context.TbCustomers.FirstOrDefault(a => a.CustomerId == id && a.CurrentState == 1);
                return category;
            }
            catch
            {
                var customer = new CustomerModel();
                return customer;
            }
        }

        public bool Save(CustomerModel customer)
        {
            try
            {
                // category new
                if (customer.CustomerId == 0)
                {
                    // complute the category not complute in veiw 
                    customer.CurrentState = 1;
                    customer.CreatedBy = "1";
                    customer.UpdatedDate = DateTime.Now;
                    context.TbCustomers.Add(customer);
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
