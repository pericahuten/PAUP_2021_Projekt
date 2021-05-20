using Paup_2021_ServisVozila.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paup_2021_ServisVozila.Controllers
{
    public class ArtikliController : Controller
    {
        BazaDbContext db = new BazaDbContext();
        // GET: Artikli
        public ActionResult Popis()
        {
            var popisArtikla = db.PopisArtikla.ToList();
            return View(popisArtikla);
        }
    }
}