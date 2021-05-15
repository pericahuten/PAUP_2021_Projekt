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
        [Display(Name = "ID vozila")]
        public int id { get; set; }

        [Display(Name = "VIN vozila")]
        [Required(ErrorMessage = "{0} je obavezan")]
        public string Vin { get; set; }

        [ForeignKey("Korisnik")]
        [Display(Name = "Id korisnika")]
        [Required(ErrorMessage = "{0} je obavezan")]
        public int korisnikId { get; set; }
        
        public virtual Korisnik IDKorisnika { get; set; }
        //fali 2 virtaulne klase

        [ForeignKey("Proizvodac")]
        [Display(Name = "Proizvodac automobila")]
        [Required(ErrorMessage = "{0} je obavezan")]
        public int Proizvodac { get; set; }


        //fali 1 virtaulne klasa

        [Display(Name = "Model vozila")]
        [Required(ErrorMessage = "{0} je obavezan")]
        public string Model { get; set; }

        [Display(Name = "Registracija vozila")]
        [Required(ErrorMessage = "{0} je obavezana")]
        public string Registracija { get; set; }
    }
}