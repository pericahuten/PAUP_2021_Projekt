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
        BazaDbContext db = new BazaDbContext();
        // GET: RacunStavke
        public ActionResult Popis()
        {
            var popisStavki = db.PopisStavki.ToList();
            return View(popisStavki);
        }
    }
}