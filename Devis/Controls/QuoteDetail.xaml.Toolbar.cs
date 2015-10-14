using System;
using Devis.Models;
using Devis.Repositories;

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
                    int quoteId = int.Parse(App.Current.Properties["QuoteID"].ToString());
                    QuoteRepository repo = new QuoteRepository();
                    Quote quote = repo.GetQuote(quoteId);
                    int numerotation = quote.GetNextNumerotation();

                    if(_selectionContext != null && _selectionContext.Model != null)
                    {
                        var package = (QuotePackage)_selectionContext.Model.UnderlyingObject;
                        numerotation = package.Numerotation + 1;
                        repo.UpgradeNumerotationFrom(quote, numerotation);
                    }

                    repo.AddPackage(quoteId, numerotation, form.Label);
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
                        var entry = (QuoteEntry)_selectionContext.Model.UnderlyingObject;
                        EntryRepository repo = new EntryRepository();

                        int numerotation = entry.GetNextNumerotation();

                        if (_selectionContext != null && _selectionContext.Model != null)
                        {
                            if (entry != null)
                            {
                                numerotation = entry.Numerotation + 1;
                                repo.UpgradeNumerotationFrom(entry, numerotation);
                            }
                        }

                        repo.AddEntry(entry, form.Label, 1);

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
                        var package = (QuotePackage)_selectionContext.Model.UnderlyingObject;
                        PackageRepository repo = new PackageRepository();

                        int numerotation = package.GetNextNumerotation();

                        if (_selectionContext != null && _selectionContext.Model != null)
                        {
                            var entry = _selectionContext.Model.UnderlyingObject as QuoteEntry;
                            if(entry != null)
                            {
                                numerotation = entry.Numerotation + 1;
                                repo.UpgradeNumerotationFrom(package, numerotation);
                            }
                        }

                        repo.AddEntry(package,form.Label,1);
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