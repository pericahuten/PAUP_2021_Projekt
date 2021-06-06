using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Paup_2021_ServisVozila.Misc;
using Paup_2021_ServisVozila.Models;

namespace Paup_2021_ServisVozila.Controllers
{
    [Authorize(Roles = OvlastiKorisnik.Administrator + ", " + OvlastiKorisnik.Korisnik)]
    public class AutomobiliController : Controller
    {
        BazaDbContext bazaPodataka = new BazaDbContext();
        // GET: Automobili
        public ActionResult Popis()
        {
            LogiraniKorisnik k = User as LogiraniKorisnik;
            if(k!=null)
            {
                ViewBag.Korisnik = k.KorisnickoIme;
            }
            var listaAutomobila = bazaPodataka.PopisAutomobila.ToList();
            return View(listaAutomobila);
        }

        public ActionResult DodajAuto()
        {
            var proizvodaci = bazaPodataka.PopisMarka.OrderBy(x => x.Marke).ToList();
            proizvodaci.Insert(0, new MarkeAutomobili { id = 1, Marke = "Odaberite proizvođača" });
            ViewBag.Proizvodaci = proizvodaci;
            return View();
        }

        [HttpPost]
        public ActionResult DodajAuto(Automobili auto)
        {
            if (!VIN.validateVIN(auto.Vin))
            {
                ModelState.AddModelError("VIN", "Neispravan VIN broj vozila!");
            }
            LogiraniKorisnik logKor = User as LogiraniKorisnik;
            if(logKor!=null)
            {
                ViewBag.Logirani = logKor.KorisnickoIme;
            }

            if (ModelState.IsValid)
            {
                auto.korisnikId = logKor.KorisnickoIme;
                bazaPodataka.PopisAutomobila.Add(auto);
                bazaPodataka.SaveChanges();
                return View("DodavanjeAuta");
            }
            return View(auto);
        }

        public ActionResult Azuriraj(string vin)
        {
            Automobili auto;
            if(vin == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            auto = bazaPodataka.PopisAutomobila.FirstOrDefault(x => x.Vin == vin);
            if(auto == null)
            {
                return HttpNotFound();
            }
            var marke = bazaPodataka.PopisMarka.OrderBy(x => x.Marke).ToList();
            ViewBag.Marke = marke;
            var korisnici = bazaPodataka.PopisKorisnika.OrderBy(x => x.KorisnickoIme).ToList();
            ViewBag.Korisnici = korisnici;

            return View(auto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Azuriraj(Automobili auto)
        {
            if (auto.Vin != "")
            {
                bazaPodataka.Entry(auto).State = System.Data.Entity.EntityState.Modified;
            }
            if (!VIN.validateVIN(auto.Vin))
            {
                ModelState.AddModelError("VIN", "Neispravan VIN broj vozila!");
            }
            if (ModelState.IsValid)
            {
                bazaPodataka.SaveChanges();
                return RedirectToAction("Popis");
            }
            return View(auto);
        }
    }
}