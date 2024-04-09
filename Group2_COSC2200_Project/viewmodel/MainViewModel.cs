/// <file>
///   <summary>
///     File Name: MainViewModel.cs
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
///     Description: This class represents the MainViewModel, This viewModel is a traffic light that monitors the currentViewModel
///         and renders various Views based on what argument is passed to the navigationStore.
///   </description>
/// </file>

using Group2_COSC2200_Project.stores;

namespace Group2_COSC2200_Project.viewmodel
{
    /// <summary>
    /// To act as the main routing for our ViewModels.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {

        // 3. get the navigationStore as a field in the mainViewModel (our mainViewModel needs the navigationStore
        // to determine which view to use
        private readonly NavigationStore _navigationStore;

        // 4. Storing 
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        //public ViewModelBase CurrentViewModel { get; }

        public MainViewModel(NavigationStore navigationStore) 
        {
            _navigationStore = navigationStore;
            //CurrentViewModel = new GameViewModel();

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        /// <summary>
        /// Monitor for changes to the currentViewModel.
        /// </summary>
        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
