using Devis.Annotations;
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

        public override string Label
        {
            get { return UnderlyingObject.Label; }
            set
            {
                UnderlyingObject.Label = value;
                OnPropertyChanged();
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
                NotifyParentProperty("Amount");
            }
        }

        public override int? Quantity
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