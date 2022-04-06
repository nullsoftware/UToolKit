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
        /// Command, which executes, when window is closing.
        /// </summary>
        /// <remarks>
        /// If the command cannot be executed, window closing will be canceled.
        /// </remarks>
        ICommand ClosingCommand { get; set; }

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
    }
}
