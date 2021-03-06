using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace HotelsBooking.Models
{
    public class HotelsModel
    {

        private int id;
        private string name;

        //Properties - Validations
        [DisplayName("ID")]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Hotels name is requerid")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Hotels name must be between 3 and 50 characters")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

    }
}
