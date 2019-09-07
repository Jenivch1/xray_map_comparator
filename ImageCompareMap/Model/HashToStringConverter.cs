using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MapComparer.Model
{
    class HashToStringConverter : IValueConverter
    {

        public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
        {
            BitArray    array   = (BitArray)value;
            String      str     = string.Empty;
            foreach (var item in array)
            {
                str = item is true ? "1" : "0";
            }
            return str;
        }

        public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
