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
    interface ILoginView
    {
        event EventHandler ShowMainView;
    }
}
