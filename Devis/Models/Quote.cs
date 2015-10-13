using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devis.ViewModels;
using System.ComponentModel.DataAnnotations;
using Devis.Repositories;

namespace Devis.Models
{
    public class Quote
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime? Date { get;set; }
        public DateTime? RevivalDate { get; set; }
        public long? CodeC { get; set; }
        public string OwnerCode { get; set; }
        public string Subject { get; set; }
        public StateEnum? State { get; internal set; }
        public List<QuotePackage> Packages { get; set; }
        public Client Client { get; set; }

        private Quote()
        {
            Packages = new List<QuotePackage>();
        }

        public Quote(string code, string subject)
        {
            Date = DateTime.Now;
            Subject = subject;
            Code = code;
            Packages = new List<QuotePackage>();
        }

        public enum StateEnum
        {
            Remis,
            Chiffrage,
            Accepte
        }

        public enum CivilityEnum
        {
            Ets,
            SARL,
            FONCIERE,
            MmeetMr,
            M,
            DIRECTION,
            BRASSERIE
        }

        internal ObservableCollection<LineViewModel> GetLines()
        {
            ObservableCollection<LineViewModel> lines = new ObservableCollection<LineViewModel>();
            int packageIndex = 1;

            foreach(QuotePackage package in Packages)
            {
                lines.Add(new LineViewModel() {
                    Numbering = packageIndex.ToString(),
                    Label = package.Label,
                });
            }

            lines.Add(new LineViewModel()
            {
                IsEmpty = true,
            });
            return lines;
        }
    }
}
