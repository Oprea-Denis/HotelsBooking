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
    class HotelsPresenter
    {
        //Fields
        private IHotelsView view;
        private IHotelsRepository repository;
        private BindingSource hotelBindingSource;
        private IEnumerable<HotelsModel> hotelsList;

        //Constructor
        public HotelsPresenter(IHotelsView view, IHotelsRepository repository)
        {
            this.hotelBindingSource = new BindingSource();
            this.view = view;
            this.repository = repository;
            //Subscribe event handler methods to view events
            this.view.SearchEvent += SearchHotels;
            this.view.AddNewEvent += AddNewHotels;
            //this.view.RezervareNewEvent += RezervareNewHotels;
            this.view.EditEvent += LoadSelectedHotelsToEdit;
            this.view.DeleteEvent += DeleteSelectedHotels;
            this.view.SaveEvent += SaveHotels;
            this.view.CancelEvent += CancelAction;
            //Set Hotels bindind source
            this.view.SetHotelsListBindingSource(hotelBindingSource);
            //Load Hotels list view
            LoadAllHotelsList();
            //Show view
            this.view.Show();
        }

        //Methods
        private void LoadAllHotelsList()
        {
            hotelsList = repository.GetAll();
            hotelBindingSource.DataSource = hotelsList;//Set data source.
        }
        private void SearchHotels(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchValue);
            if (emptyValue == false)
                hotelsList = repository.GetByValue(this.view.SearchValue);
            else hotelsList = repository.GetAll();
            hotelBindingSource.DataSource = hotelsList;
        }
        private void AddNewHotels(object sender, EventArgs e)
        {
            view.IsEdit = false;
        }

        //private void RezervareNewHotels(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        var hotels = (HotelsModel)hotelBindingSource.Current;
        //        repository.Rezervare(hotels.Id);
        //        view.IsSuccessful = true;
        //        view.Message = "Hotel deleted successfully";
        //        LoadAllHotelsList();
        //    }
        //    catch (Exception ex)
        //    {
        //        view.IsSuccessful = false;
        //        view.Message = "An error ocurred, could not delete hotel";
        //    }
        //}
        private void LoadSelectedHotelsToEdit(object sender, EventArgs e)
        {
            var hotels = (HotelsModel)hotelBindingSource.Current;
            view.HotelsId = hotels.Id.ToString();
            view.HotelsName = hotels.Name;
            view.IsEdit = true;
        }
        private void SaveHotels(object sender, EventArgs e)
        {
            var model = new HotelsModel();
            model.Id = Convert.ToInt32(view.HotelsId);
            model.Name = view.HotelsName;
            try
            {
                new Common.ModelDataValidation().Validate(model);
                if (view.IsEdit)//Edit model
                {
                    repository.Edit(model);
                    view.Message = "Hotel edited successfuly";
                }
                else //Add new model
                {
                    repository.Add(model);
                    view.Message = "Hotel added sucessfully";
                }
                view.IsSuccessful = true;
                LoadAllHotelsList();
                CleanviewFields();
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = ex.Message;
            }
        }
        private void CleanviewFields()
        {
            view.HotelsId = "0";
            view.HotelsName = "";
        }

        private void CancelAction(object sender, EventArgs e)
        {
            CleanviewFields();

        }
        private void DeleteSelectedHotels(object sender, EventArgs e)
        {
            try
            {
                var hotels = (HotelsModel)hotelBindingSource.Current;
                repository.Delete(hotels.Id);
                view.IsSuccessful = true;
                view.Message = "Hotel deleted successfully";
                LoadAllHotelsList();
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = "An error ocurred, could not delete hotel";
            }
        }
    }
}
