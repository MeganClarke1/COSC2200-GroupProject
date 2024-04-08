using GalaSoft.MvvmLight.Command;
using Group2_COSC2200_Project.commands;
using Group2_COSC2200_Project.model;
using Group2_COSC2200_Project.stores;
using System.Collections.ObjectModel;
using System.ComponentModel;

using System.Windows;
using System.Windows.Input;

namespace Group2_COSC2200_Project.viewmodel
{
    public class MenuViewModel : ViewModelBase
    { 
        public ICommand NewGameCommand {  get; }
    
        public MenuViewModel(NavigationStore navigationStore)
        {

            NewGameCommand = new NavigateCommand(navigationStore);

        }
    }
}
