using Devis.Controls.Design;
using Devis.Models;
using Devis.Repositories;
using Devis.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Devis.Controls
{
    /// <summary>
    /// Logique d'interaction pour QuoteList.xaml
    /// </summary>
    public partial class QuoteList : Page
    {
        private QuoteListViewModel _vm;

        public QuoteList()
        {
            InitializeComponent();
            _vm = new QuoteListViewModel();
            DataContext = _vm;
        }

        #region Events
        private void CreateQuote(object sender, RoutedEventArgs e)
        {
            try
            {
                var form = new AddQuoteWindow
                {
                    Title = "Ajouter un devis"
                };

                if (form.ShowDialog() == true)
                {
                    if(form.ClientId == 0)
                    {
                        MessageBoxHelper.ShowError(new Exception("Veuillez choisir un client."));
                    }
                    else
                    {
                        IQuoteRepostory repository = new QuoteRepository();
                        int quoteID = repository.CreateQuote(form.QuoteCode, form.Subject, form.ClientId);
                        NavigationService.Navigate(new QuotePanel(quoteID));
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBoxHelper.ShowError(exception);
            }
        }

        private void EditQuote(object sender, RoutedEventArgs e)
        {
            int id = ((Quote)QuoteListItem.SelectedItem).Id;
            NavigationService.Navigate(new QuotePanel(id));
        }

        private void DeleteQuote(object sender, RoutedEventArgs e)
        {
            IQuoteRepostory repository = new QuoteRepository();
            int id = ((Quote)QuoteListItem.SelectedItem).Id;
            repository.DeleteQuote(id);
            _vm.Reload();
        }
        #endregion

        private void QuoteListItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _vm.IsItemSelected = true;
        }
    }
}
