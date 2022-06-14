using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelsBooking.Views
{
    interface IRezervariView
    {
        //Properties - Fields
        string RezervariId { get; set; }
        string RezervariName { get; set; }

        string SearchValue { get; set; }
        bool IsEdit { get; set; }
        bool IsSuccessful { get; set; }
        string Message { get; set; }

        //Events
        event EventHandler SearchEvent;
        event EventHandler AddNewEvent;
        event EventHandler DeleteEvent;
        event EventHandler CancelEvent;

        //Methods
        void SetRezervariListBindingSource(BindingSource rezervariList);
        void Show();
    }
}
