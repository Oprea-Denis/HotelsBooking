using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelsBooking.Views;
using HotelsBooking.Models;
using HotelsBooking._Repositories;
using System.Windows.Forms;

namespace HotelsBooking.Views
{
    public interface IMainView
    {
        event EventHandler ShowHotelsView;
        event EventHandler ShowUsersView;
        event EventHandler ShowRezervariView;
    }
}
