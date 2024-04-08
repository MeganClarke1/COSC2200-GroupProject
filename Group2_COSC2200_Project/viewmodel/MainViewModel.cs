using Group2_COSC2200_Project.stores;

namespace Group2_COSC2200_Project.viewmodel
{
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

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
