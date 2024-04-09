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
    public class StatsPageCommand : CommandBase
    {
        private readonly NavigationStore _navigationStore;
        private Statistics _playerStats;

        public StatsPageCommand(NavigationStore navigationStore, Statistics playerStats)
        {
            _navigationStore = navigationStore;
            _playerStats = playerStats;
        }

        public override void Execute(object parameter)
        {
            // Fetch stats from JSON file here
            _playerStats = Statistics.LoadStatistics("500");

            //_playerStats = new Statistics(500,"John", 5, 5, 5, 5, Statistics.LastGameResult.Loss);
            // Deserialize JSON and assign to _playerStats

            // Navigate to GameViewModel with fetched stats
            _navigationStore.CurrentViewModel = new StatsViewModel(_navigationStore, _playerStats);
        }
    }

}
