using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Paup_2021_ServisVozila.Models
{
    [Table("ovlasti")]
    public class Ovlast
    {
        [Key]
        public string Sifra { get; set; }
        public string Naziv { get; set; }
    }
}