using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Domaine.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Paniers
    {
        private Paniers paniers;



        [Key]
        public int IdPanier { get; set; }
        public int Quantite { get; set; }
        public virtual User Userr { get; set; }
        public int? IdUser { get; set; }
        [ForeignKey("IdProduit")]
        public Produit Products { get; set; }
        public int? IdProduit { get; set; }


        public float calculMontant()
        {
            return Quantite * Products.Price;
        }

        
    }
}
