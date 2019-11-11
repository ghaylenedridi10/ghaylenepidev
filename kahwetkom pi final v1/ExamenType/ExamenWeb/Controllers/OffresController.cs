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
using System.Net.Mail;
using Services;

namespace ExamenWeb.Controllers
{
    public class OffresController : Controller
    {
        private ExamenContext db = new ExamenContext();

        // GET: Offres
        public ActionResult Index()
        {
            return View(db.Offres.Include("Products").ToList());
        }
        public ActionResult readOffreFront()
        {
            return View(db.Offres.Include("Products").ToList());
        }


        // GET: Offres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offre offre = db.Offres.Find(id);
            if (offre == null)
            {
                return HttpNotFound();
            }
            return View(offre);
        }

        // GET: Offres/Create
        public ActionResult Create()
        {

            IEnumerable<Produit> Prods = db.Produits.ToList();
            ViewBag.p = Prods;
            return View();
        }
        public ActionResult Test()
        {
            return View();
        }

        // POST: Offres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdOffer,Description,DateDebut,DateFin,idProducts")] Offre offre)
        {

            offre.Products =  db.Produits.Find(Convert.ToInt32(offre.idProducts));
            if (ModelState.IsValid)
            {
                db.Offres.Add(offre);
                db.SaveChanges();
                try
                {
                    NewslettreService ns = new NewslettreService();
                    var mails = ns.GetAll().Where(x=>x.status == false).Select(x=>x.MailUser).ToList();
                    mails.ForEach(x =>
                    {
                        MailMessage mail = new MailMessage("ghaylene.dridi@esprit.tn", x);
                        mail.Subject = offre.Description;
                        mail.Body = offre.Products.Libelle;

                        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                        smtpClient.Credentials = new System.Net.NetworkCredential("ghaylene.dridi@esprit.tn", "19952008");
                        smtpClient.EnableSsl = true;
                        smtpClient.Send(mail);
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                return RedirectToAction("Index");
            }

            return View(offre);
        }

        // GET: Offres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IEnumerable<Produit> Prods = db.Produits.ToList();
            ViewBag.p = Prods;
            Offre offre = db.Offres.Find(id);
            if (offre == null)
            {
                return HttpNotFound();
            }
            return View(offre);
        }

        // POST: Offres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdOffer,Description,DateDebut,DateFin")] Offre offre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(offre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(offre);
        }

        // GET: Offres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offre offre = db.Offres.Find(id);
            if (offre == null)
            {
                return HttpNotFound();
            }
            return View(offre);
        }

        // POST: Offres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Offre offre = db.Offres.Find(id);
            db.Offres.Remove(offre);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
