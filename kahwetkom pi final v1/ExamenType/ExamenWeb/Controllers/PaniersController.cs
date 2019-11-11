using Domain.Entities;
using ExamenWeb.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PayPal.Api;
using System.Net;
using Data;

namespace ExamenWeb.Controllers
{
    public class PaniersController : Controller
    {
        ServiceProduct spod = new ServiceProduct();
        ServicePanier sp = new ServicePanier();
        ExamenContext db;
        private string strCart = "Cart";
        // GET: Panier
        public ActionResult Index()
        {
            //var paniers = sp.GetMany();
            
            return View();
        }

        public ActionResult OrderNow(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session[strCart] == null)
            {
                List<Cart> lsCart = new List<Cart>
                {
                    new Cart(spod.GetById((int) id),1)
                };
                Session[strCart] = lsCart;

            }
            else
            {
                List<Cart> lsCart = (List<Cart>)Session[strCart];
                int check = isExistingCheck(id);
                if (check == -1)
                    lsCart.Add(new Cart(spod.GetById((int) id), 1));
                else
                    lsCart[check].Quantity++;
                
                Session[strCart] = lsCart;
            }

            return View("OrderNow");
        }

        private int isExistingCheck (int? id)
        {
            List<Cart> lsCart = (List<Cart>)Session[strCart];
            for (int i = 0; i < lsCart.Count; i++)
            {
                if (lsCart[i].Product.Id_produit == id)
                    return i;
            }
            return -1;
        }

        // GET: Panier/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Panier/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Panier/Create
        [HttpPost]
        public ActionResult Create(int  pm)
        {
            Paniers p = new Paniers()
            {
                IdProduit = pm,
                IdPanier = 1,
                Quantite = 1
            };
            sp.Add(p);
            sp.Commit();
            return RedirectToAction("Index");
        }

        // GET: Panier/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Panier/Edit/5
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

        // GET: Panier/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int check = isExistingCheck((int)id);
            List<Cart> lsCart = (List<Cart>)Session[strCart];
            lsCart.RemoveAt(check);
            return View("OrderNow");
        }

        // POST: Panier/Delete/5


        public ActionResult clearCart()
        {
            List<Cart> lsCart = (List<Cart>)Session[strCart];
            lsCart.Clear();
            return View("OrderNow");

        }

        public ActionResult UpdateCart (FormCollection frc)
        {
            string [] quantities = frc.GetValues("quantitys");
            List<Cart> lsCart = (List<Cart>)Session[strCart];
            for (int i=0; i < lsCart.Count; i++)
            {
                lsCart[i].Quantity = Convert.ToInt32(quantities[i]);

            }
            Session[strCart] = lsCart;
            return View("Index");
        }
        // Work with Paypal Payment
        private Payment payment;

        //Create a payment using an APIContext
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
         {
            var listItems = new ItemList() { items = new List<Item>() };
            List<Cart> listCarts = (List<Cart>)Session[strCart];
            foreach (var cart in listCarts)
            {
                listItems.items.Add( new Item(){
                    name = cart.Product.Nom,
                    currency = "USD",
                    price = cart.Product.Price.ToString(),
                    quantity = cart.Quantity.ToString(),
                    sku = "sku"
                });
            }

            var payer = new Payer() { payment_method = "paypal" };

            //Do the config Redirect URL here with redirectURLs
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };
            // Create details object
            var details = new Details()
            {
                tax = "1",
                shipping = "2",
                subtotal = listCarts.Sum(x => x.Quantity * x.Product.Price).ToString()
            };

            //Create amount object
            var amount = new Amount()
            {
                currency = "USD",
                total = (Convert.ToDouble(details.tax) + Convert.ToDouble(details.shipping) + Convert.ToDouble(details.subtotal)).ToString(),
                details = details
            };

            //Create transaction
            var transactionList = new List<Transaction> ();
            transactionList.Add(new Transaction()
            {
                description = "CRM Testing transaction description",
                invoice_number = Convert.ToString((new Random()).Next(100000)),
                amount=amount,
                item_list=listItems
            });
            payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            return payment.Create(apiContext);
         }

        //Create Execute Payment methode

        private Payment ExecutePayment (APIContext apiContext, string payerId,string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id=payerId
            };
            payment = new Payment() { id = paymentId };
            return payment.Execute(apiContext, paymentExecution);
        }

        // Create PaymentPaypal method
        public ActionResult PaymentWithPaypal()
        {
            //Gettings context from the paypal bases on clientId and clientSecret
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Paniers/PaymentWithPaypal?";
                    var guid = Convert.ToString((new Random()).Next(100000));
                    var createdPayment = CreatePayment(apiContext, baseURI + "guid=" + guid);
                    //Get links returned from paypal response call
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = string.Empty;
                    while (links.MoveNext())
                    {
                        Links link = links.Current;
                        if (link.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectUrl = link.href;
                        }
                    }
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This one will be executed when we have received all the payment params from previous call
                    var guid = Request.Params["guid"];
                    var executePayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    if (executePayment.state.ToLower() !="approved")
                    {
                        Session.Remove(strCart);
                        return View("Failure");
                    }
                }
            }
            catch (Exception ex)
            {
                PaypalLogger.Log("Error: " + ex.Message);
                return View("Failure");
                
            }

            Session.Remove(strCart);
            return View("Success");
            
            
        }


}
}
