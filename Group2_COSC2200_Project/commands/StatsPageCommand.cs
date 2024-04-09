/// <file>
///   <summary>
///     File Name: StatsPageCommand.cs
///   </summary>
///   <author>
///     Authors: Brody Dentinger, Megan Clarke, Colin Eade, Muhammad Yasir Patel
///   </author>
///   <created>
///     Created: April 9, 2024
///   </created>
///   <lastModified>
///     Last Modified: April 9, 2024
///   </lastModified>
///   <description>
///     Description: This will be used as a navigation command for moving from the menu to the stats page. The user's stats 
///         will be dynamically populated on the stats page from the stats.json file.
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
    /// This class represents the command to move to the stats fetch on click on the stats button on menu.
    /// </summary>
    public class StatsPageCommand : CommandBase
    {
        /// <summary>
        /// The navigation view model to be requested.
        /// </summary>
        private readonly NavigationStore _navigationStore;
        /// <summary>
        /// The player's statistics to be rendered.
        /// </summary>
        private Statistics _playerStats;

        /// <summary>
        /// The constructor for this command takes a requested ViewModel, and the player's statistics object.
        /// </summary>
        /// <param name="navigationStore"> The ViewModel to navigate to. </param>
        /// <param name="playerStats"> The player's stats to show on the screen. </param>
        public StatsPageCommand(NavigationStore navigationStore, Statistics playerStats)
        {
            _navigationStore = navigationStore;
            _playerStats = playerStats;
        }

        /// <summary>
        /// Override the base execution method to load the statistics for the player profile, and navigate to the 
        ///     Stats page, with those statistics in hand.
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object parameter)
        {
            // Fetch stats from JSON file here
            _playerStats = Statistics.LoadStatistics("500");

            // Navigate to GameViewModel with fetched stats
            _navigationStore.CurrentViewModel = new StatsViewModel(_navigationStore, _playerStats);
        }
    }
}
