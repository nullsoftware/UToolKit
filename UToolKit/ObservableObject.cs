using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NullSoftware
{
    /// <summary>
    /// Base class for objects that require property notification.
    /// </summary>
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        /// <summary>
        /// <see cref="INotifyPropertyChanged.PropertyChanged"/>.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Handles "property changed" operation.
        /// </summary>
        /// <remarks>
        /// Use <see cref="SetProperty{T}(ref T, T, string)"/> instead.
        /// </remarks>
        /// <typeparam name="T">Type of property.</typeparam>
        /// <param name="property">Property field.</param>
        /// <param name="value">New value of property.</param>
        /// <param name="propertyName">Property name.</param>
        [Obsolete("Use SetProperty method instead.", true)]
        protected void OnPropertyChanged<T>(ref T property, T value,
            [CallerMemberName] string propertyName = "")
        {
            SetProperty(ref property, value, propertyName);
        }

        /// <summary>
        /// Raises <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="e">Property changed event arguments.</param>
        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Raises <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Sets property and raises <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <typeparam name="T">Type of property.</typeparam>
        /// <param name="property">Property field.</param>
        /// <param name="value">New value of property.</param>
        /// <param name="propertyName">Property name.</param>
        protected void SetProperty<T>(ref T property, T value,
            [CallerMemberName] string propertyName = "")
        {
            property = value;
            OnPropertyChanged(propertyName);
        }
    }
}
