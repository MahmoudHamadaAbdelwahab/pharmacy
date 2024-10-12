using Dominos.Models;
namespace Bl
{
    public interface ISliders
    {
        public List<SliderModel> GetAll();
        public SliderModel GetById(int id);
        public bool Save(SliderModel os);
        public bool Dekete(int id);
    }
    public class ClsSlider : ISliders
    {
        PharmacyContext context;
        public ClsSlider(PharmacyContext ctx)
        {
            context = ctx;
        }
        public List<SliderModel> GetAll()
        {
            try
            {
                var lstCategories = context.TbSlider.Where(a => a.CurrentState == 1).ToList();
                return lstCategories;
            }
            catch
            {
                return new List<SliderModel>();
            }
        }

        public SliderModel GetById(int id)
        {
            try
            {
                var os = context.TbSlider.FirstOrDefault(a => a.SliderId == id && a.CurrentState == 1);
                return os;
            }
            catch
            {
                return new SliderModel();
            }
        }

        public bool Save(SliderModel os)
        {
            try
            {
                if (os.SliderId == 0)
                {
                    os.CreatedBy = "1";
                    os.CreatedDate = DateTime.Now;
                    context.TbSlider.Add(os);
                }
                else
                {
                    os.UpdatedBy = "1";
                    os.UpdatedDate = DateTime.Now;
                    context.Entry(os).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Dekete(int id)
        {
            try
            {
                var os = GetById(id);
                os.CurrentState = 0;
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
