using Domain.Entities;
using Data.Infrastructure;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;

namespace Services
{
   public class NewslettreService: Service<Newslettre>,INewslettreService
    {
        static IDatabaseFactory factory = new DatabaseFactory();
        static IUnitOfWork utk = new UnitOfWork(factory);

        public NewslettreService() : base(utk)
        {

        }
    }
}
