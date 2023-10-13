using InForno.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InForno.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ProdottoController : Controller
    {
        private ModelDBContext db = new ModelDBContext();
        // GET: Prodotto
        public ActionResult Index()
        {
            return View(db.Prodotto);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Prodotto p, HttpPostedFileBase Foto) 
        {
            if (ModelState.IsValid)
            {
                if (Foto != null && Foto.ContentLength > 0)
                {
                    string nomeFile = Foto.FileName;
                    string path = Path.Combine(Server.MapPath("~/Content/assets"), nomeFile);
                    Foto.SaveAs(path);
                    p.Foto = nomeFile;
                }
                else
                {
                    p.Foto = "";
                }
                db.Prodotto.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else {
                ViewBag.Error = "Dati Non Validi";
                return View();
                 }
        }

        public ActionResult Edit(int id)
        {
            return View(db.Prodotto.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(Prodotto p)
        {
            if(ModelState.IsValid)
            {
                db.Entry(p).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(p);
        }

        public ActionResult Delete(int id)
        {
            Prodotto p = db.Prodotto.Find(id);
            db.Prodotto.Remove(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}