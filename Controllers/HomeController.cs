using InForno.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InForno.Controllers
{
    [Authorize(Roles ="Admin,User")]
    public class HomeController : Controller
    {
        private static ModelDBContext db = new ModelDBContext();
        
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            ViewBag.Carrello = Session["Carrello"];
            return View(db.Prodotto.ToList());
        }


        public ActionResult AddToCart(int IdProdotto, int Quantita)
        {
            Prodotto p = db.Prodotto.Find(IdProdotto);
            Cart cartItem = new Cart(Quantita, p.Nome, p.Prezzo, IdProdotto);
            List<Cart> carrello = Session["Carrello"] as List<Cart> ?? new List<Cart>();
            carrello.Add(cartItem);
            Session["Carrello"] = carrello;
            return RedirectToAction("Index");
        }
    }
}
