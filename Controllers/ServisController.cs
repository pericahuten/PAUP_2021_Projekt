﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Paup_2021_ServisVozila.Models;

namespace Paup_2021_ServisVozila.Controllers
{
    public class ServisController : Controller
    {
        BazaDbContext bazaPodataka = new BazaDbContext();
        // GET: Servis
        public ActionResult Popis()
        {
            var listaServisa = bazaPodataka.PopisServisa.ToList();
            return View(listaServisa);

        }
    }
}