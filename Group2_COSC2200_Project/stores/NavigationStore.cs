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

namespace Group2_COSC2200_Project.stores
{

    /// <summary>
    /// Holds the current value of the view Model among all aspects of the application.
    ///     Navigation Guide:
    ///         1. Button executes function
    ///         2. Execution calls a viewModel constructor
    ///         3. That view model is passed to the navigation Store (will have navigation store as an argument)
    ///         4. Navigation store stores the current ViewModel
    ///         5. Navigation store monitors the currentViewModel property of itself
    ///         6. That property is bound to the MainWindow.
    ///         7. Mainwindow updates with the new ViewModel, which is tied to the View in the data templating in the mainWindow.
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
