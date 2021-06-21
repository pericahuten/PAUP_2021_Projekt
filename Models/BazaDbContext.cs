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

        public DbSet<MarkeAutomobili> PopisMarka{ get; set; }

        public DbSet<Racun> PopisRacuna { get; set; }

        public DbSet<RacunStavke> PopisStavki { get; set; }

        public DbSet<Artikli> PopisArtikla { get; set; }

        public DbSet<Ovlast> PopisOvlasti { get; set; }

        public DbSet<Status> PopisStatusa { get; set; }




    }
}