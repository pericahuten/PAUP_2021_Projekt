using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paup_2021_ServisVozila.Models
{
    [Table("servisi")]
    public class Servisi
    {
        [Key]
        [Display(Name ="Id servisa")]
        public int Id { get; set; }

        [Display(Name ="Id automobila")]
        [Required(ErrorMessage ="{0} je obavezan!!")]
        [StringLength(17, MinimumLength =17, ErrorMessage ="{0} mora biti duljine {1} znakova")]
        [ForeignKey("AutoId")]
        public string AutoId { get; set; } //fali virtual klasa
        

        [Column("Datum_kreiranja_zahtjeva")]
        [Display(Name =("Datum kreiranja"))]
        public DateTime DatumKreiranja { get; set; }

        [Column("Datum_pocetka_servisa")]
        [Display(Name ="Datum pocetka servisa")]
        public DateTime DatumPocetka { get; set; }

        [Column("Datum_zavrsetka_servisa")]
        [Display(Name ="Datum zavrsetka servisa")]
        public DateTime DatumZavrsetka { get; set; }

        [Column("Napomena_vlasnika")]
        [Display(Name ="Napomena vlasnika")]
        public string NapomenaVlasnika { get; set; }

        [Column("Napomena_servisera")]
        [Display(Name ="Napomena servisera")]
        public string NapomenServisera { get; set; }

        [Display(Name ="Kilometraza")]
        public int Kilometraza { get; set; }

        [Display(Name ="Serviser")]
        public int Serviser { get; set; }

        [ForeignKey("Status Id")]
        [Display(Name ="Status servisa")]
        public int Status { get; set; }








    }
}