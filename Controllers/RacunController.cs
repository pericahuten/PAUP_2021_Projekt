using Paup_2021_ServisVozila.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paup_2021_ServisVozila.Controllers
{
    public class RacunController : Controller
    {
        BazaDbContext bazaPodataka = new BazaDbContext();
        // GET: Racun
        public ActionResult Popis()
        {
            var listaRacuna = bazaPodataka.PopisRacuna.ToList();
            return View(listaRacuna);
        }
    }
}