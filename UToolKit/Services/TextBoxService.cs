using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using NullSoftware.Models;

namespace NullSoftware.Services
{
    public class TextBoxService : ITextBoxService
    {
        #region Fields

        private TextBox _textBox;

        #endregion

        #region Events

        public event EventHandler TextChanged;

        public event EventHandler<TextCaretChangedEventArgs> SelectionChanged;

        public event EventHandler Updated;

        #endregion

        #region Properties

        public string Text
        {
            get => _textBox.Text;
            set => _textBox.SetCurrentValue(TextBox.TextProperty, value);
        }

        public int CaretIndex => _textBox.CaretIndex;

        public int SelectionLength => _textBox.SelectionLength;

        public string SelectedText
        {
            get => _textBox.SelectedText;
            set => _textBox.SelectedText = value;
        }

        #endregion

        #region Constructors

        public TextBoxService(TextBox textBox)
        {
            if (textBox is null)
                throw new ArgumentNullException(nameof(textBox));

            _textBox = textBox;
            _textBox.AddHandler(TextBox.TextChangedEvent, new TextChangedEventHandler(OnTextBoxTextChanged));
            _textBox.AddHandler(TextBox.SelectionChangedEvent, new RoutedEventHandler(OnTextBoxSelectionChanged));
            //_textBox.AddHandler(TextBox.UnloadedEvent, new RoutedEventHandler(TextBox_Unloaded));
        }

        #endregion

        #region Methods

        public void Select(int start, int length)
        {
            _textBox.Select(start, length);

            // scroll to center of selection
            var scStart = _textBox.GetRectFromCharacterIndex(_textBox.SelectionStart);
            var scEnd = _textBox.GetRectFromCharacterIndex(_textBox.SelectionStart + _textBox.SelectionLength);
            _textBox.ScrollToVerticalOffset((scStart.Top + scEnd.Bottom - _textBox.ViewportHeight) / 2 + _textBox.VerticalOffset);
            _textBox.ScrollToHorizontalOffset((scStart.Left + scEnd.Right- _textBox.ViewportWidth) / 2 + _textBox.HorizontalOffset);
        }

        public void SelectAll()
        {
            _textBox.SelectAll();
        }

        public override string ToString()
        {
            return _textBox.ToString();
        }

        protected virtual void OnTextChanged(EventArgs e)
        {
            TextChanged?.Invoke(this, e);
        }

        protected virtual void OnSelectionChanged(TextCaretChangedEventArgs e)
        {
            SelectionChanged?.Invoke(this, e);
        }

        protected virtual void OnUpdated(EventArgs e)
        {
            Updated?.Invoke(this, e);
        }

        private void OnTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            OnTextChanged(EventArgs.Empty);
            OnUpdated(EventArgs.Empty);
        }

        private void OnTextBoxSelectionChanged(object sender, RoutedEventArgs e)
        {
            int ln = _textBox.GetLineIndexFromCharacterIndex(_textBox.CaretIndex);
            int col = _textBox.CaretIndex - _textBox.GetCharacterIndexFromLineIndex(ln);

            OnSelectionChanged(new TextCaretChangedEventArgs(ln, col));
            OnUpdated(EventArgs.Empty);
        }

        private void OnTextBoxUnloaded(object sender, RoutedEventArgs e)
        {
            // unsubscribe from all not-own events
            _textBox.RemoveHandler(TextBox.TextChangedEvent, new TextChangedEventHandler(OnTextBoxTextChanged));
            _textBox.RemoveHandler(TextBox.SelectionChangedEvent, new RoutedEventHandler(OnTextBoxSelectionChanged));
            _textBox.RemoveHandler(TextBox.UnloadedEvent, new RoutedEventHandler(OnTextBoxUnloaded));

            // clear all subscriptions from own events
            TextChanged = null;
            SelectionChanged = null;
            Updated = null;
        }

        #endregion
    }
}
