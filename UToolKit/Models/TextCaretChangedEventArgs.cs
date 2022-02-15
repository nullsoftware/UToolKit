using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NullSoftware.Models
{
    public class TextCaretChangedEventArgs : EventArgs
    {
        public int LineIndex { get; }

        public int CharacterIndex { get; }

        public TextCaretChangedEventArgs(int ln, int col)
        {
            this.LineIndex = ln;
            this.CharacterIndex = col;
        }

        public override string ToString()
        {
            return $"{LineIndex}, {CharacterIndex}";
        }
    }
}
