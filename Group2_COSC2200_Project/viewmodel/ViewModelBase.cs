/// <file>
///   <summary>
///     File Name: ViewModelBase.cs
///   </summary>
///   <author>
///     Authors: Brody Dentinger, Megan Clarke, Colin Eade, Muhammad Yasir Patel
///   </author>
///   <created>
///     Created: Apr 1, 2024
///   </created>
///   <lastModified>
///     Last Modified: April 9, 2024
///   </lastModified>
///   <description>
///     Description: This class represents the base class for all ViewModels to inherit from.
///   </description>
/// </file>

using System.ComponentModel;

namespace Group2_COSC2200_Project.viewmodel
{
    /// <summary>
    /// Serves as the foundation for all ViewModel classes in an application. This class implements the 
    /// INotifyPropertyChanged interface, which supports binding by notifying the view of changes to property values 
    /// within the ViewModel.
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Fired when a property on this ViewModel has changed. Binding mechanisms in the view use this event to 
        /// update the UI accordingly.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Invokes the PropertyChanged event, signaling to any bound controls that the value of a property has changed.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
