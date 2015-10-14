using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devis.Models;

namespace Devis.Repositories
{
    public class MemoryQuoteListRepository : IQuoteRepostory
    {
        private List<Quote> _quotes = new List<Quote>()
        {
            /*
            new Quote(00018967,"FAYOLLE & CO") {
                //ClientCode = 00005721,
                Date = new DateTime(2015,09,29),
                RevivalDate = new DateTime(2015,10,7),
                OwnerCode = "VS",
                State = Quote.StateEnum.Remis,
                Subject = "BOBIGNY MAISON DE LA CULTURE LOT METALLIERIE SERRURERIE"
            },
            new Quote(00018966,"MARCHAL") {
                Date = new DateTime(2015,09,29),
                //Client.Code = 00001330,
                RevivalDate = new DateTime(2015,10,07),
                ClientCivility = Quote.CivilityEnum.Ets,
                OwnerCode = "VS",
                State = Quote.StateEnum.Remis,
                Subject = "REF : CAILLEBOTIS POUR CASIERS A SKI"

            },
            new Quote(00018963,"EURO DISNEY Associés SCA") {
                Code = 00018963,
                Date = new DateTime(2015,09,28),
                //Client.= 00006832,
                RevivalDate = new DateTime(2015,06,10),
                OwnerCode = "VS",
                State = Quote.StateEnum.Remis,
                Subject = "REF : LE PLAZZA - VIP LOUNGE - CHEESY FRANCE 170 METALLERIE SERRURERIE TRAVAUX SUPPLEMENTAIRES"
            },*/
        };

        private List<QuotePackage> _packages = new List<QuotePackage>()
        {
            new QuotePackage()
            {
                Label = "Lot 1",
            }
        };

        public MemoryQuoteListRepository()
        {
            _quotes.ForEach(QuotePackage => QuotePackage.Packages = (_packages));
        }

        public ICollection<Quote> Quotes
        {
            get
            {
                return _quotes;
            }
        }

        public Quote GetQuote(int id)
        {
            return _quotes.Find(x => x.Id == id);
        }

        public int CreateQuote(string label)
        {
            int lastIndex = _quotes.Max(x => x.Id);
            int code = lastIndex++;
            //_quotes.Add(new Quote(code, label));
            return code;
        }

        public void DeleteQuote(int id)
        {
            Quote quote = _quotes.Find(x => x.Id == id) ;
            _quotes.Remove(quote);
        }

        public int CreateQuote(string code, string subject, int clientId)
        {
            throw new NotImplementedException();
        }

        public ICollection<Quote> GetAll()
        {
            throw new NotImplementedException();
        }

        public string SuggestCode()
        {
            throw new NotImplementedException();
        }

        public void Update(Quote obj)
        {
            throw new NotImplementedException();
        }
    }
}
