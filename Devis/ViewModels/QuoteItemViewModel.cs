﻿using Devis.Annotations;
using Devis.Models;

namespace Devis.ViewModels
{
    public class QuoteItemViewModel : LineViewModel
    {
        protected QuoteItemViewModel(QuoteItem item)
        {
            UnderlyingObject = item;
            IsEmpty = false;
        }

        protected virtual void NotifyParentProperty(string propertyName)
        {
            if(Parent != null)
                Parent.ChildNotification(this, propertyName);
        }

        protected virtual void ChildNotification(QuoteItemViewModel child, string propertyName)
        {
            
        }

        public QuoteItemViewModel Parent { get; set; }

        public override string Reference
        {
            get { return UnderlyingObject.Reference ??
                    (UnderlyingObject.Numerotation != 0 ? UnderlyingObject.Numerotation.ToString() : string.Empty); }
            set
            {
                UnderlyingObject.Reference = value;
                OnPropertyChanged();
            }
        }

        public override string Label
        {
            get { return UnderlyingObject.Label; }
            set
            {
                UnderlyingObject.Label = value;
                OnPropertyChanged();
            }
        }

        public override string Unit
        {
            get { return UnderlyingObject.Unit; }
            set
            {
                UnderlyingObject.Unit = value;
                OnPropertyChanged();
            }
        }

        public override double? Disbursed
        {
            get { return UnderlyingObject.Disbursed; }
            set
            {
                UnderlyingObject.Disbursed = value;
                OnPropertyChanged();
                OnPropertyChanged("Margin");
                NotifyParentProperty("Disbursed");
            }
        }

        public override double? Price
        {
            get { return UnderlyingObject.Price; }
            set
            {
                UnderlyingObject.Price = value;
                OnPropertyChanged();
                OnPropertyChanged("Amount");
                OnPropertyChanged("Margin");
                NotifyParentProperty("Amount");
            }
        }

        public override double? Quantity
        {
            get { return UnderlyingObject.Quantity; }
            set
            {
                UnderlyingObject.Quantity = value;
                OnPropertyChanged();
                OnPropertyChanged("Amount");
                NotifyParentProperty("Amount");
            }
        }

        public override double? Amount
        {
            get
            {
                if (Price.HasValue && Quantity.HasValue)
                    return Price * Quantity;

                return null;
            }

            set
            {
                /* No implementation */
            }
        }
    }
}