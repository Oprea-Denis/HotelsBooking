using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelsBooking.Views;
using HotelsBooking.Models;
using HotelsBooking._Repositories;
using System.Windows.Forms;

namespace HotelsBooking.Presenters
{
    class MainPresenter
    {
        private IMainView mainView;
        private readonly string sqlConnectionString;

        public MainPresenter(IMainView mainView, string sqlConnectionString)
        {
            this.mainView = mainView;
            this.sqlConnectionString = sqlConnectionString;
            this.mainView.ShowHotelsView += ShowHotelView;
            this.mainView.ShowUsersView += ShowUserView;
            this.mainView.ShowRezervariView += ShowRezervariView;
        }
        private void ShowHotelView(object sender, EventArgs e)
        {
            IHotelsView view = HotelsView.GetInstace((MainView)mainView);
            IHotelsRepository repository = new HotelsRepository(sqlConnectionString);
            new HotelsPresenter(view, repository);
        }
        private void ShowUserView(object sender, EventArgs e)
        {
            IUsersView view = UsersView.GetInstace((MainView)mainView);
            IUsersRepository repository = new UsersRepository(sqlConnectionString);
            new UsersPresenter(view, repository);
        }
        private void ShowRezervariView(object sender, EventArgs e)
        {
            IRezervariView view = RezervariView.GetInstace((MainView)mainView);
            IRezervariRepository repository = new RezervariRepository(sqlConnectionString);
            new RezervariPresenter(view, repository);
        }

    }
}
