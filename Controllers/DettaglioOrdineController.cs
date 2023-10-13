using InForno.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InForno.Controllers
{
    [Authorize(Roles ="User")]
    public class DettaglioOrdineController : Controller
    {
        private ModelDBContext db = new ModelDBContext();
        // GET: DettaglioOrdine
        public ActionResult Index()
        {
            User user = db.User.FirstOrDefault(u => u.Username == User.Identity.Name);
            Ordine ordine = db.Ordine.FirstOrDefault(x => x.IdUser == user.IdUser && !x.Concluso);
            if (ordine == null)
            {
                ViewBag.Error = "Non sono presenti Ordini!";
            }
            else
            {
                List<DettaglioOrdine> details = db.DettaglioOrdine.Where(x => x.IdOrdine == ordine.IdOrdine).ToList();
                ViewBag.Ordine = details[0].Ordine;
                return View(details);
            }  
            return View();
        }
    }
}