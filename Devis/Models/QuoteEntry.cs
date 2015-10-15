using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

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

        public QuoteEntry(int numerotation, string label)
        {
            Label = label;
            Numerotation = numerotation;
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

        public int Numerotation { get; set; }

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
        
        public override double? Disbursed
        {
            get
            {
                double result = 0;

                foreach (var quoteArticle in Articles)
                {
                    var disbursed = quoteArticle.Disbursed ?? 0;
                    var quantity = quoteArticle.Quantity ?? 0;

                    result += disbursed * quantity;
                }

                return result;
            }
        }

        internal int GetNextNumerotation()
        {
            int next = 1;
            if(Articles.Count > 0)
            {
                next = Articles.Max(x => x.Numerotation) + 1;
            }

            return next;
        }
    }


    public class QuoteArticle : QuoteItem
    {
        public QuoteArticle(int numerotation, string label)
        {
            Numerotation = numerotation;
            Label = label;
        }

        private QuoteArticle() { }

        public QuoteEntry Entry { get; set; }
    }

}