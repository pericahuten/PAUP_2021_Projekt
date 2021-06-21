using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Paup_2021_ServisVozila.Misc;
using Paup_2021_ServisVozila.Models;
using Paup_2021_ServisVozila.Reports;

namespace Paup_2021_ServisVozila.Controllers
{
    [Authorize(Roles = OvlastiKorisnik.Administrator + ", " + OvlastiKorisnik.Korisnik)]
    public class AutomobiliController : Controller
    {
        BazaDbContext bazaPodataka = new BazaDbContext();
        // GET: Automobili
        public ActionResult Popis()
        {
            ViewBag.Title = "Popis automobila";
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

        public ActionResult Brisi(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Popis");
            }

            Automobili a = bazaPodataka.PopisAutomobila.FirstOrDefault(x => x.Vin == id);

            if (a == null)
            {
                return HttpNotFound();
            }
            return View(a);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Brisi(string id, Servisi s)
        {
            Automobili a = bazaPodataka.PopisAutomobila.FirstOrDefault(x => x.Vin == id);
            Servisi ser = bazaPodataka.PopisServisa.FirstOrDefault(x => x.AutoID == a.Vin);
            if (a == null)
            {
                return HttpNotFound();
            }
            if (ser == null)
            {
                bazaPodataka.PopisAutomobila.Remove(a);
                bazaPodataka.SaveChanges();
            }
            return View("BrisiStatus");
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


        public ActionResult IspisVozila()
        {
            LogiraniKorisnik logKor = User as LogiraniKorisnik;
            if (logKor!=null)
            {
                ViewBag.Logirani = logKor.KorisnickoIme;
            }

            //administrator dohvaća sva vozila te ispisuje
            if (User.Identity.IsAuthenticated && ((User as LogiraniKorisnik).IsInRole(OvlastiKorisnik.Administrator)))
            {
                var automobiliAdmin = bazaPodataka.PopisAutomobila.OrderBy(x => x.marka.Marke).ToList();

                if (automobiliAdmin == null)
                {
                    return RedirectToAction("Popis");
                }
                VozilaReport ispisvozilaAdmin = new VozilaReport();
                ispisvozilaAdmin.IspisVozila(automobiliAdmin, logKor);

                return File(ispisvozilaAdmin.Podaci, System.Net.Mime.MediaTypeNames.Application.Pdf, "PopisAutomobila" + ".pdf");
            }

            //ispis vozila za trenutnog koristnika
            else
            {
                var automobili = bazaPodataka.PopisAutomobila.Where(x => x.Vlasnik.KorisnickoIme == logKor.KorisnickoIme).OrderBy(x => x.marka.Marke).ToList();

                if (automobili == null)
                {
                    return RedirectToAction("Popis");
                }
                VozilaReport ispisvozila = new VozilaReport();
                ispisvozila.IspisVozila(automobili, logKor);

                return File(ispisvozila.Podaci, System.Net.Mime.MediaTypeNames.Application.Pdf, "PopisAutomobila" + ".pdf");
            }
        }
    }
}