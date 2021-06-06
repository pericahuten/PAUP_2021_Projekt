using Paup_2021_ServisVozila.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paup_2021_ServisVozila.Controllers
{
    public class HomeController : Controller
    {
        BazaDbContext bazaPodataka = new BazaDbContext();
        public ActionResult Index()
        {
            var listaMarka = bazaPodataka.PopisMarka.ToList();
            return View(listaMarka);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}