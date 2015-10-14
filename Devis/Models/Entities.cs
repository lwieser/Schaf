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
        public DbSet<QuotePackage> Packages{ get; set; }
        public DbSet<QuoteEntry> Entries { get; set; }
        public DbSet<QuoteArticle> Articles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
