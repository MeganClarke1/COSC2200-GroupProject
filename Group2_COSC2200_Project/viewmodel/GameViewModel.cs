using GalaSoft.MvvmLight.Command;
using Group2_COSC2200_Project.model;
using System.Collections.ObjectModel;
using System.ComponentModel;

using System.Windows;
using System.Windows.Input;

namespace Group2_COSC2200_Project.viewmodel
{
    public class GameViewModel : ViewModelBase
    {
        private Game _game = new();

        public ICommand OrderUpCommand { get; private set; }
        public ICommand PassCommand { get; private set; }
        public ICommand StartCommand { get; private set; }
        public ICommand PlayCardCommand { get; private set; }

        private HandViewModel _player1Hand;
        private HandViewModel _player2Hand;
        private HandViewModel _player3Hand;
        private HandViewModel _player4Hand;
        private KittyViewModel _kitty;
        private PlayAreaViewModel _playArea;

        private Deck _deck;
        private bool _player1Turn;
        private bool _player2Turn;
        private bool _player3Turn;
        private bool _player4Turn;
        private Team _teamOne;
        private Team _teamTwo;
        private int _teamOneTricks;
        private int _teamTwoTricks;
        private Scoreboard _scoreBoard;
        private Visibility _started = Visibility.Visible;

        public Scoreboard Scoreboard
        {
            get => _scoreBoard;
            set
            {
                if (_scoreBoard != value)
                {
                    _scoreBoard = value;
                    OnPropertyChanged(nameof(Scoreboard));
                }
            }
        }

        public int TeamOneTricks
        {
            get => _teamOneTricks;
            set
            {
                if (_teamOneTricks != value)
                {
                    _teamOneTricks = value;
                    OnPropertyChanged(nameof(TeamOneTricks));
                }
            }
        }

        public int TeamTwoTricks
        {
            get => _teamTwoTricks;
            set
            {
                if (_teamTwoTricks != value)
                {
                    _teamTwoTricks = value;
                    OnPropertyChanged(nameof(TeamTwoTricks));
                }
            }
        }

        public Team TeamOne
        {
            get => _teamOne;
            set
            {
                if (_teamOne != value)
                {
                    _teamOne = value;
                    OnPropertyChanged(nameof(TeamOne));
                }
            }
        }

        public Team TeamTwo
        {
            get => _teamTwo;
            set
            {
                if (_teamTwo != value)
                {
                    _teamTwo = value;
                    OnPropertyChanged(nameof(TeamTwo));
                }
            }
        }

        public PlayAreaViewModel PlayArea
        {
            get => _playArea;
            set
            {
                if (_playArea != value)
                {
                    _playArea = value;
                    OnPropertyChanged(nameof(PlayArea));
                }
            }
        }

        public HandViewModel Player1Hand
        {
            get => _player1Hand;
            set
            {
                if (_player1Hand != value)
                {
                    _player1Hand = value;
                    OnPropertyChanged(nameof(Player1Hand));
                }
            }
        }

        public HandViewModel Player2Hand
        {
            get => _player2Hand;
            set
            {
                if (_player2Hand != value)
                {
                    _player2Hand = value;
                    OnPropertyChanged(nameof(Player2Hand));
                }
            }
        }

        public HandViewModel Player3Hand
        {
            get => _player3Hand;
            set
            {
                if (_player3Hand != value)
                {
                    _player3Hand = value;
                    OnPropertyChanged(nameof(Player3Hand));
                }
            }
        }

        public HandViewModel Player4Hand
        {
            get => _player4Hand;
            set
            {
                if (_player4Hand != value)
                {
                    _player4Hand = value;
                    OnPropertyChanged(nameof(Player4Hand));
                }
            }
        }

        public KittyViewModel Kitty
        {
            get => _kitty;
            set
            {
                if (_kitty != value)
                {
                    _kitty = value;
                    OnPropertyChanged(nameof(Kitty));
                }
            }
        }

        public Deck Deck
        {
            get => _deck;
            set
            {
                if (_deck != value)
                {
                    _deck = value;
                    OnPropertyChanged(nameof(Deck));
                }
            }
        }

        public bool Player1Turn
        {
            get { return _player1Turn; }
            private set
            {
                if (_player1Turn != value)
                {
                    _player1Turn = value;
                    OnPropertyChanged(nameof(Player1Turn));
                }
            }
        }

        public bool Player2Turn
        {
            get { return _player2Turn; }
            private set
            {
                if (_player2Turn != value)
                {
                    _player2Turn = value;
                    OnPropertyChanged(nameof(Player2Turn));
                }
            }
        }

        public bool Player3Turn
        {
            get { return _player3Turn; }
            private set
            {
                if (_player3Turn != value)
                {
                    _player3Turn = value;
                    OnPropertyChanged(nameof(Player3Turn));
                }
            }
        }

        public bool Player4Turn
        {
            get { return _player4Turn; }
            private set
            {
                if (_player4Turn != value)
                {
                    _player4Turn = value;
                    OnPropertyChanged(nameof(Player4Turn));
                }
            }
        }

        public Visibility Started
        {
            get => _started;
            set
            {
                if (_started != value)
                {
                    _started = value;
                    OnPropertyChanged(nameof(Started));
                }
            }
        }

        // public bool Player2Turn => _game.CurrentPlayer == _game.PlayerTwo && _game.trumpFromKitty;

