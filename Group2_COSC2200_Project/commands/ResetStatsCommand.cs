/// <file>
///   <summary>
///     File Name: ResetStatsCommand.cs
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
///     Description: This will be used as a reset stats command to be executed on click of the reset stats button on the stats View.
///         Will reset the players stats back to 0.
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
    /// SaveStatsCommand will be used for when a player resets stats from menu.
    /// </summary>
    public class ResetStatsCommand : CommandBase
    {
        /// <summary>
        /// The Navigation CurrentViewModel that will be stored in our navigation store.
        /// </summary>
        private readonly NavigationStore _navigationStore;

        /// <summary>
        /// The statistics object to be passed into the json.
        /// </summary>
        private Statistics _playerStats;

        /// <summary>
        /// The constructor of this command takes a players stats object, and updates the field for this class.
        /// </summary>
        /// <param name="playerStats"></param>
        public ResetStatsCommand(NavigationStore navigationStore, Statistics playerStats)
        {
            _playerStats = playerStats;
            _navigationStore = navigationStore;
        }

        /// <summary>
        /// Overrides the execution of the base class to save the statistics to the stats.json file
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object parameter)
        {
            // Fetch stats from JSON file here
            if (Statistics.ResetStatistics(_playerStats)) 
                                                         
            {
                // Navigate back to the stats view to refresh.
                _navigationStore.CurrentViewModel = new StatsViewModel(_navigationStore, _playerStats);
               
                // Notify user of update.
                MessageBox.Show("Successfully Reset Stats.");
            }
        }
    }
}
