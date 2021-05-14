using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Paup_2021_ServisVozila.Controllers
{
    public class ServisController : Controller
    {
        // GET: Servis
        public ActionResult Index()
        {
            ViewBag.Title = "Početna stranica servisa";
            ViewBag.Servis = "Servis d.o.o";
            return View();
        }
    }
}