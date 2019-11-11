using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Newslettre
    {
        [Key]
        public int IdNewslettre { get; set; }
        public virtual User Userr { get; set; }
        public int? IdUser { get; set; }
        [Required(ErrorMessage = "Email is required"), EmailAddress]
        public String MailUser { get; set; }
        [RegularExpression(@"^[0-9]{8,11}$", ErrorMessage = "Not a valid phone number, must be between 8 and 11 digits")]
        public int PhoneUser { get; set; }
        public bool status { get; set; }
    }
}
