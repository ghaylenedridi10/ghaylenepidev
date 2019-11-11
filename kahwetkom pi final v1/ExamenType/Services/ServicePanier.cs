using Data.Infra2;
using Domain.Entities;
using MyFinance.Data.Infra2;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServicePanier : Service<Paniers>, IServicePanier
    {
        private static DatabaseFactory dbf = new DatabaseFactory();
        private static IUnitOfWork uow = new UnitOfWork(dbf);
        public ServicePanier() : base(uow)
        {

        }
    }
}
