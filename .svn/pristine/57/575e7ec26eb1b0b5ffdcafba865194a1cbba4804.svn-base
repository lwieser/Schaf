using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Devis.Annotations;

namespace Devis.Controls
{
    /// <summary>
    ///     Logique d'interaction pour QuoteDetailToolbar.xaml
    /// </summary>
    public partial class QuoteDetailToolbar : INotifyPropertyChanged
    {
        private bool _canAddPackage;
        private bool _canAddEntry;
        private bool _canAddArticle;

        public QuoteDetailToolbar()
        {
            InitializeComponent();
            Container.DataContext = this;
        }


        public bool CanAddPackage
        {
            get { return _canAddPackage; }
            set
            {
                if (value.Equals(_canAddPackage)) return;
                _canAddPackage = value;
                OnPropertyChanged();
            }
        }

        public bool CanAddEntry
        {
            get { return _canAddEntry; }
            set
            {
                if (value.Equals(_canAddEntry)) return;
                _canAddEntry = value;
                OnPropertyChanged();
            }
        }

        public bool CanAddArticle
        {
            get { return _canAddArticle; }
            set
            {
                if (value.Equals(_canAddArticle)) return;
                _canAddArticle = value;
                OnPropertyChanged();
            }
        }

        public event EventHandler AddPackage;
        public event EventHandler AddEntry;
        public event EventHandler AddArticle;


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddPackage_Click(object sender, RoutedEventArgs e)
        {
            if (AddPackage != null)
                AddPackage(this, EventArgs.Empty);
        }

        private void AddEntry_Click(object sender, RoutedEventArgs e)
        {
            if (AddEntry != null)
                AddEntry(this, EventArgs.Empty);
        }

        private void AddArticle_Entry(object sender, RoutedEventArgs e)
        {
            if (AddArticle != null)
                AddArticle(this, EventArgs.Empty);
        }
    }
}