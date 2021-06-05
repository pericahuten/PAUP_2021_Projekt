using Paup_2021_ServisVozila.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paup_2021_ServisVozila.Controllers
{
    public class MarkeAutomobiliController : Controller
    {
        BazaDbContext bazaPodataka = new BazaDbContext();
        // GET: MarkeAutomobili
        public ActionResult Popis()
        {
            var listaMarka = bazaPodataka.PopisMarka.ToList();
            return View(listaMarka);
        }

        public ActionResult DodajMarku()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DodajMarku(MarkeAutomobili ma)
        {
            var zadnji = bazaPodataka.PopisMarka.ToList().OrderByDescending(x => x.id).FirstOrDefault();
            ma.id = zadnji.id + 1;
            bazaPodataka.PopisMarka.Add(ma);
            bazaPodataka.SaveChanges();
            return RedirectToAction("Popis");
        }
    }
}