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
            return new Entities().Quotes.Include("Client").Include("Client.Civility").ToList();
        }

        public Quote GetQuote(int id)
        {
            using (Entities context = new Entities())
            {
                Quote quote = context.Quotes.First(x => x.Id == id);
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
    }
}
