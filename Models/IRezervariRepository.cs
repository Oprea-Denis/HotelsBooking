using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelsBooking.Models
{
    interface IRezervariRepository
    {
        void Delete(int id);
        IEnumerable<RezervariModel> GetAll();
        IEnumerable<RezervariModel> GetByValue(string value);
    }
}
