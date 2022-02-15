using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace NullSoftware.Services
{
    public interface IPasswordSupplier
    {
        event EventHandler PasswordChanged;

        string Password { get; set; }

        SecureString SecurePassword { get; }

        void Clear();
    }
}
