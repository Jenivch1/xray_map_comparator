using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MapComparer.Interface
{
    class HashToStringConverter : IValueConverter
    {
        public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
        {
            String str  = string.Empty;
            
            if (value is BitArray)
            {
                BitArray array = value as BitArray;
                foreach (var item in array)
                {
                    str = item is true ? "1" : "0";
                }
            }
            else
            {
                MessageBox.Show("Converter parameter is not BitArray", "Error");
            }
            
            return str;
        }

        public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}