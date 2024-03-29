﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Markup;

namespace NullSoftware.ToolKit
{
    /// <summary>
    /// Provides logic to handle window placement saving/loading using <see cref="ApplicationSettingsBase"/>.
    /// </summary>
    public class SettingsStorage : MarkupExtension, IWindowPlacementStorage
    {
        /// <summary>
        /// Gets or sets main settings instance
        /// that will be used to store window placement.
        /// </summary>
        public ApplicationSettingsBase Settings { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the settings
        /// should be saved within the <see cref="SavePlacement(Window, byte[])"/> method.
        /// </summary>
        public bool IsSaveSettingsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets name format that will be used
        /// to retrive or set window placement to application settings.
        /// </summary>
        public string NameFormat { get; set; } = "{0}Placement";

        /// <inheritdoc/>
        public byte[] LoadPlacement(Window window)
        {
            string name = GetSettingKey(window);

            EnsurePropertyCreated(name);

            return (byte[])Settings[name];
        }

        /// <inheritdoc/>
        public void SavePlacement(Window window, byte[] serializedPlacement)
        {
            string name = GetSettingKey(window);

            EnsurePropertyCreated(name);

            Settings[name] = serializedPlacement;

            if (IsSaveSettingsEnabled)
                Settings.Save();
        }

        /// <inheritdoc/>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Settings is null)
                throw new InvalidOperationException("Missing Settings instance.");
            if (string.IsNullOrWhiteSpace(NameFormat))
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
        /// Ensures that specified property is created.
        /// </summary>
        /// <param name="name">Property name to create if not exists.</param>
        protected virtual void EnsurePropertyCreated(string name)
        {
            if (!Settings.Properties.Cast<SettingsProperty>().Any(t => t.Name == name))
            {
                var prop = new SettingsProperty(name);
                prop.PropertyType = typeof(byte[]);
                prop.SerializeAs = SettingsSerializeAs.Xml;
                prop.Provider = Settings.Providers[nameof(LocalFileSettingsProvider)];
                prop.Attributes.Add(typeof(UserScopedSettingAttribute), new UserScopedSettingAttribute());
                //prop.Attributes.Add(typeof(DebuggerNonUserCodeAttribute), new DebuggerNonUserCodeAttribute());

                Settings.Properties.Add(prop);
            }
        }
    }
}
