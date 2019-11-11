using Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamenWeb.Models
{
    public class NewslettreModels
    {
        [Key]
        public int IdNewslettre { get; set; }
        public virtual User Userr { get; set; }
        public int? IdUser { get; set; }
        public String MailUser { get; set; }
        public int PhoneUser { get; set; }
        public bool status { get; set; }
    }
}