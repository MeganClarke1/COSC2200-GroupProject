using Group2_COSC2200_Project.model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Group2_COSC2200_Project.viewmodel
{
    public class GameViewModel : ViewModelBase
    {
        /// <summary>
        ///  Initilize a StartGameState object that contains all properties required to start the game
        /// </summary>
        private StartGameState _startGameState;

        // Dictionary to store the hand collections for each player.
        private Dictionary<string, ObservableCollection<CardViewModel>> _playerHands;

        // Property to access the player hands dictionary.
        public Dictionary<string, ObservableCollection<CardViewModel>> PlayerHands
        {
            get { return _playerHands; }
            set
            {
                // Set the player hands dictionary.
                _playerHands = value;

                // Notify the UI that the PlayerHands property has changed.
                OnPropertyChanged(nameof(PlayerHands));
            }
        }


        // Represents the name of the current player whose turn it is.
        public string CurrentPlayerName { get; private set; }

        /// <summary>
        /// Initialize the GameViewModel
        /// </summary>
        /// <param name="startGameState"></param>
        public GameViewModel(StartGameState startGameState) 
        {
    
            _startGameState = startGameState;

            _playerHands = new Dictionary<string, ObservableCollection<CardViewModel>>();

            // Initialize hand collections for each player ??? Where are players?
            foreach (var player in Player)
            {
                _playerHands[playerName] = new ObservableCollection<CardViewModel>();
            }

            UpdateGameState();

        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateGameState()
        {
            // Clear all player hand collections
            foreach (var playerHand in _playerHands.Values)
            {
                playerHand.Clear();
            }

            // Populate hand collections for each player based on game state // 
            foreach (var playerName in PlayerNames)
            {
                var playerHand = _playerHands[playerName];
                // Populate playerHand with cards based on game state for playerName
                foreach (Card card in _startGameState.GetPlayerHand(playerName))
                {
                    playerHand.Add(new CardViewModel(card));
                }
            }

            // Set the name of the current player
            CurrentPlayerName = _startGameState.gameTurnList[0].PlayerName;

            
        }
    }

}
