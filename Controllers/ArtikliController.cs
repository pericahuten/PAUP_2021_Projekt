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
    }
}