using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.MyConventions
{
    class PromotionConv : EntityTypeConfiguration<Promotion>
    {
        public PromotionConv()
        {

          HasOptional(c => c.Products).WithMany(c => c.Promotions).HasForeignKey(p => p.IdProduit).WillCascadeOnDelete(false);
        }
    }
}
