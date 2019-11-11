using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using ExamenWeb.Models;
using Data;
using Services;

namespace ExamenWeb.Controllers
{
    public class NewslettresController : Controller
    {
        private ExamenContext db = new ExamenContext();
        NewslettreService sp = new NewslettreService();
        public ActionResult Detailsfront(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publicite Publicite = db.Publicites.Find(id);
            if (Publicite == null)
            {
                return HttpNotFound();
            }
            return View(Publicite);
        }

        public ActionResult ListeNewslettre()
        {
            ExamenContext db = new ExamenContext();

            ////List<Product> listprod = Session["Products"] as List<Product>;
            //var produit = sp.GetMany();

            return View(db.Newslettres.ToList());
        }

        [HttpPost]
        public ActionResult ListeNewslettre(String SearchString)
        {
            var newslettre = sp.GetMany(p => p.MailUser.Contains(SearchString));
            return View(newslettre);
        }
        // GET: Product/Create

        // GET: Newslettres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Newslettre newslettre = db.Newslettres.Find(id);
            if (newslettre == null)
            {
                return HttpNotFound();
            }
            return View(newslettre);
        }


        public ActionResult CreateFront()
        {
           OffreService os = new OffreService();
            ViewBag.Offre = os.GetInclude(includes: x=>x.Products).ToList();
            PubliciteService ps = new PubliciteService();
            ViewBag.Publicite = ps.GetInclude(includes: x => x.Products).ToList();
            PromotionService ss = new PromotionService();
            ViewBag.Promotion = ss.GetInclude(includes: x => x.Products).ToList();


            return View();
        }

 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFront([Bind(Include = "IdNewslettre,IdUser,MailUser,PhoneUser")] Newslettre newslettre)
        {
            if (ModelState.IsValid)
            {
                db.Newslettres.Add(newslettre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdUser = new SelectList(db.Users, "IdUser", "Nom", newslettre.IdUser);
            return View(newslettre);
        }

        // GET: Newslettres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Newslettre newslettre = db.Newslettres.Find(id);
            if (newslettre == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUser = new SelectList(db.Users, "IdUser", "Nom", newslettre.IdUser);
            return View(newslettre);
        }

        // POST: Newslettres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdNewslettre,IdUser,MailUser,PhoneUser,status")] Newslettre newslettre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newslettre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListeNewslettre");
            }
            ViewBag.IdUser = new SelectList(db.Users, "IdUser", "Nom", newslettre.IdUser);
            return View(newslettre);
        }

        // GET: Newslettres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Newslettre newslettre = db.Newslettres.Find(id);
            if (newslettre == null)
            {
                return HttpNotFound();
            }
            return View(newslettre);
        }

        // POST: Newslettres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Newslettre newslettre = db.Newslettres.Find(id);
            db.Newslettres.Remove(newslettre);
            db.SaveChanges();
            return RedirectToAction("ListeNewslettre");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult AllNewslettre()
        {
            ExamenContext db = new ExamenContext();
            return View(db.Newslettres.ToList());

        }

        //public ActionResult SearchNewslettre()
        //{
        //    return View();
        //}


        // [HttpPost]
        // public ActionResult SearchNewslettre(String SearchMail)
        // {
        //  var result = db.Newslettres.Where(a => a.MailUser.Contains(SearchMail)).ToList();
        //  return View(result);
        // }
    }
}
