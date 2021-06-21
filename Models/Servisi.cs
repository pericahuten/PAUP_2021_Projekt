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
        [Column("ID_Servisa")]
        [Display(Name ="Id servisa")]
        public int Id { get; set; }

        [Display(Name = "Id automobila")]
        [Required(ErrorMessage = "{0} je obavezan!!")]
        [ForeignKey("idAuto")]
        public string AutoID { get; set; }
        public virtual Automobili idAuto { get; set; }
        

        [Column("Datum_kreiranja_zahtjeva")]
        [Display(Name =("Datum kreiranja"))]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DatumKreiranja { get; set; }

        [Column("Datum_pocetka_servisa")]
        [Display(Name ="Datum početka servisa")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DatumPocetka { get; set; }

        [Column("Datum_zavrsetka_servisa")]
        [Display(Name ="Datum završetka servisa")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DatumZavrsetka { get; set; }

        [Column("Napomena_vlasnika")]
        [Display(Name ="Napomena vlasnika")]
        public string NapomenaVlasnika { get; set; }

        [Column("Napomena_servisera")]
        [Display(Name ="Napomena servisera")]
        public string NapomenServisera { get; set; }

        [Display(Name ="Kilometraža")]
        public int Kilometraza { get; set; }

        [Display(Name ="Serviser")]
        public string Serviser { get; set; }

        [Display(Name ="Status servisa")]
        [Column("Status")]
        [ForeignKey("StatusId")]
        public int StatusAuta { get; set; }
        public virtual Status StatusId { get; set; }








    }
}