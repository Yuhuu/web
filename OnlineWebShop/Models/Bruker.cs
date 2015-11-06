using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace webshop.Models
{
    public class bruker
    {
        [Required(ErrorMessage="Trenger et navn")]
        public string Navn { get; set; }
        [Required(ErrorMessage="Trenger et passord")]
        public string Passord { get; set; }
    }

    public class dbBruker
    {
        [Key]
        public string Navn { get; set; }
        public byte[] Passord { get; set; }
        
    }

    public class BrukerContext : DbContext
    {
        public BrukerContext() : base("name=Bruker")
        {
            Database.CreateIfNotExists();
        }

        public DbSet<dbBruker> Brukere { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

}