using System;
using Devis.Models;

namespace Devis.ViewModels
{
    public static class QuoteItemViewModelFactory
    {
        public static QuoteItemViewModel Create(QuoteItem item, QuoteItemViewModel parent)
        {
            var package = item as QuotePackage;
            if (package != null)
                return new QuotePackageViewModel(package) {Parent = parent};

            var entry = item as QuoteEntry;
            if (entry != null)
                return new QuoteEntryViewModel(entry) { Parent = parent};

            var article = item as QuoteArticle;
            if (article != null)
                return new QuoteArticleViewModel(article) { Parent = parent};

            throw new NotSupportedException(string.Format("'{0}' not supported", item.GetType()));
        }
    }
}