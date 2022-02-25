using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NullSoftware.Services
{
    public class PasswordSupplier : IPasswordSupplier, INotifyPropertyChanged
    {
        #region Fields

        private PasswordBox _passwordBox;

        #endregion

        #region Events

        /// <inheritdoc/>
        public event EventHandler PasswordChanged;

        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

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
            if (passwordBox is null)
                throw new ArgumentNullException(nameof(passwordBox));

            _passwordBox = passwordBox;
            _passwordBox.AddHandler(PasswordBox.PasswordChangedEvent, new RoutedEventHandler(OnPasswordChanged));
        }

        #endregion

        #region Methods

        /// <inheritdoc/>
        public void Clear() => _passwordBox.Clear();

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordChanged?.Invoke(this, EventArgs.Empty);
            OnPropertyChanged(nameof(Password));
        }

        #endregion
    }
}
