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
using Infragistics.Controls.Schedules;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Devis.Planning;

namespace Devis.Controls
{
    /// <summary>
    /// Logique d'interaction pour Planning.xaml
    /// </summary>
    public partial class Planning : UserControl
    {
        private PlanningViewModel _vm;

        public Planning()
        {
            InitializeComponent();
            _vm = new PlanningViewModel();
            DataContext = _vm;
        }
    }
}
