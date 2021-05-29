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
            LogiraniKorisnik k = User as LogiraniKorisnik;
            if(k != null)
            {
                ViewBag.Korisnik = k.KorisnickoIme;
            }
            var listaServisa = bazaPodataka.PopisServisa.ToList();
            return View(listaServisa);

        }

        public ActionResult Naruzdba()
        {
            LogiraniKorisnik k = User as LogiraniKorisnik;
            var vlasnik = bazaPodataka.PopisAutomobila.Where(x => x.korisnikId == k.KorisnickoIme).ToList();
            vlasnik.Insert(0, new Automobili { Vin = null, Proizvodac = 0, Model = "Odaberite auto" });
            ViewBag.Vlasnik = vlasnik;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Naruzdba(Servisi servis)
        {
            if (ModelState.IsValid)
            {
                servis.DatumKreiranja = DateTime.Now;
                servis.StatusAuta = 1;
                servis.Serviser = "Nedodjeljen";
                bazaPodataka.PopisServisa.Add(servis);
                bazaPodataka.SaveChanges();
                return View("ServisDodan");
            }
            return View(servis);
        }

        public ActionResult Detalji(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Popis");
            }

            Servisi servisi = bazaPodataka.PopisServisa.FirstOrDefault(x => x.Id == id);

            if (servisi == null)
            {
                return RedirectToAction("Popis");
            }
            return View(servisi);
        }
    }
}