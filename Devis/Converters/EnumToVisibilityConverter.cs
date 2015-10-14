using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Devis.Converters
{
    public class EnumToVisibilityConverter : IValueConverter
    {
        private EnumToBooleanConverter _baseConverter;

        public EnumToVisibilityConverter()
        {
            _baseConverter = new EnumToBooleanConverter();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var res = _baseConverter.Convert(value, targetType, parameter, culture);
            return bool.Parse(res.ToString()) ? "Visible" : "Collapsed";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var res = _baseConverter.ConvertBack(value, targetType, parameter, culture);
            return bool.Parse(res.ToString()) ? "Visible" : "Collapsed";
        }
    }
}
