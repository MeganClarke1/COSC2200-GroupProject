namespace Group2_COSC2200_Project.viewmodel
{
    public class MainViewModel : ViewModelBase
    {
        public ViewModelBase CurrentViewModel { get; }

        public MainViewModel() 
        {
            CurrentViewModel = new GameViewModel();
        }
    }
}
