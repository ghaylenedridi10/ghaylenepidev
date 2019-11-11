using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Infra2;
using System.Linq.Expressions;
using Service.Pattern;
using Domaine.Entities;
using MyFinance.Data.Infra2;
using Domain.Entities;

namespace Services
{
    public class ServiceProduct : Service<Produit>, IServiceProduct
    {
        private static DatabaseFactory dbf = new DatabaseFactory();
        private static IUnitOfWork uow = new UnitOfWork(dbf);
        public ServiceProduct() : base(uow)
        {

        }

    }
}
