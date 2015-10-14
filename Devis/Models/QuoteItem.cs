using System;
using System.ComponentModel.DataAnnotations;

namespace Devis.Models
{
    public abstract class QuoteItem 
    {
        [Key]
        public virtual int Id { get; set; }
        public virtual string Reference { get; set; }
        public virtual string Label { get; set; }
        public virtual double? Progress { get; set; }
        public virtual double? Price { get; set; }
        public virtual int? Quantity { get; set; }
        public virtual int Numerotation { get; set; }
        public string Unit { get; set; }
        public virtual double? Amount { get; set; }
        
    }
}