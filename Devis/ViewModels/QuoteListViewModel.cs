using Devis.Models;
using Devis.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devis.ViewModels
{
    public class QuoteListViewModel : NotifyPropertyChangedGeneric
    {
        #region fields
        private bool _isItemSelected = false;
        private ICollection<Quote> _quotes;
        #endregion

        #region Properties
        public ICollection<Quote> Quotes {
            get { return _quotes;  }
            set {_quotes = value; OnPropertyChanged(); }
        }
        public string Title { get; set; }

        public bool IsItemSelected {
            get { return _isItemSelected; }
            set {
                _isItemSelected = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public QuoteListViewModel()
        {
            Init();
        }

        internal void Reload()
        {
            Init();
        }

        private void Init()
        {
            IQuoteRepostory repository = new QuoteRepository();
            Title = "Devis";
            IsItemSelected = false;
            Quotes = repository.GetAll();
        }
    }    
}
