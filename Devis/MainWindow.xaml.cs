using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Devis.Controls;
using System.Resources;
using System.Reflection;

namespace Devis
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            _mainFrame.Navigate(new Home());
        }

        private void ClickMenu(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
            HighlightSelected(sender);

            MenuItem item = (MenuItem)sender;

            switch(item.Name)
            {
                case "General":
                    _mainFrame.Navigate(new QuoteList());
                    break;
                case "Planning":
                    _mainFrame.Navigate(new Controls.Planning());
                    break;
                default:
                    _mainFrame.Navigate(new Home());
                    break;
            }
        }

        private void HighlightSelected(object sender)
        {
            MenuItem selectedItem = sender as MenuItem;
            string redCustom = Application.Current.Resources["ColorRedCustom"].ToString();
            string colorLightGrey = Application.Current.Resources["ColorGreyText"].ToString();
            selectedItem.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(redCustom));

            foreach(MenuItem item in Menu.Items.OfType<MenuItem>())
            {
                if (item != sender)
                {
                    item.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(colorLightGrey));
                }
            }

        }
    }
}
