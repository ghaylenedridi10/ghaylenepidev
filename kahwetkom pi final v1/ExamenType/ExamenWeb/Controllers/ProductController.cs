using Domain.Entities;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ExamenWeb.Controllers
{
    public class ProductController : Controller
    {
        ServiceProduct sp = new ServiceProduct();
        ServicePanier spp = new ServicePanier();
        // GET: Product
        public ActionResult Index()
        {
            var prods = sp.GetMany();
            return View(prods);
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produit p = sp.GetById((int)id);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Produit u)
        {
            sp.Add(u);
            sp.Commit();
            return RedirectToAction("Index");
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
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

        [HttpPost]
        public ActionResult CreateProd(int idProd)
        {

            Paniers p = new Paniers()
            {
                IdProduit = idProd,
                IdPanier = 1,
                Quantite = 1
            };
            spp.Add(p);
            spp.Commit();
            var pan = spp.GetMany();
            return View(pan);

        }
    }
}
