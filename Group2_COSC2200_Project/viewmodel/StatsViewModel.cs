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
    public class StatsViewModel : ViewModelBase
    {
        public ICommand BackCommand { get; }

        private Statistics PlayerStats;
        public String _playerName { get; set; }
        public int _playerWins { get; set; }
        public int _playerLosses { get; set; }
        public int _totalGames { get; set; }
        public int _currentStreak {get; set;}


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

        public StatsViewModel(NavigationStore _navigationStore, Statistics _playerStats) 
        {
            BackCommand = new BackCommand(_navigationStore);

            PlayerStats = _playerStats;
            _playerName = PlayerStats.PlayerName;
            _playerWins = PlayerStats.PlayerWins;
            _playerLosses = PlayerStats.PlayerLosses;
            _totalGames = PlayerStats.TotalGames;
            _currentStreak = PlayerStats.CurrentStreak;


            MessageBox.Show(PlayerStats.PlayerID.ToString());


        } 
    }
}
