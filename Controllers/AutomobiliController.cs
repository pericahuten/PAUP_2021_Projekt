using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Paup_2021_ServisVozila.Misc;
using Paup_2021_ServisVozila.Models;

namespace Paup_2021_ServisVozila.Controllers
{
    public class AutomobiliController : Controller
    {
        BazaDbContext bazaPodataka = new BazaDbContext();
        // GET: Automobili
        public ActionResult Popis()
        {
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
        [AllowAnonymous]
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
    }
}