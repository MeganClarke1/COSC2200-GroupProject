using Group2_COSC2200_Project.model;
using Group2_COSC2200_Project.stores;
using Group2_COSC2200_Project.viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group2_COSC2200_Project.commands
{
    /// <summary>
    /// The navigate command will be used to navigate between view Models.
    /// </summary>
    public class NavigateCommand : CommandBase
    {
        // The navigation store property monitors the navigationStore property (which when changed will dynamically 
        // adjust the view.
        private readonly NavigationStore _navigationStore;

        private Statistics _playerStats; // ******

        /// <summary>
        /// On construction of NavigateCommand, pass the navigationStore (view) to be navigated to.
        /// </summary>
        /// <param name="navigationStore"> The view to be navigated too </param>
        public NavigateCommand(NavigationStore navigationStore, Statistics playerStats) // **** added player stats param. and arg.
        {
            _navigationStore = navigationStore;
            _playerStats = playerStats;
        }

        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = new GameViewModel(_playerStats); // I think we can pass data here ? into the constructor of the GameViewModel?
        }
    }
}
