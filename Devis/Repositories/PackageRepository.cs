using Devis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devis.Repositories
{
    public class PackageRepository : IRepositoryGeneric<QuotePackage>
    {
        public ICollection<QuotePackage> GetAll()
        {
            throw new NotImplementedException();
        }

        public void AddEntry(QuotePackage package, string entryLabel, int numerotation)
        {
            using (Entities context = new Entities())
            {
                QuotePackage pack = context.Packages.First(x => x.Id == package.Id);
                pack.Entries.Add(new QuoteEntry(numerotation,entryLabel));
                context.SaveChanges();
            }
        }

        internal void UpgradeNumerotationFrom(QuotePackage package, int numerotation)
        {
            using (Entities context = new Entities())
            {
                foreach (QuoteEntry entry in package.Entries.Where(x => x.Numerotation >= numerotation))
                {
                    QuoteEntry pack = context.Entries.First(x => x.Id == package.Id);
                    entry.Numerotation++;
                    context.SaveChanges();
                }
            }
        }
    }
}
