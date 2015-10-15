using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Devis.Annotations;
using Devis.Models;

namespace Devis.ViewModels
{
    public class LineViewModel : INotifyPropertyChanged
    {
        private string _numbering;
        private string _reference;
        private string _label;
        private double? _progress;
        private double? _price;
        private int? _quantity;
        private string _unit;
        private double? _amount;
        private bool _isEmpty;
        private double? _disbursed;


        public QuoteItem UnderlyingObject { get; protected set; }


        public static LineViewModel Empty
        {
            get { return new LineViewModel {IsEmpty = true}; }
        }
        
        public virtual string Unit
        {
            get { return _unit; }
            set {
                if (value == _unit) return;
                _unit = value;
                OnPropertyChanged();
            }
        }


        public virtual double? Margin
        {
            get
            {
                if (UnderlyingObject != null)
                    return UnderlyingObject.Margin;
                return null;
            }
        }

        public virtual string Numbering
        {
            get { return _numbering; }
            set
            {
                if (value == _numbering) return;
                _numbering = value;
                OnPropertyChanged();
                OnPropertyChanged("Level");
            }
        }

        public virtual double? Disbursed
        {
            get { return _disbursed; }
            set
            {
                if (value == _disbursed) return;
                _disbursed = value;
                OnPropertyChanged();
                OnPropertyChanged("Margin");
            }
        }

        public virtual string Reference
        {
            get { return _reference; }
            set
            {
                if (value == _reference) return;
                _reference = value;
                OnPropertyChanged();
            }
        }

        public virtual string Label
        {
            get { return _label; }
            set
            {
                if (value == _label) return;
                _label = value;
                OnPropertyChanged();
            }
        }

        public double? Progress
        {
            get { return _progress; }
            set
            {
                if (value.Equals(_progress)) return;
                _progress = value;
                OnPropertyChanged();
            }
        }

        public virtual double? Price
        {
            get { return _price; }
            set
            {
                if (value.Equals(_price)) return;
                _price = value;
                OnPropertyChanged();
                OnPropertyChanged("Margin");
            }
        }

        public  virtual int? Quantity
        {
            get { return _quantity; }
            set
            {
                if (value.Equals(_quantity)) return;
                _quantity = value;
                OnPropertyChanged();
            }
        }

        public virtual double? Amount
        {
            get { return _amount; }
            set
            {
                if (value.Equals(_amount)) return;
                _amount = value;
                OnPropertyChanged();
            }
        }

        public bool IsEmpty
        {
            get { return _isEmpty; }
            set
            {
                if (value.Equals(_isEmpty)) return;
                _isEmpty = value;
                OnPropertyChanged();
            }
        }

        public int Level
        {
            get
            {
                if (!string.IsNullOrEmpty(Numbering))
                {
                    if (Regex.Match(Numbering, "^[0-9]+$").Success)
                        return 1;

                    if (Regex.Match(Numbering, "^[0-9]+\\.[0-9]$").Success)
                        return 2;
                    
                }


                return IsEmpty ?  0 : 3;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        internal void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}