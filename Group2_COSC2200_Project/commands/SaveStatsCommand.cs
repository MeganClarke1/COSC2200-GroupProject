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
    public class SaveStatsCommand : CommandBase
    {
        private Statistics _playerStats;

        public SaveStatsCommand(Statistics playerStats)
        {
            _playerStats = playerStats;
        }

        public override void Execute(object parameter)
        {
            // Fetch stats from JSON file here
            if (Statistics.SaveStatistics(_playerStats))
            {
                MessageBox.Show("Successfully Saved Stats.");
            }

        }
    }

}
