using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelsBooking.Views
{
    public partial class MainView : Form, IMainView
    {
        public MainView()
        {
            InitializeComponent();
            btnHotel.Click += delegate { ShowHotelsView?.Invoke(this, EventArgs.Empty); };
            btnUser.Click += delegate { ShowUsersView?.Invoke(this, EventArgs.Empty); };
            btnRezervari.Click += delegate { ShowRezervariView?.Invoke(this, EventArgs.Empty); };

        }

        public event EventHandler ShowHotelsView;
        public event EventHandler ShowUsersView;
        public event EventHandler ShowRezervariView;
    }
}
