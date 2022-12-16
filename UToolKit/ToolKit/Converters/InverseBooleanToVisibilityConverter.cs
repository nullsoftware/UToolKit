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
    /// to and from <see cref="Visibility"/> enumeration values.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class InverseBooleanToVisibilityConverter : IValueConverter, IMultiValueConverter
    {
        #region IValueConverter

        /// <summary>
        /// Converts a <see cref="Boolean"/> value to a <see cref="Visibility"/> enumeration value.
        /// </summary>
        /// <param name="value">
        /// The Boolean value to convert. 
        /// This value can be a standard Boolean value 
        /// or a nullable Boolean value.
        /// </param>
        /// <param name="targetType">This parameter is not used.</param>
        /// <param name="parameter">This parameter is not used.</param>
        /// <param name="culture">This parameter is not used.</param>
        /// <returns>
        /// <see cref="Visibility.Collapsed"/> if value is true;
        /// otherwise, <see cref="Visibility.Visible"/>.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool?)value == true ? Visibility.Collapsed : Visibility.Visible;
        }

        /// <summary>
        /// Converts a <see cref="Visibility"/> enumeration value to a <see cref="Boolean"/> value.
        /// </summary>
        /// <param name="value">A <see cref="Visibility"/> enumeration value.</param>
        /// <param name="targetType">This parameter is not used.</param>
        /// <param name="parameter">This parameter is not used.</param>
        /// <param name="culture">This parameter is not used.</param>
        /// <returns>
        /// true if value is not <see cref="Visibility.Visible"/>; otherwise, false.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (Visibility)value != Visibility.Visible;
        }

        #endregion

        #region IMultiValueConverter

        /// <summary>
        /// Converts the array of <see cref="Boolean"/> values to a <see cref="Visibility"/> enumeration value.
        /// </summary>
        /// <param name="values">
        /// The Boolean values to convert. 
        /// This array item can be a standard Boolean value 
        /// or a nullable Boolean value.
        /// </param>
        /// <param name="targetType">This parameter is not used.</param>
        /// <param name="parameter">This parameter is not used.</param>
        /// <param name="culture">This parameter is not used.</param>
        /// <returns>
        /// <see cref="Visibility.Collapsed"/> if all values is true;
        /// otherwise, <see cref="Visibility.Visible"/>.
        /// </returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.All(t => (bool?)t == true) ? Visibility.Collapsed : Visibility.Visible;
        }

        /// <summary>
        /// Converts a <see cref="Visibility"/> enumeration value to a array of <see cref="Boolean"/> values.
        /// </summary>
        /// <param name="value">A <see cref="Visibility"/> enumeration value.</param>
        /// <param name="targetTypes">This parameter is not used.</param>
        /// <param name="parameter">This parameter is not used.</param>
        /// <param name="culture">This parameter is not used.</param>
        /// <returns>
        /// bool array with positive values if value is not <see cref="Visibility.Visible"/>;
        /// otherwise, array with negative values.
        /// </returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            bool isVisible = (Visibility)value == Visibility.Visible;
            object[] result = new object[targetTypes.Length];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = !isVisible;
            }

            return result;
        }

        #endregion
    }
}
