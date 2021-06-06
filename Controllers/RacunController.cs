﻿using Paup_2021_ServisVozila.Misc;
using Paup_2021_ServisVozila.Models;
using Paup_2021_ServisVozila.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paup_2021_ServisVozila.Controllers
{
    [Authorize(Roles = OvlastiKorisnik.Administrator + ", " + OvlastiKorisnik.Korisnik)]
    public class RacunController : Controller
    {
        BazaDbContext bazaPodataka = new BazaDbContext();
        // GET: Racun
        public ActionResult Popis()
        {
            LogiraniKorisnik logkor = User as LogiraniKorisnik;
            if (logkor!=null)
            {
                ViewBag.Korisnik = logkor.KorisnickoIme;
            }
            var listaRacuna = bazaPodataka.PopisRacuna.ToList();
            return View(listaRacuna);
        }

        [Authorize(Roles = OvlastiKorisnik.Administrator)]
        public ActionResult DodajRacun(Racun r, int id)
        {
            LogiraniKorisnik logKor = User as LogiraniKorisnik;
            Servisi servis = bazaPodataka.PopisServisa.FirstOrDefault(x => x.Id == id);
            if (servis == null)
            {
                return RedirectToAction("Popis");
            }
            r.osoba = logKor.PrezimeIme;
            r.Datum = DateTime.Now;
            r.servis = id;
            bazaPodataka.PopisRacuna.Add(r);
            bazaPodataka.SaveChanges();
            return RedirectToAction("Popis", "Racun");
        }

        public ActionResult Detalji(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Popis");
            }
            var PopisStavki = bazaPodataka.PopisStavki.Where(x => x.idRacuna == id).ToList();
            ViewBag.Stavke = PopisStavki;
            Racun racun = bazaPodataka.PopisRacuna.FirstOrDefault(x => x.id == id);

            if (racun == null)
            {
                return RedirectToAction("Popis");
            }
            return View(racun);
        }
        
        public ActionResult IspisRacuna(int? id)
        {
            LogiraniKorisnik logKor = User as LogiraniKorisnik;
            if (logKor != null)
            {
                ViewBag.Logirani = logKor.KorisnickoIme;
            }

            if (!id.HasValue)
            {
                return RedirectToAction("Popis");
            }

            Racun racun = bazaPodataka.PopisRacuna.FirstOrDefault(x => x.id == id);
            var rs = bazaPodataka.PopisStavki.Where(x => x.idRacuna == racun.id).ToList();
            if (racun == null)
            {
                return RedirectToAction("Popis");
            }

            RacunReport ispisracuna = new RacunReport();
            ispisracuna.IspisRacuna(racun, rs, logKor);

            return File(ispisracuna.Podaci, System.Net.Mime.MediaTypeNames.Application.Pdf, racun.servisiFK.idAuto.Vin + ".pdf");
        }
    }
}