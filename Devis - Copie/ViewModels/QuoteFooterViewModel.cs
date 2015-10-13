using Devis.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Devis.ViewModels
{
    public class QuoteFooterViewModel
    {
        public static double TVA_RATE = 0.196;
        public double Debourse { get; set; }
        public double TVA { get; set; }
        public double Discount { get; set; }
        public double DiscountRate { get; set; }
        public double SalePrice { get; set; }
        public double PosePrice { get; set; }
        public double Refreshing { get; set; }
        public double GrossPrice { get; set; }
        public double GrossPriceRefreshed { get; set; }
        public double TaxeFreeNetCom { get; set; }
        public double RefreshingCoef { get; set; }
        public ObservableCollection<LineViewModel> Lines { get; private set; }
        public double DownPayRate { get; internal set; }
        public double DownPayAmount { get; internal set; }
        public double TotalNet { get; internal set; }
        public double TaxeIncluded { get; set; }
        public double TaxeIncludedNet { get; set; }
        public double TaxeFreeFinal { get;
            set; }

        private List<QuotePackage> _packages;

        public QuoteFooterViewModel()
        {
            Load();

            AssignValues();
        }

        private void AssignValues()
        {
            SalePrice = _packages.Sum(package => package.Price) ?? 0;
            PosePrice = 0;
            GrossPrice = SalePrice + PosePrice;
            RefreshingCoef = 1;
            Refreshing = GrossPrice * RefreshingCoef - GrossPrice;
            GrossPriceRefreshed = GrossPrice;
            Discount = 0;
            TaxeFreeNetCom = GrossPriceRefreshed;
            TaxeFreeFinal = TaxeFreeNetCom - Discount;
            TVA = TaxeFreeFinal * TVA_RATE;
            TaxeIncluded = TaxeFreeFinal + TVA;
            TaxeIncludedNet = TaxeIncluded;
        }

        private void Load()
        {
            var lines = new List<LineViewModel>();
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Devis.Controls.Design.sampleData.txt";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                const int numberOfItems = 8;
                string line;
                NumberFormatInfo nfi = new NumberFormatInfo();
                nfi.NumberDecimalSeparator = ".";
                _packages = new List<QuotePackage>();
                QuotePackage currentPackage = new QuotePackage();
                QuoteEntry currentEntry = new QuoteEntry();

                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(';');
                    var model = new LineViewModel();
                    if (parts.Length >= numberOfItems)
                    {
                        model.Numbering = parts[0];
                        model.Reference = parts[1];
                        model.Label = parts[2];

                        double progress;
                        if (double.TryParse(parts[3], out progress))
                            model.Progress = progress;

                        double price;
                        if (double.TryParse(parts[4], NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo,
                            out price))
                            model.Price = price;

                        int qty;
                        if (int.TryParse(parts[5], out qty))
                            model.Quantity = qty;

                        model.Unit = parts[6];

                        double amount;
                        if (double.TryParse(parts[7], NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo,
                            out amount))
                            model.Amount = amount;
                    }
                    else
                        model.IsEmpty = true;

                    lines.Add(model);

                    if(!model.IsEmpty)
                    {
                        if (String.IsNullOrEmpty(parts[0])) // article 
                        {
                            currentEntry.AddArticle(new QuoteArticle()
                            {
                                Price = model.Price,
                                Quantity = model.Quantity
                            });
                        }
                        if (parts[0].Contains('.')) // entry 
                        {
                            //if bnot first 
                            if (model.Numbering.Substring(1, 2) != ".1")
                            {
                                currentPackage.AddEntry(currentEntry);
                            }
                            currentEntry = new QuoteEntry()
                            {
                                Quantity = model.Quantity
                            };
                        }
                        else // package 
                        {
                            //If  not first 
                            if (model.Numbering != "1")
                            {
                                currentPackage.AddEntry(currentEntry);
                                _packages.Add(currentPackage);
                            }

                            currentPackage = new QuotePackage();
                        }
                    }
                }
                _packages.Add(currentPackage);
            }
        }
    }
}
