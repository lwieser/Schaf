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
        #region fields
        public event PropertyChangedEventHandler PropertyChanged;
        public static double TVA_RATE = 0.196;
        private double _salePrice;
        private double _discount;
        private double _discountRate;
        private double _posePrice;
        private double _refreshing;
        private double _refreshingCoef; 
        private double _downPayRate; 
        private double _taxeIncluded;
        private double _disbursed;
        private double _costPrice;
        private List<QuotePackage> _packages;
        #endregion

        public ObservableCollection<LineViewModel> Lines { get; private set; }
        public QuoteFooterViewModel() 
        {
            Load();
            AssignValues();
        }

        private void AssignValues()
        {
            Quote quote  = new QuoteRepository().GetQuote(int.Parse(App.Current.Properties["QuoteID"].ToString()));
            Disbursed = quote.Packages.Sum(package => package.Disbursed) ?? 0;
            CostPrice = Disbursed;
            SalePrice = quote.Packages.Sum(package => package.Price) ?? 0;
            PosePrice = 0;
            RefreshingCoef = 1;
            Refreshing = GrossPrice * RefreshingCoef - GrossPrice;
            Discount = 0;
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
        public double ToPay
        {
            get { return TotalNet - DownPayAmount; }
        }

        public double Profit
        {
            get {
                double profit = SalePrice - CostPrice;
                return (profit / SalePrice) * 100; ;
            }
        }

        public double CostPrice
        {
            get { return _costPrice; }
            set
            {
                if (Equals(value, _costPrice)) return;
                _costPrice = value;
                OnPropertyChanged();
            }
        }

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

        public double Disbursed
        {
            get { return _disbursed; }
            set
            {
                if (Equals(value, _disbursed)) return;
                _disbursed = value;
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
                _discountRate = value / TaxeFreeNetCom * 100;
                OnPropertyChanged("DiscountRate");
                UpdateAfterDiscount();
                OnPropertyChanged();
            }
        }

        private void UpdateAfterDiscount()
        {
            OnPropertyChanged("TaxeFreeFinal");
            OnPropertyChanged("TVA");
            OnPropertyChanged("TaxeIncluded");
            OnPropertyChanged("TaxeIncludedNet");
            OnPropertyChanged("DownPayAmount");
            OnPropertyChanged("ToPay");
        }

        public double DiscountRate
        {
            get { return _discountRate; }
             set
            {
                if (Equals(value, _discountRate)) return;
                _discountRate = value;
                _discount= _discountRate * TaxeFreeNetCom / 100;
                OnPropertyChanged("Discount");
                UpdateAfterDiscount();
                OnPropertyChanged();
            }
        }

        public double TaxeFreeNetCom
        {
            get { return GrossPrice; }
            set { }
        }

        public double TaxeFreeFinal
        {
            get
            {
                var result = TaxeFreeNetCom - Discount;
                return result;
            }
        }

        public double TaxeIncluded
        {
            get { return _taxeIncluded; }
            set
            {
                if (Equals(value, _taxeIncluded)) return;
                _taxeIncluded = value;
                OnPropertyChanged();
            }
        }

        public double TVA
        {
            get
            {
                return TaxeFreeFinal + (TaxeFreeFinal * TVA_RATE);                    ;
            }
        }

        public double GrossPrice
        {
            get { return SalePrice + PosePrice; }
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
            get { return GrossPrice; }
            set { }
        }

        public double TaxeIncludedNet
        {
            get
            {
                return TVA + TaxeFreeFinal;
            }
            set { }
        }

        public double DownPayAmount
        {
            get {
                if (_downPayRate == 0)
                    return 0;
                return (TaxeIncludedNet * _downPayRate) / 100;
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
                OnPropertyChanged("DownPayAmount");
                OnPropertyChanged("ToPay");
                OnPropertyChanged();
            }
        }

        public double TotalNet
        {
            get { return TaxeIncludedNet;}
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
