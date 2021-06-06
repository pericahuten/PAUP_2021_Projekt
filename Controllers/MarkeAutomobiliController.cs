using Paup_2021_ServisVozila.Misc;
using Paup_2021_ServisVozila.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paup_2021_ServisVozila.Controllers
{
    [Authorize(Roles = OvlastiKorisnik.Administrator)]
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
        [ValidateAntiForgeryToken]
        public ActionResult DodajMarku(MarkeAutomobili ma)
        {
            var zadnji = bazaPodataka.PopisMarka.ToList().OrderByDescending(x => x.id).FirstOrDefault();
            var zauzeti = bazaPodataka.PopisMarka.Any(x => x.Marke == ma.Marke);
            if (zauzeti)
            {
                ModelState.AddModelError("Marke", "Marka automobila je vec upisana!");
            }
            ma.id = zadnji.id + 1;
            if (ModelState.IsValid)
            {
                bazaPodataka.PopisMarka.Add(ma);
                bazaPodataka.SaveChanges();
                return RedirectToAction("Popis");
            }
            return View(ma);
        }

        public ActionResult Brisi(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Popis");
            }

            MarkeAutomobili a = bazaPodataka.PopisMarka.FirstOrDefault(x => x.id == id);

            if (a == null)
            {
                return HttpNotFound();
            }
            return View(a);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Brisi(int id)
        {
            MarkeAutomobili s = bazaPodataka.PopisMarka.FirstOrDefault(x => x.id == id);
            if (s == null)
            {
                return HttpNotFound();
            }

            bazaPodataka.PopisMarka.Remove(s);
            bazaPodataka.SaveChanges();

            return View("BrisiStatus");
        }
    }
}