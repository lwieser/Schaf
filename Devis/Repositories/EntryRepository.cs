using Devis.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devis.Models;

namespace Devis.Repositories
{
    public class EntryRepository : IRepositoryGeneric<QuoteEntry>
    {
        internal void UpgradeNumerotationFrom(QuoteEntry entry, int numerotation)
        {
            using (Entities context = new Entities())
            {
                foreach (QuoteArticle article in entry.Articles.Where(x => x.Numerotation >= numerotation))
                {
                    QuoteArticle art = context.Articles.First(x => x.Id == article.Id);
                    art.Numerotation++;
                    context.SaveChanges();
                }
            }
        }

        internal void AddEntry(QuoteEntry entry, string label, int numerotation)
        {
            using (Entities context = new Entities())
            {
                QuoteEntry ent = context.Entries.First(x => x.Id == entry.Id);
                ent.Articles.Add(new QuoteArticle(numerotation, label));
                context.SaveChanges();
            }
        }

        public ICollection<QuoteEntry> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(QuoteEntry obj)
        {
            using (Entities entities = new Entities())
            {
                QuoteEntry entry = entities.Entries.First(x => x.Id == obj.Id);
                entry.Quantity = obj.Quantity;
                entities.SaveChanges();
            }
        }
    }
}
