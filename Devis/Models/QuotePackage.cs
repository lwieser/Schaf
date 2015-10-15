using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace Devis.Models
{
    public class QuotePackage : QuoteItem
    {
        public QuotePackage()
        {
            Entries = new List<QuoteEntry>();
        }

        public QuotePackage(string packageLabel, int numerotation)
        {
            Label = packageLabel;
            Numerotation = numerotation;
            Entries = new List<QuoteEntry>();
        }

        /// <summary>
        ///     The list of entries
        /// </summary>
        public List<QuoteEntry> Entries { get; private set; }

        /// <summary>
        /// Add an entry
        /// </summary>
        /// <param name="entry"></param>
        public void AddEntry(QuoteEntry entry)
        {
            Entries.Add(entry);
            entry.Package = this;
        }

        public override double? Price
        {
            get
            {
                double? result = 0;

                foreach (var entries in Entries)
                {
                    var price = entries.Price ?? 0;
                    var quantity = entries.Quantity ?? 0;

                    result += price * quantity;
                }

                return result;
            }

            set
            {
                //throw new NotImplementedException();
            }
        }

        public int Numerotation { get; set; }

        public override double? Disbursed
        {
            get
            {
                double result = 0;

                foreach (var entry in Entries)
                {
                    var disbursed = entry.Disbursed ?? 0;
                    var quantity = entry.Quantity ?? 0;

                    result += disbursed * quantity;
                }

                return result;
            }
        }

        internal int GetNextNumerotation()
        {
            int numerotation = 1;

            if(Entries.Count > 0)
            {
                numerotation = Entries.Max(x => x.Numerotation) + 1;
            }

            return numerotation;
        }
    }
}