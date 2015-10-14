using Devis.Annotations;
using Devis.Models;
using Devis.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Devis.ViewModels
{
    public class QuoteFooterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public static double TVA_RATE = 0.196;
        private double _salePrice;
        private double _grossPrice;
        private double _tva;
        private double _discount;
        private double _discountRate;
        private double _posePrice;
        private double _refreshing;
        private double _grossPriceRefreshed;
        private double _taxeFreeNetCom;
        private double _refreshingCoef; 
        private double _downPayRate; 
        private double _downPayAmount;
        private double _totalNet;
        private double _taxeIncluded;
        private double _taxeIncludedNet;
        private double _taxeFreeFinal; 
        private List<QuotePackage> _packages;

        public ObservableCollection<LineViewModel> Lines { get; private set; }

        public QuoteFooterViewModel() 
        {
            Load();
            AssignValues();
        }

        private void AssignValues()
        {
            Quote quote  = new QuoteRepository().GetQuote(int.Parse(App.Current.Properties["QuoteID"].ToString()));
            _salePrice = quote.Packages.Sum(package => package.Price) ?? 0;
            _posePrice = 0;
            _grossPrice = _salePrice + _posePrice;
            _refreshingCoef = 1;
            _refreshing = _grossPrice * _refreshingCoef - _grossPrice;
            _grossPriceRefreshed = _grossPrice;
            _discount = 0;
            _taxeFreeNetCom = _grossPriceRefreshed;
            _taxeFreeFinal = _taxeFreeNetCom - _discount;
            _tva = _taxeFreeFinal * TVA_RATE;
            _taxeIncluded = _taxeFreeFinal + _tva;
            _taxeIncludedNet = _taxeIncluded;
            _totalNet = _taxeIncluded;
        }

        private void Load()
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
                _packages = new List<QuotePackage>();
                QuotePackage currentPackage = new QuotePackage();
                QuoteEntry currentEntry = new QuoteEntry();

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

                    if(!model.IsEmpty)
                    {
                        if (String.IsNullOrEmpty(parts[0])) // article 
                        {
                            currentEntry.AddArticle(new QuoteArticle(1,"label")
                            {
                                Price = model.Price,
                                Quantity = model.Quantity
                            });
                        }
                        if (parts[0].Contains('.')) // entry 
                        {
                            //if bnot first 
                            if (model.Numbering.Substring(1, 2) != ".1")
                            {
                                currentPackage.AddEntry(currentEntry);
                            }
                            currentEntry = new QuoteEntry()
                            {
                                Quantity = model.Quantity
                            };
                        }
                        else // package 
                        {
                            //If  not first 
                            if (model.Numbering != "1")
                            {
                                currentPackage.AddEntry(currentEntry);
                                _packages.Add(currentPackage);
                            }

                            currentPackage = new QuotePackage();
                        }
                    }
                }
                _packages.Add(currentPackage);
            }
        }

        #region Properties
        public double SalePrice
        {
            get { return _salePrice; }
            set
            {
                if (Equals(value, _salePrice)) return;
                _salePrice = value;
                OnPropertyChanged();
            }
        }

        public double PosePrice
        {
            get { return _posePrice; }
            set
            {
                if (Equals(value, _posePrice)) return;
                _posePrice = value;
                OnPropertyChanged();
            }
        }

        public double Refreshing
        {
            get { return _refreshing; }
            set
            {
                if (Equals(value, _refreshing)) return;
                _refreshing = value;
                OnPropertyChanged();
            }
        }

        public double Discount
        {
            get { return _discount; }
            set
            {
                if (Equals(value, _discount)) return;
                _discount = value;
                DiscountRate = value / _taxeFreeNetCom * 100;
                TaxeFreeFinal = _taxeFreeNetCom - _discount;
                OnPropertyChanged();
            }
        }

        public double DiscountRate
        {
            get { return _discountRate; }
             set
            {
                if (Equals(value, _discountRate)) return;
                _discountRate = value;
                _discount= _discountRate * _taxeFreeNetCom / 100;
                TaxeFreeFinal = _taxeFreeNetCom - _discount;
                OnPropertyChanged("Discount");
                OnPropertyChanged();
            }
        }


        public double TaxeFreeNetCom
        {
            get { return _taxeFreeNetCom; }
             set
            {
                if (Equals(value, _taxeFreeNetCom)) return;
                _taxeFreeNetCom = value;
                TVA = _taxeFreeNetCom * TVA_RATE;
                TaxeFreeFinal = _taxeFreeNetCom + TVA;
                OnPropertyChanged();
            }
        }


        public double TaxeFreeFinal
        {
            get
            {
                return _taxeFreeFinal;
            }
            set
            {
                if (Equals(value, _taxeFreeFinal)) return;
                _taxeFreeFinal = value;
                TVA = _taxeFreeFinal * TVA_RATE;
                OnPropertyChanged();
            }
        }

        public double TaxeIncluded
        {
            get { return _taxeIncluded; }
            set
            {
                if (Equals(value, _taxeIncluded)) return;
                _taxeIncluded = value;
                TaxeIncludedNet = value;
                OnPropertyChanged();
            }
        }

        public double TVA
        {
            get
            {
                return _tva;                    ;
            }
            set
            {
                if (Equals(value, _tva)) return;
                _tva = value;
                TaxeIncluded = _taxeFreeFinal + value;
                OnPropertyChanged();
            }
        }

        public double GrossPrice
        {
            get { return _grossPrice; }
             set
            {
                if (Equals(value, _grossPrice)) return;
                _grossPrice = value;
                OnPropertyChanged();
            }
        }

        public double RefreshingCoef
        {
            get { return _refreshingCoef; }
            set
            {
                if (Equals(value, _refreshingCoef)) return;
                _refreshingCoef = value;
                OnPropertyChanged();
            }
        }

        public double GrossPriceRefreshed
        {
            get { return _grossPriceRefreshed; }
            set
            {
                if (Equals(value, _grossPriceRefreshed)) return;
                _grossPriceRefreshed = value;
                OnPropertyChanged();
            }
        }

        public double TaxeIncludedNet
        {
            get
            {
                return _taxeIncludedNet;
            }
            set
            {
                if (Equals(value, _taxeIncludedNet)) return;
                _taxeIncludedNet = value;
                OnPropertyChanged();
            }
        }

        public double DownPayAmount
        {
            get { return _downPayAmount; }
            set
            {
                if (Equals(value, _downPayAmount)) return;
                _downPayAmount = value;
                OnPropertyChanged();
            }
        }

        public double DownPayRate
        {
            get
            {
                return _downPayRate;
            }
            set
            {
                if (Equals(value, _downPayRate)) return;
                _downPayRate = value;
                DownPayAmount = _taxeIncludedNet / _downPayRate;
                OnPropertyChanged();
            }
        }

        public double TotalNet
        {
            get
            {
                return _totalNet;
            }
            set
            {
                if (Equals(value, _totalNet)) return;
                _totalNet = value;
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
