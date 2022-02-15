using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NullSoftware.Services
{
    public class PasswordSupplier : IPasswordSupplier
    {
        #region Fields

        private PasswordBox _passwordBox;

        #endregion

        #region Events

        public event EventHandler PasswordChanged;

        #endregion

        #region Properties

        /// <inheritdoc/>
        public string Password 
        { 
            get => _passwordBox.Password; 
            set => _passwordBox.Password = value; 
        }

        public SecureString SecurePassword => _passwordBox.SecurePassword;

        #endregion

        #region Constructors

        public PasswordSupplier(PasswordBox passwordBox)
        {
            _passwordBox = passwordBox;
            _passwordBox.AddHandler(PasswordBox.PasswordChangedEvent, new RoutedEventHandler(OnPasswordChanged));
        }

        #endregion

        #region Methods

        public void Clear() => _passwordBox.Clear();

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordChanged?.Invoke(this, EventArgs.Empty);
        }


        #endregion
    }
}
