using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Paup_2021_ServisVozila.Models
{
    [Table("korisnici")]
    public class Korisnik
    {
        [Key]
        [Display(Name = "ID Korisnika")]
        public string Id { get; set; }

        [Display(Name = "Korisničko ime")]
        [Required(ErrorMessage = "{0} je obavezno")]
        [Column("korisnicko_ime")]
        public string KorisnickoIme { get; set; }

        [Display(Name = "Ime")]
        [Required(ErrorMessage = "{0} je obavezno")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "{0} mora biti duljine minimalno {2} a maksimalno {1} znakova")]
        public string Ime { get; set; }

        [Display(Name = "Prezime")]
        [Required(ErrorMessage = "{0} je obavezno")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "{0} mora biti duljine minimalno {2} a maksimalno {1} znakova")]
        public string Prezime { get; set; }

        public string PrezimeIme
        {
            get
            {
                return Prezime + " " + Ime;
            }
        }

        [Display(Name = "Mobilni broj")]
        [Required(ErrorMessage = "{0} je potreban!")]
        public int MobilniBroj { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} je obavezan!")]
        [EmailAddress]
        public string Email { get; set; }

        public string Lozinka { get; set; }

        [Display(Name = "Administrator")]
        [Column("isAdmin")]
        public bool Admin { get; set; }

        [Display(Name = "Lozinka")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "{0} je obavezna")]
        [NotMapped]
        public string LozinkaUnos1 { get; set; }

        [Display(Name = "Ponovljena lozinka")]
        [DataType(DataType.Password)]
        [Required]
        [NotMapped]
        [Compare("LozinkaUnos2", ErrorMessage = "Lozinke moraju biti jednake")]
        public string LozinkaUnos2 { get; set; }
    }
}