﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Paup_2021_ServisVozila.Models
{
    public class KorisnikPrijava
    {
        [Display(Name = "Korisničko ime")]
        [Required]
        public string KorisnickoIme { get; set; }

        [Display(Name = "Lozinka")]
        [Required]
        [DataType(DataType.Password)]
        public string Lozinka { get; set; }
    } 
}