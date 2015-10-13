using System;
using Devis.Models;

namespace Devis.Controls
{
    partial class QuoteDetail
    {
        #region Toolbar

        private void ActionToobar_OnAddPackage(object sender, EventArgs e)
        {
            try
            {
                var form = new AddQuoteItemWindow
                {
                    Title = "Ajouter un lot"
                };

                if (form.ShowDialog() == true)
                {
                    var item = new QuotePackage
                    {
                        Label = form.Label,
                    };

                    _packages.Add(item);
                    RefreshGrid();
                }
            }
            catch (Exception exception)
            {
                MessageBoxHelper.ShowError(exception);
            }
        }

        private void ActionToobar_OnAddArticle(object sender, EventArgs e)
        {
            try
            {
                if (_selectionContext.Model != null && _selectionContext.Model.Level == 2)
                {
                    var form = new AddQuoteItemWindow
                    {
                        Title = "Ajouter un article"
                    };

                    if (form.ShowDialog() == true)
                    {
                        var article = new QuoteArticle { Label = form.Label, };
                        var entry = (QuoteEntry)_selectionContext.Model.UnderlyingObject;
                        entry.AddArticle(article);

                        RefreshGrid();
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBoxHelper.ShowError(exception);
            }
        }


        private void ActionToobar_OnAddEntry(object sender, EventArgs e)
        {
            try
            {
                if (_selectionContext.Model != null && _selectionContext.Model.Level == 1)
                {
                    var form = new AddQuoteItemWindow
                    {
                        Title = "Ajouter un poste"
                    };

                    if (form.ShowDialog() == true)
                    {
                        var entry = new QuoteEntry { Label = form.Label, };
                        var package = (QuotePackage)_selectionContext.Model.UnderlyingObject;
                        package.AddEntry(entry);
                        

                        RefreshGrid();
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBoxHelper.ShowError(exception);
            }
        }

        #endregion
    }
}