/// <file>
///   <summary>
///     File Name: NavigationStore.cs
///   </summary>
///   <author>
///     Authors: Brody Dentinger, Megan Clarke, Colin Eade, Muhammad Yasir Patel
///   </author>
///   <created>
///     Created: April 8, 2024
///   </created>
///   <lastModified>
///     Last Modified: April 9, 2024
///   </lastModified>
///   <description>
///     Description: This class stores an instance of the ViewModel variable as the currentViewModel.
///         Used to navigate between views by altering this property.
///   </description>
/// </file>

using Group2_COSC2200_Project.viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Group2_COSC2200_Project.stores
{

    /// <summary>
    /// Holds the current value of the view Model among all aspects of the application.
    /// </summary>
    public class NavigationStore
    {
        /// <summary>
        /// The currentViewModel property to be monitored.
        /// </summary>
        private ViewModelBase _currentViewModel; //Using OUR viewModelBase (not gala's)

        /// <summary>
        /// Dynamically set the value of the CurrentViewModel 
        /// </summary>
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        /// <summary>
        /// To monitor the changing of the current view model. To be used below to notify the window when a new view model
        /// has been loaded.
        /// </summary>
        public event Action CurrentViewModelChanged;

        /// <summary>
        /// Notify when a ViewModel has been changed. Passes the new currentViewModel to the navigation Store for rendering.
        /// </summary>
        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
