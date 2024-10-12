using Dominos.Models;

namespace Dominos.Bl
{
    public interface ISettings
    {
        public SettingsModel GetAll();
        public bool Save(SettingsModel setting);

    }
	
    public class ClsSettings : ISettings
    {
        PharmacyContext context;
        public ClsSettings(PharmacyContext ctx)
        {
            context = ctx;
        }
        // should be use curentState = 1; becouse show ele result = 1 ,
        // but the curentState result 0 it's deleted  
        public SettingsModel GetAll()
        {
            try
            {
                var lstSettings = context.TbSettings.FirstOrDefault();
                return lstSettings;
            }
            catch
            {
                return new SettingsModel();
            }
        }

        public bool Save(SettingsModel suppliers)
        {
            try
            {
                // setting new
                if (suppliers.SettingsId == 0)
                {
                    // complute the setting not complute in veiw 
                    context.TbSettings.Add(suppliers);
                }
                else // edit existe setting
                {
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

    }
}
