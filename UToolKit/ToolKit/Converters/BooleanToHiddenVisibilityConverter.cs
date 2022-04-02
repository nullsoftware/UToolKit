using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace NullSoftware.ToolKit.Converters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanToHiddenVisibilityConverter : IValueConverter, IMultiValueConverter
    {
        #region IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (Visibility)value == Visibility.Visible;
        }

        #endregion

        #region IMultiValueConverter

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.All(t => (bool)t) ? Visibility.Visible : Visibility.Hidden;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            bool isVisible = (Visibility)value == Visibility.Visible;
            object[] result = new object[targetTypes.Length];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = isVisible;
            }

            return result;
        }

        #endregion
    }
}
