using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows;

namespace NullSoftware.ToolKit.Extensions
{
    /// <summary>
    /// Defines a extension methods and attached properties
    /// for <see cref="Control"/>.
    /// </summary>
    public static class ControlExtensions
    {
        #region FocusMode

        /// <summary>
        /// Identifies the FocusMode attached property.
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        public static readonly DependencyProperty FocusModeProperty
           = DependencyProperty.RegisterAttached("FocusMode", typeof(FocusMode), typeof(ControlExtensions),
               new FrameworkPropertyMetadata(FocusMode.Default, OnFocusModeChanged));

        /// <summary>
        /// Sets the value of the FocusMode attached property
        /// to a given <see cref="Control"/>.
        /// </summary>
        /// <param name="element">
        /// The element on which to set the FocusMode attached property.
        /// </param>
        /// <param name="value">
        /// The property value to set.
        /// </param>
        public static void SetFocusMode(DependencyObject element, FocusMode value)
        {
            element.SetValue(FocusModeProperty, value);
        }

        /// <summary>
        /// Gets the value of the FocusMode attached property
        /// from a given <see cref="Control"/>.
        /// </summary>
        /// <param name="element">
        /// The element from which to read the property value.
        /// </param>
        /// <returns>
        /// The value of the FocusMode attached property.
        /// </returns>
        public static FocusMode GetFocusMode(DependencyObject element)
        {
            return (FocusMode)element.GetValue(FocusModeProperty);
        }

        private static void OnFocusModeChanged(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            if (sender is Control ctrl)
            {
                FocusMode mode = (FocusMode)e.NewValue;

                switch (mode)
                {
                    case FocusMode.FocusOnLoad:
                        ctrl.Dispatcher.BeginInvoke(
                            new Func<bool>(ctrl.Focus));
                        break;
                }
            }
            else if (sender != null)
            {
                throw new NotSupportedException();
            }
        }

        #endregion
    }
}
