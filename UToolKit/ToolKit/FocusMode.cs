using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace NullSoftware.ToolKit
{
    /// <summary>
    /// Represents focus modes for <see cref="Control"/>.
    /// </summary>
    public enum FocusMode : byte
    {
        /// <summary>
        /// Default built-in focus mode.
        /// </summary>
        Default,

        /// <summary>
        /// Focus element on <see cref="FrameworkElement.Loaded"/> event.
        /// </summary>
        FocusOnLoad,
    }
}
