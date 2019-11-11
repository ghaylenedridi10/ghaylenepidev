using Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamenWeb.Models
{
    public class OffreModels
    {
        [Key]
        public int IdOffer { get; set; }

        [NotMapped]
        public string idProducts { get; set; }
        public Produit Products { get; set; }
        public Produit Produit { get; set; }
        public String Description { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }

    }
}