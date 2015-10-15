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
using Devis.Repositories;
using System.Windows.Data;
using System.Reflection;

namespace Devis.Controls
{
    /// <summary>
    ///     Logique d'interaction pour QuoteDetail.xaml
    /// </summary>
    public partial class QuoteDetail : INotifyPropertyChanged
    {
        private readonly List<QuotePackage> _packages = new List<QuotePackage>();
        private ObservableCollection<LineViewModel> _lines;
        private QuoteDetailSelectionContext _selectionContext;
        private Quote _quote;
        private int _id;
        private QuoteDetailViewModel _vm;

        public QuoteDetail()
        {
            InitializeComponent();
            Lines = new ObservableCollection<LineViewModel>();
            Container.DataContext = this;
            ActionToobar.CanAddPackage = true;
            FmtToolbar.PropertyChanged += FmtToolbar_PropertyChanged;
            _vm = new QuoteDetailViewModel();
            DataContext = _vm;
            _packages = _vm.Quote.Packages;

            //TODO : a déplacer dans le viewModel
            Lines = GenerateLines();
        }

        public ObservableCollection<LineViewModel> Lines
        {
            get { return _lines; }
            private set
            {
                if (Equals(value, _lines)) return;
                _lines = value;
                OnPropertyChanged();
            }
        }

        private void FirstLevelIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var item = UITools.FindAncestor<ListViewItem>(e.OriginalSource as DependencyObject);
            }
            catch (Exception exception)
            {
                MessageBoxHelper.ShowError(exception);
            }
        }

        private void SecondLevelIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var item = UITools.FindAncestor<ListViewItem>(e.OriginalSource as DependencyObject);
            }
            catch (Exception exception)
            {
                MessageBoxHelper.ShowError(exception);
            }
        }


        private void DataGridRow_MouseUp(object sender, MouseButtonEventArgs e)
        {
        }

        private void FmtToolbar_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                if (_selectionContext != null && _selectionContext.SelectedCells != null)
                {
                    Action<Control> actionToApply = null;
                    switch (e.PropertyName)
                    {
                        case "CurrentFontSize":
                            actionToApply = c => c.SetValue(FontSizeProperty, FmtToolbar.CurrentFontSize);
                            break;
                        case "CurrentFontFamily":
                            actionToApply = c => c.SetValue(FontFamilyProperty, FmtToolbar.CurrentFontFamily);
                            break;
                    }

                    if (actionToApply != null)
                    {
                        foreach (DataGridCell dataGridCell in _selectionContext.SelectedCells)
                            actionToApply(dataGridCell);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBoxHelper.ShowError(exception);
            }
        }

        private void TheGrid_OnSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            int count = e.AddedCells.Count;

            if (count > 0)
            {
                DataGridCell[] tmp = e.AddedCells.Select(DataGridHelper.GetCell).ToArray();
                _selectionContext = new QuoteDetailSelectionContext(tmp, TheGrid);

                DataGridCell firstCell = tmp[0];
                object fontFamilly = firstCell.GetValue(FontFamilyProperty);
                object fontSize = firstCell.GetValue(FontSizeProperty);
                object fontWeight = firstCell.GetValue(FontWeightProperty);
                object foreGround = firstCell.GetValue(ForegroundProperty);

                FmtToolbar.CurrentFontSize = (double) fontSize;
                FmtToolbar.CurrentFontFamily = (FontFamily) fontFamilly;


                /* Retreive the current row */
                UpdateActionButtonState(_selectionContext.Model);
            }
            else
                _selectionContext = new QuoteDetailSelectionContext(null, TheGrid);
        }

        /* Update button state */

        private void UpdateActionButtonState(LineViewModel lineModel)
        {
            ActionToobar.CanAddEntry = false;
            ActionToobar.CanAddArticle = false;

            if (lineModel != null)
            {
                if (lineModel.Level == 1)
                    ActionToobar.CanAddEntry = true;
                else if (lineModel.Level == 2)
                    ActionToobar.CanAddArticle = true;
            }
        }


        private ObservableCollection<LineViewModel> GenerateLines()
        {
            int quoteId = int.Parse(App.Current.Properties["QuoteID"].ToString());
            QuoteRepository repo = new QuoteRepository();
            Quote quote = repo.GetQuote(quoteId);
            List<QuotePackage> packages = quote.Packages;
            var models = new List<LineViewModel>();

            // Add Packages
            int index = 0;

            foreach (QuotePackage package in packages)
            {
                QuoteItemViewModel model = QuoteItemViewModelFactory.Create(package, null);
                model.Numbering = (index + 1).ToString();

                models.Add(model);
                models.Add(LineViewModel.Empty);

                // Add Entries (Postes)
                for (int entryIndex = 0; entryIndex < package.Entries.Count; entryIndex++)
                {
                    QuoteEntry entry = package.Entries[entryIndex];

                    QuoteItemViewModel entryModel = QuoteItemViewModelFactory.Create(entry, model);
                    entryModel.Numbering = string.Format("{0}.{1}", model.Numbering, (entryIndex + 1));
                    models.Add(entryModel);

                    models.AddRange(entry.Articles.Select(a => QuoteItemViewModelFactory.Create(a, entryModel)));
                }

                if (package.Entries.Any())
                    models.Add(LineViewModel.Empty);

                index++;
            }

            //Update grid
            Lines = new ObservableCollection<LineViewModel>(models);
            return Lines;
        }

        private void RefreshGrid()
        {
            Lines = GenerateLines();
            // Update buttons
            UpdateActionButtonState(null);
        }

        private void AddQuoteDetail_Click(object sender, RoutedEventArgs e)
        {
                     AddNewQuoteDetail();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
                AddNewQuoteDetail();
        }

        private void AddNewQuoteDetail()
        {
            try
            {
                //AddQuoteDetail();
            }
            catch (Exception exception)
            {
                MessageBoxHelper.ShowError(exception);
            }
        }

        private void FlushQuote(object sender, RoutedEventArgs e)
        {
            QuoteRepository repo = new QuoteRepository();
            repo.Flush(int.Parse(App.Current.Properties["QuoteID"].ToString()));
            RefreshGrid();
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

        private void CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            QuoteEntry entry = _selectionContext.Model.UnderlyingObject as QuoteEntry;
            QuoteArticle article = _selectionContext.Model.UnderlyingObject as QuoteArticle;

            var binding = (e.Column as DataGridBoundColumn).Binding as Binding;
            string propertyChanged = binding.Path.Path;
            var editedTextbox = e.EditingElement as TextBox;
            string value = editedTextbox.Text;

            if (entry != null)
            {
                EntryRepository repo = new EntryRepository();
                switch (propertyChanged)
                {
                    case "Quantity":
                        entry.Quantity = int.Parse(value);
                        break;
                }
                repo.Update(entry);
            }

            try
            {

                if (article != null)
                {
                    ArticleRepository repo = new ArticleRepository();
                    switch (propertyChanged)
                    {
                        case "Price":
                            article.Price = double.Parse(value);
                            break;
                        case "Amount":
                            article.Amount = double.Parse(value);
                            break;
                        case "Quantity":
                            article.Quantity = int.Parse(value);
                            break;
                        case "Unit":
                            article.Unit = value;
                            break;
                        case "Disbursed":
                            article.Disbursed = double.Parse(value);
                            break;
                    }
                    repo.Update(article);
                }
            }
            catch(Exception exception)
            {
                MessageBoxHelper.ShowError(exception);
            }
        }
    }
}