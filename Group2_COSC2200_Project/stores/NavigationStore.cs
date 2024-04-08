using Group2_COSC2200_Project.viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Group2_COSC2200_Project.stores
{
    // 1. Navigation Store 
    /// <summary>
    /// Holds the current value of the view Model among all aspects of the application.
    /// </summary>
    public class NavigationStore
    {

        private ViewModelBase _currentViewModel; //Using OUR viewModelBase (not gala's)

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        public event Action CurrentViewModelChanged;

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
