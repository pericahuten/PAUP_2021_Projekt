using Paup_2021_ServisVozila.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paup_2021_ServisVozila.Controllers
{
    public class MarkeAutomobiliController : Controller
    {
        BazaDbContext bazaPodataka = new BazaDbContext();
        // GET: MarkeAutomobili
        public ActionResult Popis()
        {
            var listaMarka = bazaPodataka.PopisMarka.ToList();
            return View(listaMarka);
        }
    }
}