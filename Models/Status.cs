using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paup_2021_ServisVozila.Models
{
    [Table("status")]
    public class Status
    {
        [Key]
        public int id { get; set; }

        [Column("Status")]
        public string StatusAutomobila { get; set; }
    }
}