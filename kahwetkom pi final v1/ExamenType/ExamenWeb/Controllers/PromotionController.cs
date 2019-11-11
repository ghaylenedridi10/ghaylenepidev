using Data;
using Domain.Entities;
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
    public class PromotionController : Controller
    {
        private ExamenContext db = new ExamenContext();
        PromotionService sp = new PromotionService();
        // GET: Promotion
        
        public ActionResult Index()
        {
            return View(db.Promotions.Include("Products").ToList());
        }

        // GET: Promotion/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult readPromotion()
        {
            return View(db.Promotions.Include("Products").ToList());
        }

        // GET: Promotion/Create
        public ActionResult Create()
        {
            PromotionModel pm = new PromotionModel();
            //pour recupérer un  champ spécifique  Select(c=>c.name)

            IEnumerable<Produit> Prods = db.Produits.ToList();
            ViewBag.p = Prods;



            return View(pm);
        }




        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Promotion promotion)
        {
            ProduitService ps = new ProduitService();
            promotion.IdProduit = db.Produits.Find(Convert.ToInt32(promotion.IdProduit)).IdProduit;
            //ProduitService ps = new ProduitService();
            var Product = db.Produits.Find(Convert.ToInt32(promotion.IdProduit));
            promotion.NewPrice = Product.Prix * (100 - promotion.Remise) / 100;
            

            try
            {
                // TODO: Add insert logic here
                sp.Add(promotion);
                // listprod.Add(p);

                // Session["Products"] = listprod;

                sp.Commit();
                return RedirectToAction("ListePublicitee");
            }
            catch(Exception x)
            {
                return View();
            }
        }

        // GET: Promotion/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Promotion/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promotion u = sp.GetById((int)id);
            if (u == null)
            {
                return HttpNotFound();
            }
            return View(u);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Promotion u)
        {
            sp.Update(u.IdPromotion, u);
            //db.Entry(avis).State = EntityState.Modified;

            return RedirectToAction("Index");
        }

        // GET: Promotion/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Promotion/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
