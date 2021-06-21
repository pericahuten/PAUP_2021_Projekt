using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Paup_2021_ServisVozila.Models
{
    [Table("artikli")]
    public class Artikli
    {
        [Key]
        [Display(Name = "ID artikla")]
        public int id { get; set; }
        [Display(Name = "Naziv artikla")]
        [Required(ErrorMessage = "{0} je obavezan")]
        public string Naziv { get; set; }

        [Column("jedinica_mjere")]
        [Display(Name = "Jedinica mjere")]
        [Required(ErrorMessage = "{0} je obavezna")]
        public string jedinicaMjere { get; set; }

        [Display(Name = "Cijena artikla")]
        [Required(ErrorMessage = "{0} je obavezna")]
        [Range(1,99999,ErrorMessage = "Vrijednost nije valjana")]
        public int cijena { get; set; }
    }
}