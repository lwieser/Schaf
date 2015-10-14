using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Devis.Tools;
using Devis.Models;
using Devis.ViewModels;
using Devis.Repositories;

namespace Devis.Controls
{
    /// <summary>
    /// Logique d'interaction pour QuoteHeaderPanel.xaml
    /// </summary>
    public partial class QuoteHeaderPanel 
    {
        public QuoteHeaderPanel()
        {
            InitializeComponent();

            Panels = new ObservableCollection<QuoteHeaderItem>
            {
                new QuoteHeaderItem(QuoteHeaderPanelType.Client),
                new QuoteHeaderItem(QuoteHeaderPanelType.Billing),
                new QuoteHeaderItem(QuoteHeaderPanelType.ConstructionSite),
            };
            DataContext = new QuoteHeaderViewModel();
            Container.DataContext = this;
            Container.SelectedIndex = 0;
        }

        public ObservableCollection<QuoteHeaderItem> Panels { get; private set; }

        private void RemoveQuoteHeader_Click(object sender, RoutedEventArgs e)
        {
            var item = UITools.FindAncestor<TabItem>(e.OriginalSource as DependencyObject);
            if (item != null)
            {
                var context = item.DataContext as QuoteHeaderItem;
                if (context != null)
                    Panels.Remove(context);
            }
        }
    }

    public class QuoteHeaderItem
    {
        public QuoteHeaderItem(QuoteHeaderPanelType panelType)
        {
            PanelType = panelType;
        }

        public QuoteHeaderPanelType PanelType { get; private set; }
    }

    public enum QuoteHeaderPanelType
    {
        Client,
        Billing,
        ConstructionSite,
    }

    public class QuoteHeaderDesignViewModel : NotifyPropertyChangedGeneric
    {
        public QuoteHeaderDesignViewModel()
        {
            Panels = new ObservableCollection<QuoteHeaderItem>
            {
                new QuoteHeaderItem(QuoteHeaderPanelType.Client),
                new QuoteHeaderItem(QuoteHeaderPanelType.Billing),
                new QuoteHeaderItem(QuoteHeaderPanelType.ConstructionSite)
            };
        }

        public string Test
        {
            get { return "Testxx"; }
        }
        public ObservableCollection<QuoteHeaderItem> Panels { get; set; }
        public Client Client { get; set; }
    }

    public class QuoteHeaderPanelTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ClientDataTemplate { get; set; }
        public DataTemplate BillingdataTemplate { get; set; }
        public DataTemplate ConstructionSiteDataTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            try
            {
                var tmp = item as QuoteHeaderItem;
                if (tmp == null)
                    return base.SelectTemplate(item, container);

                switch (tmp.PanelType)
                {
                    case QuoteHeaderPanelType.Client:
                        return ClientDataTemplate;
                    case QuoteHeaderPanelType.Billing:
                        return BillingdataTemplate;
                    case QuoteHeaderPanelType.ConstructionSite:
                        return ConstructionSiteDataTemplate;
                    default:
                        return ClientDataTemplate;
                }
            }
            catch
            {
                return base.SelectTemplate(item, container);
            }
        }
    }
}
