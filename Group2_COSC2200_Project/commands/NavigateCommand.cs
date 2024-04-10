/// <file>
///   <summary>
///     File Name: NavigateCommand.cs
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
///     Description: This will be used as a navigation command for moving from the menu to the game. To be used 
///         for "New Game", if the feature is utilized. (Player stats not being fetched)
///   </description>
/// </file>

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
    /// //Test
    public class NavigateCommand : CommandBase
    {
        // The navigation store property monitors the navigationStore property (which when changed will dynamically 
        // adjust the view.
        private readonly NavigationStore _navigationStore;

        private Statistics _playerStats; 

        /// <summary>
        /// On construction of NavigateCommand, pass the navigationStore (view) to be navigated to.
        /// </summary>
        /// <param name="navigationStore"> The view to be navigated too </param>
        /// <param name="playerStats"> The player stats to be passed to the view. </param>
        public NavigateCommand(NavigationStore navigationStore, Statistics playerStats)
        {
            _navigationStore = navigationStore;
            _playerStats = playerStats;
        }

        /// <summary>
        /// Override the base call method to navigate to the GameViewModel, passing the players stats.
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object parameter)
        {
            _navigationStore.CurrentViewModel = new GameViewModel(_playerStats, _navigationStore);
        }
    }
}
