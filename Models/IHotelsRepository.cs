using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelsBooking.Models
{
    interface IHotelsRepository
    {
        void Add(HotelsModel hotelsModel);
        //void Rezervare(HotelsModel hotelsModel);
        void Edit(HotelsModel hotelsModel);
        void Delete(int id);
        IEnumerable<HotelsModel> GetAll();
        IEnumerable<HotelsModel> GetByValue(string value);

    }
}
