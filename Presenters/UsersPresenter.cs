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
    public class UsersPresenter
    {
        //Fields
        private IUsersView view;
        private IUsersRepository repository;
        private BindingSource userBindingSource;
        private IEnumerable<UsersModel> usersList;

        //Constructor
        public UsersPresenter(IUsersView view, IUsersRepository repository)
        {
            this.userBindingSource = new BindingSource();
            this.view = view;
            this.repository = repository;
            //Subscribe event handler methods to view events
            this.view.SearchEvent += SearchUsers;
            this.view.AddNewEvent += AddNewUsers;
            this.view.EditEvent += LoadSelectedUsersToEdit;
            this.view.DeleteEvent += DeleteSelectedUsers;
            this.view.SaveEvent += SaveUsers;
            this.view.CancelEvent += CancelAction;
            //Set Users bindind source
            this.view.SetUsersListBindingSource(userBindingSource);
            //Load Users list view
            LoadAllUsersList();
            //Show view
            this.view.Show();
        }

        //Methods
        private void LoadAllUsersList()
        {
            usersList = repository.GetAll();
            userBindingSource.DataSource = usersList;//Set data source.
        }
        private void SearchUsers(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchValue);
            if (emptyValue == false)
                usersList = repository.GetByValue(this.view.SearchValue);
            else usersList = repository.GetAll();
            userBindingSource.DataSource = usersList;
        }
        private void AddNewUsers(object sender, EventArgs e)
        {
            view.IsEdit = false;
        }
        private void LoadSelectedUsersToEdit(object sender, EventArgs e)
        {
            var users = (UsersModel)userBindingSource.Current;
            view.UsersId = users.Id.ToString();
            view.UsersName = users.Name;
            view.UsersPassword = users.Password;
            view.UsersRole = users.Role;
            view.IsEdit = true;
        }
        private void SaveUsers(object sender, EventArgs e)
        {
            var model = new UsersModel();
            model.Id = Convert.ToInt32(view.UsersId);
            model.Name = view.UsersName;
            model.Password = view.UsersPassword;
            model.Role = view.UsersRole;
            try
            {
                new Common.ModelDataValidation().Validate(model);
                if (view.IsEdit)//Edit model
                {
                    repository.Edit(model);
                    view.Message = "User edited successfuly";
                }
                else //Add new model
                {
                    repository.Add(model);
                    view.Message = "User added sucessfully";
                }
                view.IsSuccessful = true;
                LoadAllUsersList();
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
            view.UsersId = "0";
            view.UsersName = "";
            view.UsersPassword = "";
            view.UsersRole = "";
        }

        private void CancelAction(object sender, EventArgs e)
        {
            CleanviewFields();
        }
        private void DeleteSelectedUsers(object sender, EventArgs e)
        {
            try
            {
                var users = (UsersModel)userBindingSource.Current;
                repository.Delete(users.Id);
                view.IsSuccessful = true;
                view.Message = "User deleted successfully";
                LoadAllUsersList();
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = "An error ocurred, could not delete user";
            }
        }
    }
}
