using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelsBooking.Models
{
    interface ILoginRepository
    {
        IEnumerable<LoginModel> GetAll();
        IEnumerable<LoginModel> GetByValue(string value);
    }
}
