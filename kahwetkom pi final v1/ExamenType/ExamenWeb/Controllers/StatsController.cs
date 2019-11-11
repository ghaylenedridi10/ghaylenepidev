using Data;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamenWeb.Controllers
{
    public class StatsController : Controller
    {
        ServiceUser serviceUser = null;
        public StatsController()
        {
            serviceUser = new ServiceUser();
        }
        // GET: Stats
        public ActionResult Index()
        {
            return View();
        }

        //pie Chart
        public ActionResult GetUsers()
        {
            int active = serviceUser.NbrActiveUsers();
            int inActive = serviceUser.NbrInActiveUsers();
            Ratio obj = new Ratio();
            obj.Active = active;
            obj.InActive = inActive;
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public class Ratio
        {
            public int Active { get; set; }
            public int InActive { get; set; }
        }

        //Line Chart
        public ActionResult GetData()
        {
            ExamenContext context = new ExamenContext();

            var query = context.Commandes.Include("User")
                   .GroupBy(p => p.DateCommand.Month)
                   .Select(g => new { name = g.Key, count = g.Count() }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);

        }
    }
}