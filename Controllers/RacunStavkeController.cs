﻿using Paup_2021_ServisVozila.Misc;
using Paup_2021_ServisVozila.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paup_2021_ServisVozila.Controllers
{
    public class RacunStavkeController : Controller
    {
        BazaDbContext bazaPodataka = new BazaDbContext();
        // GET: RacunStavke
        public ActionResult Popis()
        {
            var popisStavki = bazaPodataka.PopisStavki.ToList();
            return View(popisStavki);
        }

        public ActionResult DodajStavkuRacuna(int id)
        {
            LogiraniKorisnik k = User as LogiraniKorisnik;
            var artikli = bazaPodataka.PopisArtikla.ToList();
            artikli.Insert(0, new Artikli { id = 0, cijena = 0, jedinicaMjere = "0", Naziv = "Odaberite vrstu artikla" });
            ViewBag.Artikli = artikli;
            return View();
        }

        [HttpPost]
        public ActionResult DodajStavkuRacuna(RacunStavke rs, int id)
        {

            Racun r = bazaPodataka.PopisRacuna.FirstOrDefault(x => x.id == id);
            var artikli = bazaPodataka.PopisArtikla.SingleOrDefault(x => x.id == rs.sifraArtikla);
            var servis = bazaPodataka.PopisServisa.FirstOrDefault(x => x.Id == r.servis);
            var popisStavki = bazaPodataka.PopisStavki.Where(x => x.idRacuna == r.id).ToList();
            var price = 0;
            rs.redniBroj = 1;
            foreach (var x in popisStavki)
            {  
                if (x.redniBroj == rs.redniBroj)
                {
                    rs.redniBroj++;
                }
            }
            if (ModelState.IsValid)
            {
                rs.idRacuna = r.id;
                rs.iznos = artikli.cijena * rs.kolicina;
                foreach (var x in popisStavki)
                {
                    price += x.iznos; 
                }
                r.cijena = price+rs.iznos;
                servis.StatusAuta = 2;
                bazaPodataka.PopisStavki.Add(rs);
                bazaPodataka.SaveChanges();
                return RedirectToAction("Popis", "Racun");
            }
            return View(rs);
        }

        public ActionResult Brisi(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Popis");
            }

            RacunStavke rs = bazaPodataka.PopisStavki.FirstOrDefault(x => x.id == id);

            if (rs == null)
            {
                return HttpNotFound();
            }
            return View(rs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Brisi(int id)
        {
            RacunStavke rs = bazaPodataka.PopisStavki.FirstOrDefault(x => x.id == id);
            Racun r = bazaPodataka.PopisRacuna.FirstOrDefault(x => x.id == rs.idRacuna);
            var cijena = 0;

            if (rs == null)
            {
                return HttpNotFound();
            }

            bazaPodataka.PopisStavki.Remove(rs);
            bazaPodataka.SaveChanges();
            var popisStavki = bazaPodataka.PopisStavki.Where(x => x.idRacuna == r.id).ToList();
            foreach (var x in popisStavki)
            {
                cijena += x.iznos;
            }
            r.cijena = cijena;
            bazaPodataka.SaveChanges();

            return View("BrisiStatus");
        }
    }
}