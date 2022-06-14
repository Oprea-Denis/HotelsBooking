using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace HotelsBooking.Models
{
        public class UsersModel
        {
            //Fields
            private int id;
            private string name;
            private string password;
            private string role;

            //Properties - Validations
            [DisplayName("ID")]
            public int Id
            {
                get { return id; }
                set { id = value; }
            }

            [DisplayName("Name")]
            [Required(ErrorMessage = "Users name is requerid")]
            [StringLength(50, MinimumLength = 3, ErrorMessage = "Users name must be between 3 and 50 characters")]
            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            [DisplayName("Password")]
            [Required(ErrorMessage = "Users password is requerid")]
            [StringLength(50, MinimumLength = 3, ErrorMessage = "Users password must be between 3 and 50 characters")]
            public string Password
            {
                get { return password; }
                set { password = value; }
            }

            [DisplayName("Role")]
            [Required(ErrorMessage = "Users role is requerid")]
            [StringLength(50, MinimumLength = 3, ErrorMessage = "Users role must be between 3 and 50 characters")]
            public string Role
            {
                get { return role; }
                set { role = value; }
            }
        }
}
