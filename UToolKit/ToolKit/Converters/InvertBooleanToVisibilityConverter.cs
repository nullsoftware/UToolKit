﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace NullSoftware.ToolKit.Converters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class InvertBooleanToVisibilityConverter : IValueConverter, IMultiValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Visibility.Collapsed : Visibility.Visible;
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.All(t => (bool)t) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (Visibility)value != Visibility.Visible;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
