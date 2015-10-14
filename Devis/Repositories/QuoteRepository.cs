using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devis.Models;
using System.Data.Entity;

namespace Devis.Repositories
{
    public class QuoteRepository : IQuoteRepostory
    {
        public int CreateQuote(string code, string subject, int clientId)
        {
            using (Entities context = new Entities())
            {
                Quote nQuote = new Quote(code, subject);
                ClientRepository repo = new ClientRepository();
                nQuote.Client = repo.Get(clientId);
                context.Quotes.Add(nQuote);
                context.Clients.Attach(nQuote.Client);
                context.SaveChanges();
                return nQuote.Id;
            }
        }

        internal void AddPackage(int quoteId, int numerotation, string packageLabel)
        {
            using (Entities context = new Entities())
            {
                QuotePackage package = new QuotePackage(packageLabel, numerotation);
                Quote quote = context.Quotes.First(x => x.Id == quoteId);
                quote.Packages.Add(package);
                context.SaveChanges();
            }
        }

        internal void UpgradeNumerotationFrom(Quote quote,int numerotation)
        {
            using (Entities context = new Entities())
            {
                foreach(QuotePackage package in quote.Packages.Where(x => x.Numerotation >= numerotation))
                {
                    QuotePackage pack = context.Packages.First(x => x.Id == package.Id);
                    pack.Numerotation++;
                    context.SaveChanges();
                }
            }
        }

        public void DeleteQuote(int id)
        {
            using (Entities context = new Entities())
            {
                context.Quotes.Remove(context.Quotes.First(x => x.Id == id));
                context.SaveChanges();
            }
        }

        public ICollection<Quote> GetAll()
        {
            return new Entities().Quotes
                .Include("Client")
                .Include("Client.Civility")
                .Include("Packages")
                .Include("Packages.Entries")
                .Include("Packages.Entries.Articles")
                .ToList();
        }

        public Quote GetQuote(int id)
        {
            using (Entities context = new Entities())
            {
                Quote quote = GetAll().First(x => x.Id == id);

                quote.Packages = quote.Packages.OrderBy(p => p.Numerotation).ToList();
                return quote;
            }
        }

        public string SuggestCode()
        {
            ICollection<Quote> quotes = GetAll();
            int currentCode = quotes.Count == 0 ? 1 : quotes.Max(x => int.Parse(x.Code)) + 1;
            int totalLength = 8;
            string code = currentCode.ToString();
            code = code.PadLeft(totalLength, '0');
            return code;
        }

        internal void Flush(int quoteId)
        {
            using (Entities context = new Entities())
            {
                Quote quote = GetQuote(quoteId);

                foreach(QuotePackage package in quote.Packages)
                {
                    context.Packages.Attach(package);
                    context.Packages.Remove(package);
                    context.SaveChanges();
                }
            }
        }

        public void Update(Quote obj)
        {
            throw new NotImplementedException();
        }
    }
}
