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
    /// <summary>
    /// Represnts the menu view Model.
    /// </summary>
    public class MenuViewModel : ViewModelBase
    { 
        /// <summary>
        /// The NewGameCommand ICommand implementation.
        /// </summary>
        public ICommand NewGameCommand {  get; }
        public ICommand ContinueCommand { get; } // *** Added new continue command
        
        /// <summary>
        /// Binds the NewGameCommand to the MenuViewModel. Will be used to navigate to the GameView.
        /// </summary>
        /// <param name="navigationStore"> The view to be navigated to </param>
        public MenuViewModel(NavigationStore navigationStore, Statistics playerStats) // ** added the playerstats param
        {

            NewGameCommand = new NavigateCommand(navigationStore, null); // **** passing null stats to new game (eventually needs to pass a text field with player name... or a blank stats object with player name input)
            ContinueCommand = new FetchStatsCommand(navigationStore, playerStats); // *** passing playerStats arg

        }
    }
}
