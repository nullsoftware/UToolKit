using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NullSoftware.Services
{
    public interface IWindowService
    {
        /// <summary>
        /// Gets value that indicates whether the window is active.
        /// </summary>
        bool IsActive { get; }

        /// <summary>
        /// Attempts to brind the window to the foreground and activates it.
        /// </summary>
        void Activate();

        /// <summary>
        /// Closes window.
        /// </summary>
        void Close();

        /// <summary>
        /// Closes window with specified result.
        /// </summary>
        /// <param name="dialogResult">Dialog result.</param>
        void Close(bool dialogResult);

        /// <summary>
        /// Hides window from user.
        /// </summary>
        void Hide();

        /// <summary>
        /// Shows hidden window.
        /// </summary>
        void Show();
    }
}
