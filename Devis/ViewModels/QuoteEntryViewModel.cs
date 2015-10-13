using Devis.Models;

namespace Devis.ViewModels
{
    public class QuoteEntryViewModel : QuoteItemViewModel
    {
        public QuoteEntryViewModel(QuoteItem item) : base(item)
        {
        }

        protected override void ChildNotification(QuoteItemViewModel child, string propertyName)
        {
            if (propertyName == "Amount")
            {
                OnPropertyChanged("Price");
                OnPropertyChanged("Amount");
                NotifyParentProperty("Amount");
            }
        }
    }
}