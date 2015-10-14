using Devis.Models;
using Devis.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devis.ViewModels
{
    public class QuoteHeaderViewModel : NotifyPropertyChangedGeneric
    {
        private Client _client;

        #region Properties
        public Client Client
        {
            get { return _client; }
            set
            {
                _client = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public QuoteHeaderViewModel()
        {
            Client = new QuoteRepository().GetQuote(int.Parse(App.Current.Properties["QuoteID"].ToString())).Client;
        }
    }
}
