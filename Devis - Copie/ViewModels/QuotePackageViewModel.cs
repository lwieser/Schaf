using Devis.Models;

namespace Devis.ViewModels
{
    public class QuotePackageViewModel : QuoteItemViewModel
    {
        public QuotePackageViewModel(QuoteItem item) : base(item)
        {
        }

        public override string Reference
        {
            get { return Numbering; }

            set { /**/ }
        }

        public override string Numbering
        {
            get { return base.Numbering; }
            set
            {
                base.Numbering = value;
                OnPropertyChanged("Reference");
            }
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