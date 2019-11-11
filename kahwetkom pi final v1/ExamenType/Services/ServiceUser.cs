using Data.Infra2;
using Domaine.Entities;
using MyFinance.Data.Infra2;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceUser : Service<User>, IServiceUser
    {
        private static DatabaseFactory dbf = new DatabaseFactory();
        private static UnitOfWork ut = new UnitOfWork(dbf);

        public ServiceUser(): base(ut)
        {

        }

        public int NbrActiveUsers()
        {
            return GetMany().Where(x => x.IsActive == true).Count();
        }
        public int NbrInActiveUsers()
        {
            return GetMany().Where(x => x.IsActive == false).Count();
        }

        public List<User> checkUserName(User user)
        {
            return GetMany().Where(a => a.UserName == user.UserName).ToList();
        }

        public int LoginCheck(User u)
        {
            return GetMany().Where(x => x.UserName == u.UserName && x.Password == u.Password).Count();
        }

        public List<User> ListAdmin()
        {
            return GetMany().Where(x => x.Role == "Admin").ToList();
        }

        public User selectedUser(int id)
        {
            return GetMany().Where(x => x.UserID == id).FirstOrDefault();
        }
    }
}
