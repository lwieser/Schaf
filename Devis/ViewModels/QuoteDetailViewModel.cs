using Devis.Annotations;
using Devis.Models;
using System.Linq;
using Devis.Repositories;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Devis.ViewModels
{
    public class QuoteDetailViewModel : INotifyPropertyChanged
    {
        private int _id;
        private Quote _quote;
        private ObservableCollection<LineViewModel> _lines;

        public QuoteDetailViewModel()
        {
            _id = int.Parse(App.Current.Properties["QuoteID"].ToString());
            IQuoteRepostory repository = new QuoteRepository();
            var tmp = Load();
            Lines = new ObservableCollection<LineViewModel>(tmp);

            Quote = repository.GetQuote(_id);
            Lines = Quote.GetLines();

        }

        private IEnumerable<LineViewModel> Load()
        {
            var lines = new List<LineViewModel>();
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Devis.Controls.Design.sampleData.txt";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                const int numberOfItems = 8;
                string line;
                NumberFormatInfo nfi = new NumberFormatInfo();
                nfi.NumberDecimalSeparator = ".";
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(';');
                    var model = new LineViewModel();
                    if (parts.Length >= numberOfItems)
                    {
                        model.Numbering = parts[0];
                        model.Reference = parts[1];
                        model.Label = parts[2];

                        double progress;
                        if (double.TryParse(parts[3], out progress))
                            model.Progress = progress;

                        double price;
                        if (double.TryParse(parts[4], NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo,
                            out price))
                            model.Price = price;

                        int qty;
                        if (int.TryParse(parts[5], out qty))
                            model.Quantity = qty;

                        model.Unit = parts[6];

                        double amount;
                        if (double.TryParse(parts[7], NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo,
                            out amount))
                            model.Amount = amount;
                    }
                    else
                        model.IsEmpty = true;
                        
                    lines.Add(model);
                }
            }

            return lines;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #region Property 
        public string QuoteName
        {
            get { return _quote.Subject; }
        }

        public Quote Quote
        {
            get { return _quote; }
            set
            {
                if (Equals(value, _quote)) return;
                _quote = value;
                OnPropertyChanged();
            }
        }


        public ObservableCollection<LineViewModel> Lines
        {
            get { return _lines; }
            set
            {
                _lines = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region  INotifyPropertyChanged

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
