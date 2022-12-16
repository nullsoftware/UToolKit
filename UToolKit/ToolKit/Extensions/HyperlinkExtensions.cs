using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Documents;
using System.Diagnostics;

namespace NullSoftware.ToolKit.Extensions
{
    /// <summary>
    /// Defines a extension methods and attached properties
    /// for <see cref="Hyperlink"/>.
    /// </summary>
    public class HyperlinkExtensions
    {
        #region IsExternal

        /// <summary>
        /// Identifies the IsExternal attached property.
        /// </summary>
        /// <remarks>
        /// Allows to open <see cref="Hyperlink.NavigateUri"/> by external provider.
        /// </remarks>
        public static readonly DependencyProperty IsExternalProperty
            = DependencyProperty.RegisterAttached(
                "IsExternal",
                typeof(bool),
                typeof(HyperlinkExtensions),
                new UIPropertyMetadata(OnIsExternalChanged));

        /// <summary>
        /// Sets the value of the IsExternal attached property
        /// to a given <see cref="Hyperlink"/>.
        /// </summary>
        /// <param name="element">
        /// The element on which to set the IsExternal attached property.
        /// </param>
        /// <param name="value">
        /// The property value to set.
        /// </param>
        public static void SetIsExternal(DependencyObject element, bool value)
        {
            element.SetValue(IsExternalProperty, value);
        }

        /// <summary>
        /// Gets the value of the IsExternal attached property
        /// from a given <see cref="Hyperlink"/>.
        /// </summary>
        /// <param name="element">
        /// The element from which to read the property value.
        /// </param>
        /// <returns>
        /// The value of the IsExternal attached property.
        /// </returns>
        public static bool GetIsExternal(DependencyObject element)
        {
            return (bool)element.GetValue(IsExternalProperty);
        }

        private static void OnIsExternalChanged(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            if (DesignModeVerifier.GetIsInDesignMode())
                return;

            Hyperlink hyperlink = (Hyperlink)sender;

            if ((bool)e.NewValue)
                hyperlink.AddHandler(Hyperlink.RequestNavigateEvent, new RequestNavigateEventHandler(OnHyperlinkRequestNavigate));
            else
                hyperlink.RemoveHandler(Hyperlink.RequestNavigateEvent, new RequestNavigateEventHandler(OnHyperlinkRequestNavigate));
        }

        private static void OnHyperlinkRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }

        #endregion
    }
}
