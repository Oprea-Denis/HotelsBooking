using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelsBooking.Models;
using HotelsBooking.Views;

namespace HotelsBooking.Presenters
{
    class RezervariPresenter
    {
        //Fields
        private IRezervariView view;
        private IRezervariRepository repository;
        private BindingSource rezervareBindingSource;
        private IEnumerable<RezervariModel> rezervariList;

        //Constructor
        public RezervariPresenter(IRezervariView view, IRezervariRepository repository)
        {
            this.rezervareBindingSource = new BindingSource();
            this.view = view;
            this.repository = repository;
            //Subscribe event handler methods to view events
            this.view.SearchEvent += SearchRezervari;
            this.view.AddNewEvent += AddNewRezervari;
            this.view.DeleteEvent += DeleteSelectedRezervari;
            this.view.CancelEvent += CancelAction;
            //Set Hotels bindind source
            this.view.SetRezervariListBindingSource(rezervareBindingSource);
            //Load Hotels list view
            LoadAllRezervariList();
            //Show view
            this.view.Show();
        }

        //Methods
        private void LoadAllRezervariList()
        {
            rezervariList = repository.GetAll();
            rezervareBindingSource.DataSource = rezervariList;//Set data source.
        }
        private void SearchRezervari(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchValue);
            if (emptyValue == false)
                rezervariList = repository.GetByValue(this.view.SearchValue);
            else rezervariList = repository.GetAll();
            rezervareBindingSource.DataSource = rezervariList;
        }
        private void AddNewRezervari(object sender, EventArgs e)
        {
            view.IsEdit = false;
        }
        private void LoadSelectedRezervariToEdit(object sender, EventArgs e)
        {
            var rezervari = (RezervariModel)rezervareBindingSource.Current;
            view.RezervariId = rezervari.Id.ToString();
            view.RezervariName = rezervari.Name;
            view.IsEdit = true;
        }
        //private void SaveRezervari(object sender, EventArgs e)
        //{
        //    var model = new RezervariModel();
        //    model.Id = Convert.ToInt32(view.RezervariId);
        //    model.Name = view.RezervariName;
        //    try
        //    {
        //        new Common.ModelDataValidation().Validate(model);
        //        if (view.IsEdit)//Edit model
        //        {
        //            repository.Edit(model);
        //            view.Message = "Rezervare edited successfuly";
        //        }
        //        else //Add new model
        //        {
        //            repository.Add(model);
        //            view.Message = "Rezervare added sucessfully";
        //        }
        //        view.IsSuccessful = true;
        //        LoadAllRezervariList();
        //        CleanviewFields();
        //    }
        //    catch (Exception ex)
        //    {
        //        view.IsSuccessful = false;
        //        view.Message = ex.Message;
        //    }
        //}
        private void CleanviewFields()
        {
            view.RezervariId = "0";
            view.RezervariName = "";
        }

        private void CancelAction(object sender, EventArgs e)
        {
            CleanviewFields();

        }
        private void DeleteSelectedRezervari(object sender, EventArgs e)
        {
            try
            {
                var rezervari = (RezervariModel)rezervareBindingSource.Current;
                repository.Delete(rezervari.Id);
                view.IsSuccessful = true;
                view.Message = "Rezervare deleted successfully";
                LoadAllRezervariList();
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = "An error ocurred, could not delete hotel";
            }
        }
    }
}
