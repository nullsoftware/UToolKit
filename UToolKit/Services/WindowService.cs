﻿using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shell;

namespace NullSoftware.Services
{
    public class WindowService : IWindowService
    {
        #region Fields

        private Window _currentWindow;

        #endregion

        #region Properties

        /// <inheritdoc/>
        public bool IsActive => _currentWindow.IsActive;

        #endregion

        #region Constructors

        public WindowService(Window window)
        {
            if (window is null)
                throw new ArgumentNullException(nameof(window));

            _currentWindow = window;
        }

        #endregion

        #region Methods

        /// <inheritdoc/>
        public void Activate() => _currentWindow.Activate();

        /// <inheritdoc/>
        public void Close() => _currentWindow.Close();

        /// <inheritdoc/>
        public void Close(bool dialogResult)
        {
            _currentWindow.DialogResult = dialogResult;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return _currentWindow.ToString();
        }

        #endregion
    }
}
