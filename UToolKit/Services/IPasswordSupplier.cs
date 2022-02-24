using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace NullSoftware.Services
{
    /// <summary>
    /// Password supplier.
    /// </summary>
    public interface IPasswordSupplier
    {
        /// <summary>
        /// Occurs when the password of the PasswordBox changes.
        /// </summary>
        event EventHandler PasswordChanged;

        /// <summary>
        /// Gets or sets password from/to PasswordBox.
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// Gets secure password from PasswordBox.
        /// </summary>
        SecureString SecurePassword { get; }

        /// <summary>
        /// Clears password from PasswordBox.
        /// </summary>
        void Clear();
    }
}
