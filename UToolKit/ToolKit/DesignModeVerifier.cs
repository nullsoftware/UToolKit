using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace NullSoftware.ToolKit
{
    /// <summary>
    /// Provides information about design mode.
    /// </summary>
    public static class DesignModeVerifier
    {
        private static DependencyObject _dumbObj;

        /// <summary>
        /// Gets value that indicates whether the current code executes in design mode.
        /// </summary>
        /// <returns>A value that indicates whether the current code executes in design mode.</returns>
        public static bool GetIsInDesignMode()
        {
            if (_dumbObj is null)
                _dumbObj = new DependencyObject();

            return DesignerProperties.GetIsInDesignMode(_dumbObj);
        }
    }
}
