/// <file>
///   <summary>
///     File Name: SaveStatsCommand.cs
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
///     Description: This will be used as a save stats command for when the user clicks "save stats" in the menu.
///         Takes a statistics object and replaces the current stats record in the stats.json file with updated stats.
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
    /// SaveStatsCommand will be used for when a player saves stats from menu.
    /// </summary>
    public class SaveStatsCommand : CommandBase
    {
        /// <summary>
        /// The statistics object to be passed into the json.
        /// </summary>
        private Statistics _playerStats;

        /// <summary>
        /// The constructor of this command takes a players stats object, and updates the field for this class.
        /// </summary>
        /// <param name="playerStats"></param>
        public SaveStatsCommand(Statistics playerStats)
        {
            _playerStats = playerStats;
        }

        /// <summary>
        /// Overrides the execution of the base class to save the statistics to the stats.json file
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object parameter)
        {
            if (_playerStats != null)
            {
                // Fetch stats from JSON file here
                if (Statistics.SaveStatistics(_playerStats))

                {
                    MessageBox.Show("Successfully Saved Stats.");
                }
            }
            else
            {
                MessageBox.Show("Save Currently Unavailable");
            }
        }
    }
}
