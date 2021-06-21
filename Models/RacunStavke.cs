using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Paup_2021_ServisVozila.Models
{
    [Table("racun_stavke")]
    public class RacunStavke
    {
        [Key]
        [Display(Name = "Id stavke")]
        public int id { get; set; }

        [Column("id_racuna")]
        [Display(Name = "Id računa")]
        [Required(ErrorMessage = "{0} je obavezan")]
        [ForeignKey("RacunFK")]
        public int idRacuna { get; set; }
        public virtual Racun RacunFK { get; set; }
        
        [Column("redni_broj_stavke")]
        [Display(Name = "Redni broj stavke")]
        [Required(ErrorMessage = "{0} je obavezan")]
        public int redniBroj { get; set; }

        [Column("sifra_artikla")]
        [Display(Name = "Šifra artikla")]
        [Required(ErrorMessage = "{0} je obavezna")]
        [ForeignKey("ArtiklFK")]
        public int sifraArtikla { get; set; }
        public virtual Artikli ArtiklFK { get; set; }

        [Display(Name = "Količina")]
        [Required(ErrorMessage = "{0} je obavezna")]
        public int kolicina { get; set; }

        //potrebno izracunati iznos preko cijene artikla te kolicine
        [Display(Name = "Iznos")]
        public int iznos { get; set; }
    }
}