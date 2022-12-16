using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace NullSoftware.ToolKit.Converters
{
    /// <summary>
    /// Represents the converter that converts <see cref="Boolean"/> values 
    /// to inverse <see cref="Boolean"/> values.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(bool))]
    public class InverseBooleanConverter : IValueConverter
    {
        /// <summary>
        /// Converts a <see cref="Boolean"/> value to a inverse <see cref="Boolean"/> value.
        /// </summary>
        /// <param name="value">
        /// The Boolean value to convert. 
        /// </param>
        /// <param name="targetType">This parameter is not used.</param>
        /// <param name="parameter">This parameter is not used.</param>
        /// <param name="culture">This parameter is not used.</param>
        /// <returns>
        /// Inverse <see cref="Boolean"/> value.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !((bool)value);
        }

        /// <summary>
        /// Converts a <see cref="Boolean"/> value to a inverse <see cref="Boolean"/> value.
        /// </summary>
        /// <param name="value">
        /// The Boolean value to convert. 
        /// </param>
        /// <param name="targetType">This parameter is not used.</param>
        /// <param name="parameter">This parameter is not used.</param>
        /// <param name="culture">This parameter is not used.</param>
        /// <returns>
        /// Inverse <see cref="Boolean"/> value.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !((bool)value);
        }
    }
}
