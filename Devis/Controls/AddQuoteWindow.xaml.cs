using Devis.Annotations;
using Devis.Models;
using Devis.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Devis.Controls.Design
{
    /// <summary>
    /// Logique d'interaction pour AddQuoteWindow.xaml
    /// </summary>
    public partial class AddQuoteWindow : INotifyPropertyChanged
    {
        private string _quoteCode;
        private string _subject;
        private int _clientId;
        private ICollection<Client> _clients;

        public AddQuoteWindow()
        {
            InitializeComponent();
            GridContainer.DataContext = this;
            ClientRepository clientRepo = new ClientRepository();
            Clients = clientRepo.GetAll();

            IQuoteRepostory quoteRepo = new QuoteRepository();
            _quoteCode = quoteRepo.SuggestCode();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        public ICollection<Client> Clients
        {
            get { return _clients; }
            set
            {
                _clients = value;
                OnPropertyChanged();
            }

        }

        public int ClientId
        {
            get { return _clientId; }
            set
            {
                if (value == _clientId) return;
                _clientId = value;
                OnPropertyChanged();
            }
        }

        public string QuoteCode
        {
            get { return _quoteCode; }
            set
            {
                if (value == _quoteCode) return;
                _quoteCode = value;
                OnPropertyChanged();
            }
        }

        public string Subject
        {
            get { return _subject; }
            set
            {
                if (value == _subject) return;
                _subject = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #region  INotifyPropertyChanged

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private void ComboClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object value = ((ComboBox)sender).SelectedValue;

            try
            {
                ClientId = int.Parse(value.ToString());
            }
            catch(Exception)
            {
            }
        }
    }
}
