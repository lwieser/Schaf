using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media;
using Devis.Annotations;

namespace Devis
{
    /// <summary>
    ///     Logique d'interaction pour FormatToolBar.xaml
    /// </summary>
    public partial class FormatToolBar : INotifyPropertyChanged
    {
        public FormatToolBar()
        {
            InitializeComponent();

            MainToolbar.DataContext = this;
        }

        private readonly double[] _fontSizes = { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
        private double _currentFontSize;
        private FontFamily _currentFontFamily;

        public IEnumerable<double> FontSizes
        {
            get { return _fontSizes; }
        }

        public IEnumerable<FontFamily> FontFamilies
        {
            get { return Fonts.SystemFontFamilies.OrderBy(f => f.Source); }
        }

        public double CurrentFontSize
        {
            get { return _currentFontSize; }
            set
            {
                if (value.Equals(_currentFontSize)) return;
                _currentFontSize = value;
                OnPropertyChanged();
            }
        }


        public FontFamily CurrentFontFamily
        {
            get { return _currentFontFamily; }
            set
            {
                if (Equals(value, _currentFontFamily)) return;
                _currentFontFamily = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}