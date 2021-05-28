using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Paup_2021_ServisVozila.Misc;
using Paup_2021_ServisVozila.Models;

namespace Paup_2021_ServisVozila.Controllers
{
    public class ServisController : Controller
    {
        BazaDbContext bazaPodataka = new BazaDbContext();
        // GET: Servis
        public ActionResult Popis()
        {
            var listaServisa = bazaPodataka.PopisServisa.ToList();
            return View(listaServisa);

        }

        public ActionResult Naruzdba()
        {
            LogiraniKorisnik k = User as LogiraniKorisnik;
            var auti = bazaPodataka.PopisAutomobila.OrderBy(x => x.korisnikId).ToList();
            auti.Insert(0, new Automobili { korisnikId = "", Vin = "Odaberite svoj auto" });
            ViewBag.Auto = auti;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Naruzdba(Servisi servis, LogiraniKorisnik kor)
        {
            kor = User as LogiraniKorisnik;
            Automobili auto = bazaPodataka.PopisAutomobila.FirstOrDefault(x => x.Vlasnik.ToString() == kor.KorisnickoIme);
            if (ModelState.IsValid)
            {
                servis.AutoID = auto.Vin;
                servis.DatumKreiranja = DateTime.Now;
                servis.StatusAuta = 1;
            }
            return View();
        }
    }
}