        public GameViewModel()
        {   
            UpdateViewModelState();
            OrderUpCommand = new RelayCommand<object>(OrderUpExecute, CanOrderUpExecute);
            PassCommand = new RelayCommand<object>(PassExecute, CanPassExecute);
            StartCommand = new RelayCommand<object>(StartExecute, CanStartExecute);
            PlayCardCommand = new RelayCommand<object>(PlayCardExecute, CanPlayCardExecute);
        }

        private void UpdateViewModelState()
        {
            switch (_game.CurrentState)
            {
                case Game.GameState.Initialize:
                    // 
                    break;
                case Game.GameState.Start:
                    Start();
                    break;
                case Game.GameState.Deal:
                    // 
                    break;
                case Game.GameState.TrumpSelectionFromKitty:
                    TrumpSelectionFromKitty();
                    break;
            }
        }

        private void OrderUpExecute(object parameter)
        {
            _game.OrderUp();
            UpdateViewModelState();
        }

        private bool CanOrderUpExecute(object parameter)
        {
            return true;
        }

        private void PassExecute(object parameter)
        {
            _game.Pass();
            UpdateViewModelState();
        }

        private bool CanPassExecute(object parameter)
        {
            return true;
        }

        private void StartExecute(object parameter)
        {
            _game.Start();
            UpdateViewModelState();
            _game.TrumpSelectionFromKitty();
            UpdateViewModelState();
        }

        private bool CanStartExecute(object parameter)
        {
            return true;
        }

        private void Start()
        {
            Started = Visibility.Collapsed;
            Player1Hand = new HandViewModel(_game.PlayerOne.PlayerHand);
            Player2Hand = new HandViewModel(_game.PlayerTwo.PlayerHand);
            Player3Hand = new HandViewModel(_game.PlayerThree.PlayerHand);
            Player4Hand = new HandViewModel(_game.PlayerFour.PlayerHand);
            Kitty = new KittyViewModel(_game.Kitty);
            PlayArea = new PlayAreaViewModel(_game.PlayArea);
            TeamOne = new Team(_game.Team1.TeamId, _game.Team1.TeamPlayers);
            TeamTwo = new Team(_game.Team2.TeamId, _game.Team2.TeamPlayers);
            Scoreboard = new Scoreboard();
        }

        private void TrumpSelectionFromKitty()
        {
            Player1Turn = _game.CurrentPlayer == _game.PlayerOne;
            Player2Turn = _game.CurrentPlayer == _game.PlayerTwo;
            Player3Turn = _game.CurrentPlayer == _game.PlayerThree;
            Player4Turn = _game.CurrentPlayer == _game.PlayerFour;
        }

        /// <summary>
        /// Adds a card from a hand to the play area, removes it from hand.
        /// If it is the 4th card to be played, automatically evaulates the winning result of the play area. 
        /// Is bound to a button on each card in a player's hand. When clicked, will passed the clicked object to this
        /// function, and add that clicked card to the PlayArea of the game instance. Sets the GameViewModel property PlayArea
        /// to the newly add to game instance play area.
        /// 
        /// </summary>
        /// <param name="parameter"> The object passed from the Command {binding} in the GameView.xaml. In this case, will be a 
        /// CardViewModel object of the clicked card. </param>
        private void PlayCardExecute(object parameter)
        {


            // This is fetching a cardViewModel when a card is clicked
            // Therefore, must fetch that cardViewModel's .Card property (which is the card object)
            if (parameter is CardViewModel cardViewModel)
            {
                Card clickedCard = cardViewModel.Card;
                _game.AddCardtoPlayAreaTest(clickedCard);
                PlayArea = new PlayAreaViewModel(_game.PlayArea);

                // Remove the card from the hand that is represented by the CURRENT TURN
                if (_game.CurrentPlayer == _game.PlayerOne)
                    Player1Hand.Cards.Remove(cardViewModel);
                else if (_game.CurrentPlayer == _game.PlayerTwo)
                    Player2Hand.Cards.Remove(cardViewModel);           // This is directly modifying ONLY the GameViewModel props...
                                                                        // Is that bad? do we also need to update Model stuff?
                else if (_game.CurrentPlayer == _game.PlayerThree)    // Probably could create a remove card function
                    Player3Hand.Cards.Remove(cardViewModel);          // or a function for this logic to clean this area up.
                else if (_game.CurrentPlayer == _game.PlayerFour)
                    Player4Hand.Cards.Remove(cardViewModel);

                // Pass turns and update the buttons 
                _game.Pass();
                    
                // 4th Card HAS been played. (Checked after every play)
                if (_game.PlayArea.Count >= 4)
                {
                    MessageBox.Show("Everyone has played a card. Time to see who wins!");
                    Team winningTeam = Trick.DetermineTrickWinner(_game.PlayArea, _game.Team1, _game.Team2);
                    MessageBox.Show("Winning Team: " + winningTeam.TeamId);

                    // Increment the trick counter based on winning team
                    if (winningTeam.TeamId == Team.TeamID.TeamOne)
                    {
                        TeamOneTricks++;
                        //Scoreboard.IncrementTrickCount(TeamOne);   // **** When trying to do this same method from the Scoreboard obj, doesnt work .... ***
                    }
                    else if (winningTeam.TeamId == Team.TeamID.TeamTwo)
                    {
                        TeamTwoTricks++;
                        //Scoreboard.IncrementTrickCount(TeamTwo);
                    }

                    // Clear the _game instances playArea, and set the ViewModel prop to it's reset (empty) value.
                    _game.PlayArea.Clear(); 
                    PlayArea = new PlayAreaViewModel(_game.PlayArea);
                }
            }   
        }

        private bool CanPlayCardExecute(object parameter)
        {
            return true;
        }
    }

}
