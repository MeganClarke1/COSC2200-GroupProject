/// <file>
///   <summary>
///     File Name: FetchStatsCommand.cs
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
///     Description: This command is used to "Continue" a game. It will fetch the players stats from the json, and 
///         redirect them to the GameView with their player data.
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
using System.Windows;

namespace Group2_COSC2200_Project.commands
{
    /// <summary>
    /// For use in the "Continue" button on the menu. Fetches player stats and navigates to gameView.
    /// </summary>
    public class FetchStatsCommand : CommandBase
    {
        /// <summary>
        /// The Navigation CurrentViewModel that will be stored in our navigation store.
        /// </summary>
        private readonly NavigationStore _navigationStore;

        /// <summary>
        /// The player's statistics to be fetched from the json.
        /// </summary>
        private Statistics _playerStats;

        /// <summary>
        /// The fetchStatsCommand constructor.
        /// </summary>
        /// <param name="navigationStore"> The view to be navigated to </param>
        /// <param name="playerStats"> The player stats to be passed from the command. </param>
        public FetchStatsCommand(NavigationStore navigationStore, Statistics playerStats)
        {
            _navigationStore = navigationStore;
            _playerStats = playerStats;
        }

        /// <summary>
        /// Override the base class Execute to LoadStatistics for the given Player, store them, then
        ///     change the CurrentViewModel in the navigation store to the gameViewModel, passing the fetched player stats.
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object parameter)
        {
            // Fetch stats from JSON file here
            _playerStats = Statistics.LoadStatistics("500");

            // Navigate to GameViewModel with fetched stats
            _navigationStore.CurrentViewModel = new GameViewModel(_playerStats);
        }
    }

}
