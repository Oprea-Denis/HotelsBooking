using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace HotelsBooking.Models
{
    class LoginModel
    {
      public static bool LoginUser(string userName, string password)
        {
            using (var db = new Model.UsersModel())
            {
                var rec = db.Users.Where(a => a.Username == userName && a.Password == password).FirstOrDefault();
                if (rec != null)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
