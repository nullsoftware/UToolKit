﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Markup;
using Microsoft.Win32;

namespace NullSoftware.ToolKit
{
    /// <summary>
    /// Provides logic to handle window placement saving/loading using Windows registry.
    /// </summary>
    public class RegistryStorage : MarkupExtension, IWindowPlacementStorage
    {
        /// <summary>
        /// Gets or sets top-level node of registry key that related to <see cref="Key"/>.
        /// </summary>
        public RegistryHive Hive { get; set; }

        /// <summary>
        /// Gets or sets registry key that will be used to store window placement.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets name format that will be used
        /// to retrive or set window placement to registry.
        /// </summary>
        public string NameFormat { get; set; } = "{0}.Placement";

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistryStorage"/> class.
        /// </summary>
        public RegistryStorage()
        {
            Hive = ProvideDefualtHive();
            Key = ProvideDefaultKey();
        }

        /// <inheritdoc/>
        public byte[] LoadPlacement(Window window)
        {
            RegistryKey key = RegistryKey.OpenBaseKey(Hive, RegistryView.Default).OpenSubKey(Key);

            if (key == null)
                return null;

            return (byte[])key.GetValue(GetSettingKey(window));
        }

        /// <inheritdoc/>
        public void SavePlacement(Window window, byte[] serializedPlacement)
        {
            RegistryKey key = RegistryKey.OpenBaseKey(Hive, RegistryView.Default).OpenSubKey(Key, true);

            if (key == null)
                key = Registry.CurrentUser.CreateSubKey(Key);

            key.SetValue(GetSettingKey(window), serializedPlacement, RegistryValueKind.Binary);
        }

        /// <inheritdoc/>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrWhiteSpace(Key) || string.IsNullOrWhiteSpace(NameFormat))
                throw new InvalidOperationException("Inputs cannot be blank.");

            return this;
        }

        /// <summary>
        /// Gets registry setting key for specified window.
        /// </summary>
        /// <param name="window">Window to get setting key.</param>
        /// <returns>The registry setting key for specified window.</returns>
        protected virtual string GetSettingKey(Window window)
        {
            return string.Format(NameFormat, window.GetType().Name);
        }

        /// <summary>
        /// Provides default value for <see cref="Hive"/> property.
        /// </summary>
        /// <returns>The default value for <see cref="Hive"/> property.</returns>
        protected virtual RegistryHive ProvideDefualtHive()
        {
            return RegistryHive.CurrentUser;
        }

        /// <summary>
        /// Provides default value for <see cref="Key"/> property.
        /// </summary>
        /// <returns>The default value for <see cref="Key"/> property.</returns>
        protected virtual string ProvideDefaultKey()
        {
            FileVersionInfo fileInfo = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location);
            string company = fileInfo.CompanyName;
            string appName = fileInfo.ProductName;

            if (string.IsNullOrEmpty(company))
                return string.Format(@"SOFTWARE\{0}", appName);

            return string.Format(@"SOFTWARE\{0}\{1}", company, appName);
        }
    }
}
