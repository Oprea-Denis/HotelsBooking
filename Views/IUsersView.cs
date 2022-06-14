using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelsBooking.Views
{
      public interface IUsersView
        {
            //Properties - Fields
            string UsersId { get; set; }
            string UsersName { get; set; }
            string UsersPassword { get; set; }
            string UsersRole { get; set; }

            string SearchValue { get; set; }
            bool IsEdit { get; set; }
            bool IsSuccessful { get; set; }
            string Message { get; set; }

            //Events
            event EventHandler SearchEvent;
            event EventHandler AddNewEvent;
            event EventHandler EditEvent;
            event EventHandler DeleteEvent;
            event EventHandler SaveEvent;
            event EventHandler CancelEvent;

            //Methods
            void SetUsersListBindingSource(BindingSource usersList);
            void Show();//Optional

        }
}
