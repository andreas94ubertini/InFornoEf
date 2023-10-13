using InForno.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InForno.Controllers
{
    [Authorize(Roles ="Admin")]
    public class QueryController : Controller
    {
        private static ModelDBContext db = new ModelDBContext();
        // GET: Query
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetOrdiniEvasi()
        {
            List<Ordine> ordiniEvasi = db.Ordine.Where(x => x.Evaso).ToList();
            var formattedOrdini = ordiniEvasi.Select(o => new
            {
                o.IdOrdine,
                o.Evaso,
                o.Concluso,
                Data = o.Data.ToString("yyyy-MM-dd"),
                o.TempoConsegna,
                o.Commento,
                o.Indirizzo,
                o.Importo,
                o.IdUser
            }).ToList();
            return Json(formattedOrdini,JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTotGuadagnato()
        {
            DateTime dataOdierna = DateTime.Today;
            DateTime inizioDomani = dataOdierna.AddDays(1);
            List<Ordine> ordiniOggi = db.Ordine
                .Where(x => x.Data >= dataOdierna && x.Data < inizioDomani && x.Concluso).ToList();
            decimal tot = 0;
            foreach(Ordine o in ordiniOggi)
            {
                tot += o.Importo;
            }
            return Json(tot, JsonRequestBehavior.AllowGet);
        }
    }
}