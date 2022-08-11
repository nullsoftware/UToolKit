using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace NullSoftware.ToolKit
{
    internal static class DesignModeVerifier
    {
        private static DependencyObject _dumbObj;

        internal static bool GetIsInDesignMode()
        {
            if (_dumbObj is null)
                _dumbObj = new DependencyObject();

            return DesignerProperties.GetIsInDesignMode(_dumbObj);
        }
    }
}
