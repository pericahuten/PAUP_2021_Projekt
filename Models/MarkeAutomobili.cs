using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paup_2021_ServisVozila.Models
{
    [Table("marke_automobili")]
    public class MarkeAutomobili
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Display(Name = "Marka automobila")]
        [Required(ErrorMessage = "{0} je obavezna")]
        public string Marke { get; set; }
    }
}