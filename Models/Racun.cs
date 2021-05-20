using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Paup_2021_ServisVozila.Models
{
    [Table("racun")]
    public class Racun
    {
        [Key]
        [Display(Name = "ID racuna")]
        public int id { get; set; }

        [Display(Name="ID servisa")]
        [Required(ErrorMessage ="{0} je potreban")]
        [ForeignKey("servisiFK")]
        public int servis { get; set; }
        public virtual Servisi servisiFK { get; set; }

        [Display(Name = "Datum izdavanja računa")]
        [Required(ErrorMessage = "{0} je obavezan")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }

        [Column("Izdaje_osoba")]
        [Display(Name = "Izdaje")]
        [Required(ErrorMessage = "Ime radnika je potrebno!")]
        public string osoba { get; set; }

        [Display(Name = "Cijena")]
        [Required(ErrorMessage = "{0} je obavezna")]
        public int cijena { get; set; }
    }
}