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
using NullSoftware.Models;

namespace NullSoftware.ToolKit.Extensions
{
    /// <summary>
    /// Defines a extension methods and attached properties
    /// for <see cref="Window"/>.
    /// </summary>
    public static class WindowExtensions
    {
        #region ClosingCommand

        /// <summary>
        /// Identifies the ClosingCommand attached property.
        /// </summary>
        /// <remarks>
        /// Command, which executes, when window is closing.
        /// If the command cannot be executed, window closing will be canceled.
        /// </remarks>
        public static readonly DependencyProperty ClosingCommandProperty
            = DependencyProperty.RegisterAttached(
                "ClosingCommand",
                typeof(ICommand),
                typeof(WindowExtensions),
                new UIPropertyMetadata(OnClosingCommandChanged));

        /// <summary>
        /// Sets the value of the ClosingCommand attached property
        /// to a given <see cref="Window"/>.
        /// </summary>
        /// <param name="element">
        /// The element on which to set the ClosingCommand attached property.
        /// </param>
        /// <param name="value">
        /// The property value to set.
        /// </param>
        public static void SetClosingCommand(DependencyObject element, ICommand value)
        {
            element.SetValue(ClosingCommandProperty, value);
        }

        /// <summary>
        /// Gets the value of the ClosingCommand attached property
        /// from a given <see cref="Window"/>.
        /// </summary>
        /// <param name="element">
        /// The element from which to read the property value.
        /// </param>
        /// <returns>
        /// The value of the ClosingCommand attached property.
        /// </returns>
        public static ICommand GetClosingCommand(DependencyObject element)
        {
            return (ICommand)element.GetValue(ClosingCommandProperty);
        }

        private static void OnClosingCommandChanged(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
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
            ICommand cmd = GetClosingCommand(win);

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

        #endregion ClosingCommand

        #region WindowPlacementStorageStrategy

        /// <summary>
        /// Identifies the WindowPlacementStorageStrategy attached property.
        /// </summary>
        /// <remarks>
        /// Provides service to handle window placement loading/saving.
        /// </remarks>
        public static readonly DependencyProperty WindowPlacementStorageStrategyProperty
            = DependencyProperty.RegisterAttached(
                "WindowPlacementStorageStrategy",
                typeof(IWindowPlacementStorageStrategy),
                typeof(WindowExtensions),
                new UIPropertyMetadata(OnWindowPlacementStorageStrategyChanged));

        /// <summary>
        /// Sets the value of the WindowPlacementStorageStrategy
        /// attached property to a given <see cref="Window"/>.
        /// </summary>
        /// <param name="element">
        /// The element on which to set the ClosingCommand attached property.
        /// </param>
        /// <param name="value">
        /// The property value to set.
        /// </param>
        public static void SetWindowPlacementStorageStrategy(DependencyObject element, IWindowPlacementStorageStrategy value)
        {
            element.SetValue(WindowPlacementStorageStrategyProperty, value);
        }

        /// <summary>
        /// Gets the value of the WindowPlacementStorageStrategy
        /// attached property from a given <see cref="Window"/>.
        /// </summary>
        /// <param name="element">
        /// The element from which to read the property value.
        /// </param>
        /// <returns>
        /// The value of the WindowPlacementStorageStrategy attached property.
        /// </returns>
        public static IWindowPlacementStorageStrategy GetWindowPlacementStorageStrategy(DependencyObject element)
        {
            return (IWindowPlacementStorageStrategy)element.GetValue(WindowPlacementStorageStrategyProperty);
        }

        private static void OnWindowPlacementStorageStrategyChanged(DependencyObject sender,
           DependencyPropertyChangedEventArgs e)
        {
            Window win = (Window)sender;

            win.SourceInitialized -= OnWindowPlacementSourceInitialized;
            win.Closing -= OnWindowPlacementClosing;

            win.SourceInitialized += OnWindowPlacementSourceInitialized;
            win.Closing += OnWindowPlacementClosing;
        }

        private static void OnWindowPlacementSourceInitialized(object sender, EventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return;

            Window win = (Window)sender;
            IWindowPlacementStorageStrategy placementStorageStrategy = GetWindowPlacementStorageStrategy(win);
            byte[] rawData = placementStorageStrategy.LoadPlacement(win);

            WindowPlacementManager.SetPlacement(win, WindowPlacementManager.Deserialize(rawData));
        }

        private static void OnWindowPlacementClosing(object sender, CancelEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return;

            Window win = (Window)sender;
            IWindowPlacementStorageStrategy placementStorageStrategy = GetWindowPlacementStorageStrategy(win);
            byte[] rawData = WindowPlacementManager.Serialize(WindowPlacementManager.GetPlacement(win));

            placementStorageStrategy.SavePlacement(win, rawData);
        }

        #endregion WindowPlacementStorageStrategy
    }
}
