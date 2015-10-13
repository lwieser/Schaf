using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Data.Entity;


namespace Devis.Models
{
    public partial class Entities : DbContext
    {

        public Entities() : base("name=SchaffnerModel")
        {

        }

        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
