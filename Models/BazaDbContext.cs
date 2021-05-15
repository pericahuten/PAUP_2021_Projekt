using MySql.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Paup_2021_ServisVozila.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class BazaDbContext : DbContext
    {
        public DbSet<Korisnik> PopisKorisnika { get; set; }

        public DbSet<Automobili> PopisAutomobila { get; set; }

        public DbSet<Servisi> PopisServisa { get; set; }

    }
}