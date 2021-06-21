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
        [RegularExpression("[A-HJ-NPR-Z0-9]{13}[0-9]{4}", ErrorMessage = "Krivi format identifikacijskog broja vozila!")]
        public string Vin { get; set; }

        [Display(Name = "Id korisnika")]
        [ForeignKey("Vlasnik")]
        public string korisnikId { get; set; }
        public virtual Korisnik Vlasnik { get; set; }

        [Display(Name = "Proizvodač automobila")]
        [Required(ErrorMessage = "{0} je obavezan")]
        [ForeignKey("marka")]
        public int Proizvodac { get; set; }
        public virtual MarkeAutomobili marka { get; set; }

        [Display(Name = "Model vozila")]
        [Required(ErrorMessage = "{0} je obavezan")]
        public string Model { get; set; }

        [Display(Name = "Registracija vozila")]
        [Required(ErrorMessage = "{0} je obavezna")]
        public string Registracija { get; set; }

        public string Automobil
        {
            get
            {
                return marka?.Marke + " " + Model + ": " + Registracija;
            }
        }

        public string AutomobilAdmin
        {
            get
            {
                return Vlasnik?.PrezimeIme + " - " + marka?.Marke + " " + Model + ": " + Registracija;
            }
        }

    }
}