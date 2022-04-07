﻿using System.Windows;
using System.Windows.Input;

namespace NullSoftware.ToolKit
{
    /// <summary>
    ///  Allows associated a routed command with a non-routed command.  Used by
    ///  <see cref="RoutedCommandHandlers"/>.
    /// </summary>
    public class RoutedCommandHandler : Freezable
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
          "Command",
          typeof(ICommand),
          typeof(RoutedCommandHandler),
          new PropertyMetadata(default(ICommand)));

        /// <summary> The command that should be executed when the RoutedCommand fires. </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary> The command that triggers <see cref="ICommand"/>. </summary>
        public ICommand RoutedCommand { get; set; }

        /// <inheritdoc />
        protected override Freezable CreateInstanceCore()
        {
            return new RoutedCommandHandler();
        }

        /// <summary>
        ///  Register this handler to respond to the registered RoutedCommand for the
        ///  given element.
        /// </summary>
        /// <param name="owner"> The element for which we should register the command
        ///  binding for the current routed command. </param>
        internal void Register(FrameworkElement owner)
        {
            var binding = new CommandBinding(RoutedCommand, HandleExecuted, HandleCanExecute);
            owner.CommandBindings.Add(binding);
        }

        /// <summary> Proxy to the current Command.CanExecute(object). </summary>
        private void HandleCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Command?.CanExecute(e.Parameter) == true;
            e.Handled = true;
        }

        /// <summary> Proxy to the current Command.Execute(object). </summary>
        private void HandleExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Command?.Execute(e.Parameter);
            e.Handled = true;
        }
    }
}
