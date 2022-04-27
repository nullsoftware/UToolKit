using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using NullSoftware.ToolKit;

namespace NullSoftware.ToolKit
{
    /// <summary>
    /// Provides methods to handle saving or loading window placement in binary format.
    /// </summary>
    public interface IWindowPlacementStorage
    {
        /// <summary>
        /// Saves placement of specified window.
        /// </summary>
        /// <param name="window">The placement owner.</param>
        /// <param name="serializedPlacement">Window placement in binary format.</param>
        void SavePlacement(Window window, byte[] serializedPlacement);

        /// <summary>
        /// Loads placement of specified window.
        /// </summary>
        /// <param name="window">The placement owner.</param>
        /// <returns>The window placement in binary format.</returns>
        byte[] LoadPlacement(Window window);
    }
}
