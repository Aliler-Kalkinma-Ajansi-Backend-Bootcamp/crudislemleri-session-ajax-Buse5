using LoginData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoginData.Services
{
    public class KampBilgiServices
    {
        public List<Models.KampAlanBilgi>GetAll(bool IsActive=false) 
        {
            List<Models.KampAlanBilgi> result = new List<Models.KampAlanBilgi>();
            using (var srv = new KampContext()) 
            {
                var data = srv.KampAlanBilgis.Where(w => w.Id > 0);
                if (IsActive) 
                {
                    data = data.Where(w=> (bool)w.IsActive);
                }
                result = data.ToList();
            }
            return result;
        }
    }
}
