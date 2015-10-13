using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Devis.Annotations;
using Devis.Models;
using Devis.Tools;
using Devis.ViewModels;

namespace Devis.Controls
{
    /// <summary>
    /// Logique d'interaction pour QuoteFooter.xaml
    /// </summary>
    public partial class QuoteFooter : INotifyPropertyChanged
    {
        private QuoteFooterViewModel _viewModel;

        public QuoteFooter()
        {
            InitializeComponent();
            _viewModel = new QuoteFooterViewModel();
            Container.DataContext = this;
        }

        public double Debourse
        {
            get { return _viewModel.Debourse; }
            private set
            {
                if (Equals(value, _viewModel.Debourse)) return;
                _viewModel.Debourse = value;
                OnPropertyChanged();
            }
        }

        public double SalePrice
        {
            get { return _viewModel.SalePrice; }
        }

        public double PosePrice
        {
            get { return _viewModel.PosePrice; }
            private set
            {
                if (Equals(value, _viewModel.PosePrice)) return;
                _viewModel.PosePrice = value;
                OnPropertyChanged();
            }
        }

        public double Refreshing
        {
            get { return _viewModel.Refreshing; }
            private set
            {
                if (Equals(value, _viewModel.Refreshing)) return;
                _viewModel.Refreshing = value;
                OnPropertyChanged();
            }
        }

        public double Discount
        {
            get { return _viewModel.Discount; }
            private set
            {
                if (Equals(value, _viewModel.Discount)) return;
                _viewModel.Discount = value;
                DiscountRate = value / TaxeFreeNetCom * 100;
                TaxeFreeFinal = TaxeFreeNetCom - Discount;
                OnPropertyChanged();
            }
        }

        public double DiscountRate
        {
            get { return _viewModel.DiscountRate; }
            private set
            {
                if (Equals(value, _viewModel.DiscountRate)) return;
                _viewModel.DiscountRate = value;
                Discount = _viewModel.DiscountRate * TaxeFreeNetCom / 100;
                OnPropertyChanged();
            }
        }


        public double TaxeFreeNetCom
        {
            get { return _viewModel.TaxeFreeNetCom; }
            private set
            {
                if (Equals(value, _viewModel.TaxeFreeNetCom)) return;
                _viewModel.TaxeFreeNetCom = value;
                TVA = TaxeFreeNetCom * QuoteFooterViewModel.TVA_RATE;
                TaxeFreeFinal = TaxeFreeNetCom + TVA;
                OnPropertyChanged();
            }
        }


        public double TaxeFreeFinal
        {
            get
            {
                return _viewModel.TaxeFreeFinal;
            }
            set
            {
                if (Equals(value, _viewModel.TaxeFreeFinal)) return;
                _viewModel.TaxeFreeFinal = value;
                TVA = TaxeFreeFinal * QuoteFooterViewModel.TVA_RATE;
                OnPropertyChanged();
            }
        }

        public double TaxeIncluded
        {
            get { return _viewModel.TaxeIncluded; }
            set
            {
                if (Equals(value, _viewModel.TaxeIncluded)) return;
                _viewModel.TaxeIncluded = value;
                TaxeIncludedNet = value;
                OnPropertyChanged();
            }
        }

        public double TVA
        {
            get
            {
                return _viewModel.TVA;
            }
            set
            {
                if (Equals(value, _viewModel.TVA)) return;
                _viewModel.TVA = value;
                TaxeIncluded = _viewModel.TaxeFreeFinal + value;
                OnPropertyChanged();
            }
        }

        public double GrossPrice
        {
            get { return _viewModel.GrossPrice; }
        }

        public double RefreshingCoef
        {
            get { return _viewModel.RefreshingCoef; }
            private set
            {
                if (Equals(value, _viewModel.RefreshingCoef)) return;
                _viewModel.RefreshingCoef = value;
                OnPropertyChanged();
            }
        }

        public double GrossPriceRefreshed
        {
            get { return _viewModel.GrossPriceRefreshed; }
            private set
            {
                if (Equals(value, _viewModel.GrossPriceRefreshed)) return;
                _viewModel.GrossPriceRefreshed = value;
                OnPropertyChanged();
            }
        }

       

        public double TaxeIncludedNet
        {
            get
            {
                return _viewModel.TaxeIncludedNet;
            }
            set
            {
                if (Equals(value, _viewModel.TaxeIncludedNet)) return;
                _viewModel.TaxeIncludedNet = value;
                OnPropertyChanged();
            }
        }

        public double DownPayAmount { get { return _viewModel.DownPayAmount; } set
            {
                if (Equals(value, _viewModel.DownPayAmount)) return;
                _viewModel.DownPayAmount = value;
                OnPropertyChanged();
            }
        }

        public double DownPayRate {
            get
            {
                return _viewModel.DownPayRate;
            }
            set
            {
                if (Equals(value, _viewModel.DownPayRate)) return;
                _viewModel.DownPayRate = value;
                DownPayAmount = TaxeIncludedNet / _viewModel.DownPayRate;
                OnPropertyChanged();
            }
        }

        public double TotalNet
        {
            get
            {
                return _viewModel.TotalNet;
            }
            set
            {
                if (Equals(value, _viewModel.TotalNet)) return;
                _viewModel.TotalNet = value;
                OnPropertyChanged();
            }
        }

        #region  INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
