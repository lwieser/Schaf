﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Devis.Models
{
    public class QuotePackage : QuoteItem
    {
        public QuotePackage()
        {
            Entries = new Collection<QuoteEntry>();
        }

        /// <summary>
        ///     The list of entries
        /// </summary>
        public IList<QuoteEntry> Entries { get; private set; }

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
                throw new NotImplementedException();
            }
        }
    }
}