using System;
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
    public class SettingsStorage : MarkupExtension, IWindowPlacementStorage
    {
        public ApplicationSettingsBase Settings { get; set; }

        public bool IsSaveSettingsEnabled { get; set; }

        /// <summary>
        /// Gets or sets custom value name that will be used
        /// to retrive or set window placement to registry.
        /// </summary>
        /// <remarks>
        /// Note: if this name is not null, it will be used instead of auto-generated.
        /// </remarks>
        public string CustomName { get; set; }

        /// <inheritdoc/>

        public byte[] LoadPlacement(Window window)
        {
            string name = GetSettingKey(window);

            if (!Settings.Properties.Cast<SettingsProperty>().Any(t => t.Name == name))
            {
                var prop = new SettingsProperty(name);
                prop.PropertyType = typeof(byte[]);
                prop.Provider = Settings.Providers["LocalFileSettingsProvider"];
                prop.Attributes.Add(typeof(UserScopedSettingAttribute), new UserScopedSettingAttribute());

                Settings.Properties.Add(prop);
            }

            return (byte[])Settings[name];
        }

        /// <inheritdoc/>
        public void SavePlacement(Window window, byte[] serializedPlacement)
        {
            string name = GetSettingKey(window);

            if (!Settings.Properties.Cast<SettingsProperty>().Any(t => t.Name == name))
                Settings.Properties.Add(new SettingsProperty(name) { PropertyType = typeof(byte[]), Provider = new LocalFileSettingsProvider() });

            Settings[name] = serializedPlacement;

            if (IsSaveSettingsEnabled)
                Settings.Save();
        }

        /// <inheritdoc/>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Settings is null)
                throw new InvalidOperationException("Missing settings instance.");

            return this;
        }

        /// <summary>
        /// Gets registry setting key for specified window.
        /// </summary>
        /// <param name="window">Window to get setting key.</param>
        /// <returns>The registry setting key for specified window.</returns>
        protected virtual string GetSettingKey(Window window)
        {
            return CustomName ?? window.GetType().Name + "Placement";
        }

    }
}
