using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Offre
    {
        [Key]
        public int IdOffer { get; set; }

        [NotMapped]
        public string idProducts { get; set; }
        public Produit Products { get; set; }
        public Produit Produit { get; set; }
        [MinLength(8)]
        public String Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateDebut { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateFin { get; set; }

    }
}
