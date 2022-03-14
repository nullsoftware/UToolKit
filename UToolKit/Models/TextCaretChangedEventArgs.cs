using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NullSoftware.Models
{
    public class TextCaretChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets textbox line index.
        /// </summary>
        public int LineIndex { get; }

        /// <summary>
        /// Gets textbox character index.
        /// </summary>
        public int CharacterIndex { get; }

        public TextCaretChangedEventArgs(int ln, int col)
        {
            this.LineIndex = ln;
            this.CharacterIndex = col;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{LineIndex}, {CharacterIndex}";
        }
    }
}
