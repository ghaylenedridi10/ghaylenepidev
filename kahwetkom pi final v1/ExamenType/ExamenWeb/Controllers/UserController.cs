using Domaine.Entities;
using ExamenWeb.Models;
using Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ExamenWeb.Controllers
{
    public class UserController : Controller
    {
        ServiceUser su = new ServiceUser();

        // GET: User
        public ActionResult Index()
        {
            var users = su.GetMany();
            return View(users);
        }

        [HttpPost]
        public ActionResult Index(String SearchString)
        {
            var users = su.GetMany(p => p.Name.Contains(SearchString));
            return View(users);
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = su.GetById((int) id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            //UserModels um = new UserModels();
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create( User u)
        {
            /*um.Image = Image.FileName;
            User u = new User()

            {
                FirstName = um.FirstName,
                LastName = um.LastName,
                Email = um.Email,
                Birthdate = um.Birthdate,
                login = um.login,
                Password = um.Password,
                Image = um.Image,
                Function = um.Function
        };
            var path = Path.Combine(Server.MapPath("~/Content/Upload/"), Image.FileName);
            Image.SaveAs(path);
            try
            {
                su.Add(u);
                su.Commit();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }*/

            
            u.SignUpDate= DateTime.Now; 
            su.Add(u);
                su.Commit();
                return RedirectToAction("Index");
           
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User u = su.GetById((int)id);
            if (u == null)
            {
                return HttpNotFound();
            }
            return View(u);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User u)
        {
            su.Update(u.UserID,u);
            //db.Entry(avis).State = EntityState.Modified;
            
            return RedirectToAction("Index");
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User u = su.GetById((int)id);
            if (u == null)
            {
                return HttpNotFound();
            }
            return View(u);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            User i = su.GetById(id);
            su.Delete(i);
            su.Commit();
            return RedirectToAction("Index");
        }

    }
}
