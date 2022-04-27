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

        /// <inheritdoc/>
        public event EventHandler TextChanged;

        /// <inheritdoc/>
        public event EventHandler<TextCaretChangedEventArgs> SelectionChanged;

        #endregion

        #region Properties

        /// <inheritdoc/>
        public string Text
        {
            get => _textBox.Text;
            set => _textBox.SetCurrentValue(TextBox.TextProperty, value);
        }

        /// <inheritdoc/>
        public int CaretIndex => _textBox.CaretIndex;

        /// <inheritdoc/>
        public int SelectionLength => _textBox.SelectionLength;

        /// <inheritdoc/>
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
        }

        #endregion

        #region Methods

        /// <inheritdoc/>
        public void Select(int start, int length)
        {
            _textBox.Select(start, length);

            // scroll to center of selection
            var scStart = _textBox.GetRectFromCharacterIndex(_textBox.SelectionStart);
            var scEnd = _textBox.GetRectFromCharacterIndex(_textBox.SelectionStart + _textBox.SelectionLength);
            _textBox.ScrollToVerticalOffset((scStart.Top + scEnd.Bottom - _textBox.ViewportHeight) / 2 + _textBox.VerticalOffset);
            _textBox.ScrollToHorizontalOffset((scStart.Left + scEnd.Right- _textBox.ViewportWidth) / 2 + _textBox.HorizontalOffset);
        }

        /// <inheritdoc/>
        public void SelectAll()
        {
            _textBox.SelectAll();
        }

        /// <inheritdoc/>
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

        private void OnTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            OnTextChanged(EventArgs.Empty);
        }

        private void OnTextBoxSelectionChanged(object sender, RoutedEventArgs e)
        {
            int ln = _textBox.GetLineIndexFromCharacterIndex(_textBox.CaretIndex);
            int col = _textBox.CaretIndex - _textBox.GetCharacterIndexFromLineIndex(ln);

            OnSelectionChanged(new TextCaretChangedEventArgs(ln, col));
        }

        #endregion
    }
}
