using InForno.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InForno.Controllers
{
    [Authorize(Roles ="Admin,User")]
    public class OrdineController : Controller
    {
        private ModelDBContext db = new ModelDBContext();
        // GET: Ordine
        public ActionResult AddOrdine()
        {
            ViewBag.Carrello = Session["Carrello"];
            return View();
        }

        [HttpPost]
        public ActionResult AddOrdine(Ordine ordine)
        {
            ViewBag.Carrello = Session["Carrello"];
            ordine.Concluso = false;
            ordine.Evaso = false;
            ordine.Data = DateTime.Now;
            ordine.TempoConsegna = ViewBag.Carrello.Count * 5 + "min";
            ordine.Importo = Cart.CalcoloCostoTotale(ViewBag.Carrello);
            User user = db.User.FirstOrDefault(u => u.Username == User.Identity.Name);
            ordine.IdUser = user.IdUser;
            
            if(ModelState.IsValid)
            {
                db.Ordine.Add(ordine);
                db.SaveChanges();

                foreach(Cart item in ViewBag.Carrello)
                {
                    DettaglioOrdine d = new DettaglioOrdine(item.Quantita,item.IdProdotto,ordine.IdOrdine);
                    db.DettaglioOrdine.Add(d);
                    db.SaveChanges();
                }
                Session.Remove("Carrello");
                return RedirectToAction("Index","Home");
            }
            else { return View(); }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Ordine.Where(x => !x.Concluso).ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        { 
            return View(db.Ordine.Find(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(Ordine o)
        {
            if (ModelState.IsValid)
            {
                ModelDBContext db1 = new ModelDBContext();
                db1.Entry(o).State = EntityState.Modified;
                db1.SaveChanges();
                return RedirectToAction("Index","Ordine");    
            }
            return View(o);
        }
    }
}