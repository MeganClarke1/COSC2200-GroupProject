/// <file>
///   <summary>
///     File Name: StatsViewModel.cs
///   </summary>
///   <author>
///     Authors: Brody Dentinger, Megan Clarke, Colin Eade, Muhammad Yasir Patel
///   </author>
///   <created>
///     Created: Apr 8, 2024
///   </created>
///   <lastModified>
///     Last Modified: April 9, 2024
///   </lastModified>
///   <description>
///     Description: This class represents the StatsViewModel, which is the screen that players view their stats.
///   </description>
/// </file>

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
    /// Represents the stats view model.
    /// </summary>
    public class StatsViewModel : ViewModelBase
    {
        /// <summary>
        /// Defining the command for the back button to travel back to menu.
        /// </summary>
        public ICommand BackCommand { get; }

        /// <summary>
        /// Represents the players stats.
        /// </summary>
        private Statistics PlayerStats;

        /// <summary>
        /// Represents the player's name.
        /// </summary>
        public String _playerName { get; set; }

        /// <summary>
        /// Represents the players wins.
        /// </summary>
        public int _playerWins { get; set; }

        /// <summary>
        /// Represents the players losses.
        /// </summary>
        public int _playerLosses { get; set; }

        /// <summary>
        /// Represents the players total games.
        /// </summary>
        public int _totalGames { get; set; }

        /// <summary>
        /// Represents the current streak of the player.
        /// </summary>
        public int _currentStreak {get; set;}

        /// <summary>
        /// The following section serves to monitor for changes to the classes properties being OnPropertyChanged, which will
        ///     dynamically update the view, because the fields on the view are bound to these properties values.
        /// </summary>
        public String PlayerName
        {
            get => _playerName;
            set
            {
                if (_playerName != value)
                {
                    _playerName = value;
                    OnPropertyChanged(nameof(PlayerName));
                }
            }
        }

        public int PlayerWins
        {
            get => _playerWins;
            set
            {
                if (_playerWins != value)
                {
                    _playerWins = value;
                    OnPropertyChanged(nameof(PlayerWins));
                }
            }
        }

        public int PlayerLosses
        {
            get => _playerLosses;
            set
            {
                if (_playerLosses != value)
                {
                    _playerLosses = value;
                    OnPropertyChanged(nameof(PlayerLosses));
                }
            }
        }

        public int TotalGames
        {
            get => _totalGames;
            set
            {
                if (_totalGames != value)
                {
                    _totalGames = value;
                    OnPropertyChanged(nameof(TotalGames));
                }
            }
        }

        public int CurrentStreak
        {
            get => _currentStreak;
            set
            {
                if (_currentStreak != value)
                {
                    _currentStreak = value;
                    OnPropertyChanged(nameof(CurrentStreak));
                }
            }
        }


        /// <summary>
        /// The Constructor for the StatsViewModel. Takes the navigation viewModel and player stats as arugments.
        ///     Renders the player's stats based on what has been returned via the JSON fetch from the Menu command.
        ///     Additionally, defines the BackCommand to navigate back to the main menu. This is bound to the back button on the view.
        /// </summary>
        /// <param name="_navigationStore"></param>
        /// <param name="_playerStats"></param>
        public StatsViewModel(NavigationStore _navigationStore, Statistics _playerStats) 
        {
            BackCommand = new BackCommand(_navigationStore);

            PlayerStats = _playerStats;
            _playerName = PlayerStats.PlayerName;
            _playerWins = PlayerStats.PlayerWins;
            _playerLosses = PlayerStats.PlayerLosses;
            _totalGames = PlayerStats.TotalGames;
            _currentStreak = PlayerStats.CurrentStreak;
        } 
    }
}
