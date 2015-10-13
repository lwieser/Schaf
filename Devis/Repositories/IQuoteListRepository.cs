using Devis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devis.Repositories
{
    interface IQuoteRepostory : IRepositoryGeneric<Quote>
    {
        Quote GetQuote(int id);
        int CreateQuote(string code, string subject, int clientId);
        void DeleteQuote(int id);
        string SuggestCode();
    }
}
