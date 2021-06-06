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
    public class ArtikliController : Controller
    {
        BazaDbContext bazaPodataka = new BazaDbContext();
        // GET: Artikli
        public ActionResult Popis()
        {
            var popisArtikla = bazaPodataka.PopisArtikla.ToList();
            return View(popisArtikla);
        }

        public ActionResult DodajArtikl()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DodajArtikl(Artikli a)
        {
            var zadnji = bazaPodataka.PopisArtikla.ToList().OrderByDescending(x => x.id).FirstOrDefault();
            a.id = zadnji.id + 1;
            bazaPodataka.PopisArtikla.Add(a);
            bazaPodataka.SaveChanges();
            return RedirectToAction("Popis");
        }

        public ActionResult Azuriraj(int id)
        {
            Artikli artikl;
            artikl = bazaPodataka.PopisArtikla.FirstOrDefault(x => x.id == id);
            if (artikl == null)
            {
                return HttpNotFound();
            }
            return View(artikl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Azuriraj(Artikli artikl)
        {
            bazaPodataka.Entry(artikl).State = System.Data.Entity.EntityState.Modified;
            if (ModelState.IsValid)
            {
                bazaPodataka.SaveChanges();
                return RedirectToAction("Popis");
            }
            return View(artikl);
        }

        public ActionResult Brisi(int? id)
        {
            //ako id nije defiran preusmjeravamo korisnika na popis studenata
            if (id == null)
            {
                return RedirectToAction("Popis");
            }

            //dohvaćamo studenta iz baze podataka na temelju id
            Artikli a = bazaPodataka.PopisArtikla.FirstOrDefault(x => x.id == id);

            //ako ne postoji student s tim id-em vraćamo HTTP status Not found
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
            Artikli s = bazaPodataka.PopisArtikla.FirstOrDefault(x => x.id == id);
            if (s == null)
            {
                return HttpNotFound();
            }

            bazaPodataka.PopisArtikla.Remove(s);
            bazaPodataka.SaveChanges();

            return View("BrisiStatus");
        }
    }
}