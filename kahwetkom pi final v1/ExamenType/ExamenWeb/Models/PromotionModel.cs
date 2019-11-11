using Domain.Entities;
using Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExamenWeb.Models
{
    public class PromotionModel
    {
        [Key]
        public int IdPromotion { get; set; }
        public virtual Produit Products { get; set; }
        public int IdProduit { get; set; }
        public float NewPrice { get; set; }
        public float Remise { get; set; }
        public DateTime DateFin { get; set; }
        public IEnumerable<Produit> Prods { get; set; }
        public List<Produit> p { get; set; }
        
        public PromotionModel()
        {
            ProduitService ps = new ProduitService();
      
            Prods = ps.GetAll().ToList();

        }
    }

}