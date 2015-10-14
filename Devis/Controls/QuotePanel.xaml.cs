using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Devis.Tools;
using System;
using Devis.Repositories;
using Devis.Models;

namespace Devis.Controls
{
    /// <summary>
    ///     Logique d'interaction pour QuotePanel.xaml
    /// </summary>
    public partial class QuotePanel
    {
        public int Id { get; set; }

        public QuotePanel(int id)
        {
            App.Current.Properties["QuoteID"] = id.ToString();
            InitializeComponent();

            Panels = new ObservableCollection<QuoteDetailItem>
            {
                new QuoteDetailItem(QuotePanelType.Head),
                new QuoteDetailItem(QuotePanelType.Foot)
            };
            Container.DataContext = new QuotePanelDesignViewModel();
            Container.SelectedIndex = 0;
        }

        public ObservableCollection<QuoteDetailItem> Panels { get; private set; }

        public void AddQuoteDetail()
        {
            Panels.Insert(Panels.Count - 1, new QuoteDetailItem(QuotePanelType.QuoteDetail));
            Container.SelectedIndex = Panels.Count - 2;
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            AddNewQuoteDetail();
        }

        private void AddNewQuoteDetail()
        {
            try
            {
                AddQuoteDetail();
            }
            catch (Exception exception)
            {
                MessageBoxHelper.ShowError(exception);
            }
        }

        private void RemoveQuoteDetail_Click(object sender, RoutedEventArgs e)
        {
            var item = UITools.FindAncestor<TabItem>(e.OriginalSource as DependencyObject);
            if (item != null)
            {
                var context = item.DataContext as QuoteDetailItem;
                if (context != null)
                    Panels.Remove(context);
            }
        }

        private void AddQuoteDetail_Click(object sender, RoutedEventArgs e)
        {
            AddNewQuoteDetail();
        }
    }

    public class QuoteDetailItem
    {
        public QuoteDetailItem(QuotePanelType panelType)
        {
            PanelType = panelType;
        }

        public QuotePanelType PanelType { get; private set; }
    }

    public enum QuotePanelType
    {
        Head,
        Foot,
        QuoteDetail,
    }

    public class QuotePanelDesignViewModel
    {
        public int Id { get; set; }

        public QuotePanelDesignViewModel()
        {
            Panels = new ObservableCollection<QuoteDetailItem>
            {
                new QuoteDetailItem(QuotePanelType.Head),
                new QuoteDetailItem(QuotePanelType.QuoteDetail),
                new QuoteDetailItem(QuotePanelType.Foot)
            };
        }

        public ObservableCollection<QuoteDetailItem> Panels { get; set; }
    }

    public class QuotePanelTemplateSelector : DataTemplateSelector
    {
        public DataTemplate HeadDataTemplate { get; set; }
        public DataTemplate FooterDataTemplate { get; set; }
        public DataTemplate QuoteDetailDataTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            try
            {
                var tmp = item as QuoteDetailItem;
                if (tmp == null)
                    return base.SelectTemplate(item, container);

                switch (tmp.PanelType)
                {
                    case QuotePanelType.Head:
                        return HeadDataTemplate;
                    case QuotePanelType.Foot:
                        return FooterDataTemplate;
                    case QuotePanelType.QuoteDetail:
                        return QuoteDetailDataTemplate;
                    default:
                        return HeadDataTemplate;
                }
            }
            catch
            {
                return base.SelectTemplate(item, container);
            }
        }
    }
    
}