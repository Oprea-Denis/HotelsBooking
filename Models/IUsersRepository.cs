using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelsBooking.Models
{       public interface IUsersRepository
        {
            void Add(UsersModel usersModel);
            void Edit(UsersModel usersModel);
            void Delete(int id);
            IEnumerable<UsersModel> GetAll();
            IEnumerable<UsersModel> GetByValue(string value);//Searchs
        }
}
