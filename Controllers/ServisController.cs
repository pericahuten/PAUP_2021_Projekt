﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Paup_2021_ServisVozila.Misc;
using Paup_2021_ServisVozila.Models;

namespace Paup_2021_ServisVozila.Controllers
{
    [Authorize(Roles = OvlastiKorisnik.Administrator + ", " + OvlastiKorisnik.Korisnik)]
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
            vlasnik.Insert(0, new Automobili { Vin = "123", Proizvodac = 0, Model = "Odaberite auto" });
            ViewBag.Vlasnik = vlasnik;
            return View();
        }

        [HttpPost]
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

        public ActionResult Azuriraj(int? id)
        {
            Servisi servis;
            servis = bazaPodataka.PopisServisa.FirstOrDefault(x => x.Id == id);
            if (servis == null)
            {
                return HttpNotFound();
            }
            var ovlast = bazaPodataka.PopisOvlasti.OrderBy(x => x.Naziv).ToList();
            ViewBag.Ovlast = ovlast;
            var status = bazaPodataka.PopisStatusa.OrderBy(x => x.id).ToList();
            ViewBag.Status = status;

            return View(servis);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Azuriraj(Servisi servis)
        {
            LogiraniKorisnik k = User as LogiraniKorisnik;
            if(k != null)
            {
                ViewBag.Logirani = k.KorisnickoIme;
            }
            if (servis.Id != 0)
            {
                bazaPodataka.Entry(servis).State = System.Data.Entity.EntityState.Modified;
            }
            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated && ((User as LogiraniKorisnik).IsInRole(OvlastiKorisnik.Korisnik)))
                {
                    servis.Serviser = servis.Serviser;
                }
                else
                    servis.Serviser = k.PrezimeIme;
                bazaPodataka.SaveChanges();
                return RedirectToAction("Popis");
            }
            return View(servis);
        }

    }
}