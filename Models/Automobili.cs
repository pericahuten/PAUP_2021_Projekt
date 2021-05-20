using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paup_2021_ServisVozila.Models
{
    [Table("automobili")]
    public class Automobili
    {
        [Key]
        [Display(Name = "VIN vozila")]
        [Required(ErrorMessage = "{0} je obavezan")]
        public string Vin { get; set; }

        [Display(Name = "Id korisnika")]
        [Required(ErrorMessage = "{0} je obavezan")]
        [ForeignKey("Vlasnik")]
        public string korisnikId { get; set; }
        public virtual Korisnik Vlasnik { get; set; }

        [Display(Name = "Proizvodac automobila")]
        [Required(ErrorMessage = "{0} je obavezan")]
        [ForeignKey("marka")]
        public int Proizvodac { get; set; }

        public virtual MarkeAutomobili marka { get; set; }

        [Display(Name = "Model vozila")]
        [Required(ErrorMessage = "{0} je obavezan")]
        public string Model { get; set; }

        [Display(Name = "Registracija vozila")]
        [Required(ErrorMessage = "{0} je obavezana")]
        public string Registracija { get; set; }
    }
}