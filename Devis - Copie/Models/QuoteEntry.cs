using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Devis.Models
{
    /// <summary>
    /// QuoteEntry class
    /// </summary>
    public class QuoteEntry : QuoteItem
    {
        public QuoteEntry()
        {
            Articles = new Collection<QuoteArticle>();
        }

        public QuotePackage Package { get; set; }

        /// <summary>
        /// Gets article
        /// </summary>
        public ICollection<QuoteArticle> Articles { get; private set; }

        /// <summary>
        ///  Add an article
        /// </summary>
        /// <param name="article"></param>
        public void AddArticle(QuoteArticle article)
        {
            Articles.Add(article);
            article.Entry = this;
        }

        public override double? Price
        {
            get
            {
                double? result = 0;

                foreach (var quoteArticle in Articles)
                {
                    var price = quoteArticle.Price ?? 0;
                    var quantity = quoteArticle.Quantity ?? 0;

                    result += price * quantity;
                }

                return result;
            }

            set
            {
                //throw new NotImplementedException();
            }
        }

    }


    public class QuoteArticle : QuoteItem
    {
        public QuoteEntry Entry { get; set; }
    }

}