using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Paup_2021_ServisVozila.Models;

namespace Paup_2021_ServisVozila.Controllers
{
    public class AutomobiliController : Controller
    {
        BazaDbContext bazaPodataka = new BazaDbContext();
        // GET: Automobili
        public ActionResult Index()
        {
            var listaAutomobila = bazaPodataka.PopisAutomobila.ToList();
            return View(listaAutomobila);
        }
    }
}