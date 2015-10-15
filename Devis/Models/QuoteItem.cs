using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Devis.Models
{
    public abstract class QuoteItem 
    {
        [Key]
        public virtual int Id { get; set; }
        public virtual int Numerotation { get; set; }
        public virtual string Reference { get; set; }
        public virtual string Label { get; set; }
        public virtual double? Quantity { get; set; }
        public string Unit { get; set; }
        public virtual double? Disbursed { get; set; }
        public virtual double? Price { get; set; }
        public virtual double? Progress { get; set; }
        public virtual double? Amount { get; set; }

        [NotMapped]
        public virtual double? Margin
        {
            get
            {
                if (Disbursed != null && Price != null)
                {
                    if (Price == 0)
                        return 0;

                    return (1 - ((double)Disbursed / (double)Price)) * 100;
                }
                return null;
            }

            set { }
        }
    }
}