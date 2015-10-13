using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Devis.Models;
using Devis.Tools;
using Devis.ViewModels;

namespace Devis.Controls
{
    partial class QuoteDetail
    {
        #region Commands

        public void DeleteCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            bool canExecute = false;

            if (_selectionContext != null)
            {
                //  We can delete only if a line with data is selected
                if (_selectionContext.Model != null && !_selectionContext.Model.IsEmpty)
                    canExecute = true;
            }

            e.CanExecute = canExecute;

            Trace.WriteLine(string.Format("canExecute = {0}", canExecute));
        }

        public void DeleteCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // We will retreive the lineModel associated with the row
            var row = UITools.FindAncestor<DataGridRow>(e.OriginalSource as DependencyObject);
            if (row != null)
            {
                var model = row.DataContext as LineViewModel;
                if (model != null)
                {
                    try
                    {
                        // Now that we found the line, we remove the underlying object and refresh the grid
                        if (!RemovePackage(model.UnderlyingObject))
                        {
                            if(!RemoveEntry(model.UnderlyingObject))
                                RemoveArticle(model.UnderlyingObject);
                        }

                        RefreshGrid();
                    }
                    catch (Exception exception)
                    {
                        MessageBoxHelper.ShowError(exception);
                    }
                }
            }
        }

        private void RemoveArticle(QuoteItem item)
        {
            var article = item as QuoteArticle;
            if (article != null && article.Entry != null)
            {
                QuoteEntry entry = article.Entry;
                if (entry != null && entry.Articles.Remove(article))
                {
                    article.Entry = null;
                }
            }
        }

        private bool RemoveEntry(QuoteItem item)
        {
            bool result = false;

            var entry = item as QuoteEntry;
            if (entry != null && entry.Package != null)
            {
                QuotePackage package = entry.Package;
                if (package != null && package.Entries.Remove(entry))
                {
                    entry.Package = null;
                    result = true;
                }
            }

            return result;
        }

        private bool RemovePackage(QuoteItem item)
        {
            bool result = false;
            var package = item as QuotePackage;
            if (package != null)
                result = _packages.Remove(package);

            return result;
        }

        #endregion
    }
}