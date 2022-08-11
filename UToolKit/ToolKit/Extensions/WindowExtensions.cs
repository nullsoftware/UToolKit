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

namespace NullSoftware.ToolKit.Extensions
{
    /// <summary>
    /// Defines a extension methods and attached properties
    /// for <see cref="Window"/>.
    /// </summary>
    public static class WindowExtensions
    {
        #region CloseCommand

        /// <summary>
        /// Identifies the CloseCommand attached property.
        /// </summary>
        /// <remarks>
        /// Command, which executes, when window is closing.
        /// If the command cannot be executed, window closing will be canceled.
        /// </remarks>
        public static readonly DependencyProperty CloseCommandProperty
            = DependencyProperty.RegisterAttached(
                "CloseCommand",
                typeof(ICommand),
                typeof(WindowExtensions),
                new UIPropertyMetadata(OnCloseCommandChanged));

        /// <summary>
        /// Sets the value of the CloseCommand attached property
        /// to a given <see cref="Window"/>.
        /// </summary>
        /// <param name="element">
        /// The element on which to set the CloseCommand attached property.
        /// </param>
        /// <param name="value">
        /// The property value to set.
        /// </param>
        public static void SetCloseCommand(DependencyObject element, ICommand value)
        {
            element.SetValue(CloseCommandProperty, value);
        }

        /// <summary>
        /// Gets the value of the CloseCommand attached property
        /// from a given <see cref="Window"/>.
        /// </summary>
        /// <param name="element">
        /// The element from which to read the property value.
        /// </param>
        /// <returns>
        /// The value of the CloseCommand attached property.
        /// </returns>
        public static ICommand GetCloseCommand(DependencyObject element)
        {
            return (ICommand)element.GetValue(CloseCommandProperty);
        }

        private static void OnCloseCommandChanged(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            if (DesignModeVerifier.GetIsInDesignMode())
                return;

            Window win = (Window)sender;

            win.Closing -= OnWindowClosing;
            win.Closed -= OnWindowClosed;

            if (e.NewValue is ICommand)
            {
                win.Closing += OnWindowClosing;
                win.Closed += OnWindowClosed;
            }
        }

        private static void OnWindowClosing(object sender, CancelEventArgs e)
        {
            Window win = (Window)sender;
            ICommand cmd = GetCloseCommand(win);

            if (cmd.CanExecute(null))
                cmd.Execute(null);
            else
                e.Cancel = true;
        }

        private static void OnWindowClosed(object sender, EventArgs e)
        {
            Window win = (Window)sender;

            win.Closing -= OnWindowClosing;
            win.Closed -= OnWindowClosed;
        }

        #endregion CloseCommand

        #region PlacementStorageStrategy

        /// <summary>
        /// Identifies the PlacementStorageStrategy attached property.
        /// </summary>
        /// <remarks>
        /// Provides service to handle window placement loading/saving.
        /// </remarks>
        public static readonly DependencyProperty PlacementStorageStrategyProperty
            = DependencyProperty.RegisterAttached(
                "PlacementStorageStrategy",
                typeof(IWindowPlacementStorage),
                typeof(WindowExtensions),
                new UIPropertyMetadata(OnPlacementStorageStrategyChanged));

        /// <summary>
        /// Sets the value of the PlacementStorageStrategy
        /// attached property to a given <see cref="Window"/>.
        /// </summary>
        /// <param name="element">
        /// The element on which to set the CloseCommand attached property.
        /// </param>
        /// <param name="value">
        /// The property value to set.
        /// </param>
        public static void SetPlacementStorageStrategy(DependencyObject element, IWindowPlacementStorage value)
        {
            element.SetValue(PlacementStorageStrategyProperty, value);
        }

        /// <summary>
        /// Gets the value of the PlacementStorageStrategy
        /// attached property from a given <see cref="Window"/>.
        /// </summary>
        /// <param name="element">
        /// The element from which to read the property value.
        /// </param>
        /// <returns>
        /// The value of the PlacementStorageStrategy attached property.
        /// </returns>
        public static IWindowPlacementStorage GetPlacementStorageStrategy(DependencyObject element)
        {
            return (IWindowPlacementStorage)element.GetValue(PlacementStorageStrategyProperty);
        }

        private static void OnPlacementStorageStrategyChanged(DependencyObject sender,
           DependencyPropertyChangedEventArgs e)
        {
            if (DesignModeVerifier.GetIsInDesignMode())
                return;

            Window win = (Window)sender;

            win.SourceInitialized -= OnWindowPlacementSourceInitialized;
            win.Closing -= OnWindowPlacementClosing;

            if (e.NewValue != null)
            {
                win.SourceInitialized += OnWindowPlacementSourceInitialized;
                win.Closing += OnWindowPlacementClosing;
            }
        }

        private static void OnWindowPlacementSourceInitialized(object sender, EventArgs e)
        {
            if (DesignModeVerifier.GetIsInDesignMode())
                return;

            Window win = (Window)sender;
            IWindowPlacementStorage placementStorage = GetPlacementStorageStrategy(win);
            byte[] rawData = placementStorage.LoadPlacement(win);

            if (rawData != null)
                WindowPlacementManager.SetPlacement(win, WindowPlacementManager.Deserialize(rawData));
        }

        private static void OnWindowPlacementClosing(object sender, CancelEventArgs e)
        {
            if (DesignModeVerifier.GetIsInDesignMode())
                return;

            Window win = (Window)sender;
            IWindowPlacementStorage placementStorage = GetPlacementStorageStrategy(win);
            byte[] rawData = WindowPlacementManager.Serialize(WindowPlacementManager.GetPlacement(win));

            placementStorage.SavePlacement(win, rawData);
        }

        #endregion PlacementStorageStrategy
    }
}
