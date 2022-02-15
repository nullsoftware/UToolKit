using System;
using NullSoftware.Models;

namespace NullSoftware.Services
{
    /// <summary>
    /// TextBox service.
    /// </summary>
    public interface ITextBoxService
    {
        /// <summary>
        /// Informs that the text has changed.
        /// </summary>
        event EventHandler TextChanged;

        /// <summary>
        /// Informs that the selection has changed.
        /// </summary>
        event EventHandler<TextCaretChangedEventArgs> SelectionChanged;

        /// <summary>
        /// Informs thet textbox was updated.
        /// </summary>
        /// <remarks>
        /// Triggers on text or selection changed.
        /// </remarks>
        event EventHandler Updated;

        /// <summary>
        /// Gets or sets text in text box.
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Gets current caret index.
        /// </summary>
        int CaretIndex { get; }

        /// <summary>
        /// Gets selected text length.
        /// </summary>
        int SelectionLength { get; }

        /// <summary>
        /// Gets or sets selected text in text box.
        /// </summary>
        string SelectedText { get; set; }

        /// <summary>
        /// Selects a range of text in text box.
        /// </summary>
        /// <param name="start">The zero-based character index of the first character in the selection.</param>
        /// <param name="length">The length of the selection, in characters.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Throws, when <paramref name="start"/> or <paramref name="length"/> is negative.
        /// </exception>
        void Select(int start, int length);

        /// <summary>
        /// Selects all text in text box.
        /// </summary>
        void SelectAll();
    }
}
