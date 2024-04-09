/// <file>
///   <summary>
///     File Name: GameViewModel.cs
///   </summary>
///   <author>
///     Authors: Brody Dentinger, Megan Clarke, Colin Eade, Muhammad Yasir Patel
///   </author>
///   <created>
///     Created: Apr 1, 2024
///   </created>
///   <lastModified>
///     Last Modified: April 9, 2024
///   </lastModified>
///   <description>
///     Description: This class represents the GameViewModel, which is the main view when players are playing a game.
///   </description>
/// </file>

using GalaSoft.MvvmLight.Command;
using Group2_COSC2200_Project.commands;
using Group2_COSC2200_Project.model;
using System.Collections.ObjectModel;
using System.ComponentModel;

using System.Windows;
using System.Windows.Input;
using static Group2_COSC2200_Project.model.Game;

namespace Group2_COSC2200_Project.viewmodel
{
    /// <summary>
    /// The GameViewModel represents the view when user is playing the game.
    /// </summary>
    public class GameViewModel : ViewModelBase
    {
        /// <summary>
        /// Constructs a new game object automatically.
        /// </summary>
        private Game _game = new();

        /// <summary>
        /// Command to order up a kitty suit.
        /// </summary>
        public ICommand OrderUpCommand { get; private set; }
        /// <summary>
        /// Command to pass on the kitty suit.
        /// </summary>
        public ICommand PassCommand { get; private set; }
        /// <summary>
        /// Command to order a trump suit up after the initial ktty round.
        /// </summary>
        public ICommand OrderUpPostKittyCommand {  get; private set; }
        /// <summary>
        /// Command to pass a trump suit up after the initial kitty round.
        /// </summary>
        public ICommand PassPostKittyCommand { get; private set; }
        /// <summary>
        /// Command to start the game. (deal cards, create teams, etc)
        /// </summary>
        public ICommand StartCommand { get; private set; }
        /// <summary>
        /// Command to be executed when a player clicks a card to play
        /// </summary>
        public ICommand ClickCardCommand { get; private set; }
        /// <summary>
        /// Command to exectute when save stats button is clicked.
        /// </summary>
        public ICommand SaveStatsCommand { get; private set; }

        /// <summary>
        /// Initailizing properties to be monitored by the ViewModel (Automatic changes to the view require this)
        /// </summary>
        private HandViewModel _player1Hand;
        private HandViewModel _player2Hand;
        private HandViewModel _player3Hand;
        private HandViewModel _player4Hand;
        private KittyViewModel _kitty;
        private PlayAreaViewModel _playArea;

        /// <summary>
        /// Setting visibility of the player turns to collapsed so it can't be seen.
        /// </summary>
        private Visibility _player1Turn = Visibility.Collapsed;
        private Visibility _player2Turn = Visibility.Collapsed;
        private Visibility _player3Turn = Visibility.Collapsed;
        private Visibility _player4Turn = Visibility.Collapsed;

        /// <summary>
        /// Setting the visibility for Turns post initial kitty round to also be unseen.
        /// </summary>
        private Visibility _player1PostKittyTurn = Visibility.Collapsed;

        /// <summary>
        /// Tracking boolean status for is players can click their cards based on if it's their turn.
        /// </summary>
        private bool _player1CanClickCard;
        private bool _player2CanClickCard;
        private bool _player3CanClickCard;
        private bool _player4CanClickCard;

        /// <summary>
        /// Initializing a list of Suits that are available for selection as the trump suit after the kitty round.
        /// </summary>
        private List<Card.Suits> _nonKittySuits;

        /// <summary>
        /// Initializing more properties to keep track of via IMontior for view changes. Will be mutated to update the view.
        /// </summary>
        private Team _teamOne;
        private Team _teamTwo;
        private int _teamOneTricks;
        private int _teamTwoTricks;
        private int _teamOneScore;
        private int _teamTwoScore;
        private Scoreboard _scoreBoard;

        /// <summary>
        /// Setting initial visibiltiy of the started property to visible.
        /// </summary>
        private Visibility _started = Visibility.Visible;

        /// <summary>
        /// Hiding these properties to create invisible tracks not seen by the user.
        /// </summary>
        private Visibility _hasTrumpSuit = Visibility.Collapsed;
        private Visibility _hasLeadSuit = Visibility.Collapsed;

        /// <summary>
        /// Intializing empty Strings to store the trump suit and lead suit for comparison during play.
        /// </summary>
        private String _trumpSuit = "";
        private String _leadSuit = "";

        /// <summary>
        /// A container for storage of the player's statistics as passed to this view by the MenuView.
        /// </summary>
        private Statistics _GVMplayerStats;

        /// <summary>
        /// A public property for setting via the player's statistics to dyanmically update player one's name on the view.
        /// </summary>
        public String _playerName;

        /// <summary>
        /// Monitoring the playername for changes.
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

        /// <summary>
        /// Montioring the scoreboard for changes.
        /// </summary>
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

        /// <summary>
        /// Montioring team one tricks for changes.
        /// </summary>
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

        /// <summary>
        /// Montioring teamTwo tricks for changes.
        /// </summary>
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

        /// <summary>
        /// Montioring teamOne Score for changes.
        /// </summary>
        public int TeamOneScore
        {
            get => _teamOneScore;
            set
            {
                if (_teamOneScore != value)
                {
                    _teamOneScore = value;
                    OnPropertyChanged(nameof(TeamOneScore));
                }
            }
        }

        /// <summary>
        /// Montioring teamTwo Score for changes.
        /// </summary>
        public int TeamTwoScore
        {
            get => _teamTwoScore;
            set
            {
                if (_teamTwoScore != value)
                {
                    _teamTwoScore = value;
                    OnPropertyChanged(nameof(TeamTwoScore));
                }
            }
        }

        /// <summary>
        /// Montioring teamOn for changes.
        /// </summary>
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

        /// <summary>
        /// Montioring teamTwo for changes.
        /// </summary>
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

        /// <summary>
        /// Montioring the playArea (cards played) for changes.
        /// </summary>
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

        /// <summary>
        /// Montioring Player hand for changes.
        /// </summary>
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

        /// <summary>
        /// Montioring Player hand for changes.
        /// </summary>
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

        /// <summary>
        /// Montioring Player hand for changes.
        /// </summary>
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

        /// <summary>
        /// Montioring Player hand for changes.
        /// </summary>
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

        /// <summary>
        /// Montioring Kitty for changes.
        /// </summary>
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

        /// <summary>
        /// Montioring Player turn for changes.
        /// </summary>
        public Visibility Player1Turn
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

        /// <summary>
        /// Montioring Player turn for changes.
        /// </summary>
        public Visibility Player2Turn
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

        /// <summary>
        /// Montioring Player turn for changes.
        /// </summary>
        public Visibility Player3Turn
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

        /// <summary>
        /// Montioring Player turn for changes.
        /// </summary>
        public Visibility Player4Turn
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

        /// <summary>
        /// Montioring Player turn post kitty round for changes.
        /// </summary>
        public Visibility Player1PostKittyTurn
        {
            get { return _player1PostKittyTurn; }
            private set
            {
                if (_player1PostKittyTurn != value)
                {
                    _player1PostKittyTurn = value;
                    OnPropertyChanged(nameof(Player1PostKittyTurn));
                }
            }
        }

        /// <summary>
        /// Montioring Player ability to click card for changes.
        /// </summary>
        public bool Player1CanClickCard
        {
            get => _player1CanClickCard;
            set
            {
                if (_player1CanClickCard != value)
                {
                    _player1CanClickCard = value;
                    OnPropertyChanged(nameof(Player1CanClickCard));
                }
            }
        }

        /// <summary>
        /// Montioring Player ability to click card for changes.
        /// </summary>
        public bool Player2CanClickCard
        {
            get => _player2CanClickCard;
            set
            {
                if (_player2CanClickCard != value)
                {
                    _player2CanClickCard = value;
                    OnPropertyChanged(nameof(Player2CanClickCard));
                }
            }
        }

        /// <summary>
        /// Montioring Player ability to click card for changes.
        /// </summary>
        public bool Player3CanClickCard
        {
            get => _player3CanClickCard;
            set
            {
                if (_player3CanClickCard != value)
                {
                    _player3CanClickCard = value;
                    OnPropertyChanged(nameof(Player3CanClickCard));
                }
            }
        }

        /// <summary>
        /// Montioring Player ability to click card for changes.
        /// </summary>
        public bool Player4CanClickCard
        {
            get => _player4CanClickCard;
            set
            {
                if (_player4CanClickCard != value)
                {
                    _player4CanClickCard = value;
                    OnPropertyChanged(nameof(Player4CanClickCard));
                }
            }
        }

        /// <summary>
        /// Montioring list of non kitty suits for changes.
        /// </summary>
        public List<Card.Suits> NonKittySuits
        {
            get => _nonKittySuits;
            set
            {
                _nonKittySuits = value;
                OnPropertyChanged(nameof(NonKittySuits));
            }
        }

        /// <summary>
        /// Montioring Started for changes.
        /// </summary>
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

        /// <summary>
        /// Montioring HasTrumpSuit for changes.
        /// </summary>
        public Visibility HasTrumpSuit
        {
            get => _hasTrumpSuit;
            set
            {
                if (_hasTrumpSuit != value)
                {
                    _hasTrumpSuit = value;
                    OnPropertyChanged(nameof(HasTrumpSuit));
                }
            }
        }

        /// <summary>
        /// Montioring has lead suit for changes.
        /// </summary>
        public Visibility HasLeadSuit
        {
            get => _hasLeadSuit;
            set
            {
                if (_hasLeadSuit != value)
                {
                    _hasLeadSuit = value;
                    OnPropertyChanged(nameof(HasLeadSuit));
                }
            }
        }

        /// <summary>
        /// Montioring Trump suit for changes.
        /// </summary>
        public String TrumpSuit
        {
            get => _trumpSuit;
            set
            {
                if (_trumpSuit != value)
                {
                    _trumpSuit = "Current Trump suit: " + value;
                    OnPropertyChanged(nameof(TrumpSuit));
                }
            }
        }

        /// <summary>
        /// Montioring Lead suit for changes.
        /// </summary>
        public String LeadSuit
        {
            get => _leadSuit;
            set
            {
                if (_leadSuit != value)
                {
                    _leadSuit = "Current Lead suit: " + value;
                    OnPropertyChanged(nameof(LeadSuit));
                }
            }
        }

        // public bool Player2Turn => _game.CurrentPlayer == _game.PlayerTwo && _game.trumpFromKitty;

        /// <summary>
        /// The constructor for the GameViewModel.
        /// </summary>
        /// <param name="_playerStats"> Accepts plaer stats from the menuView and passes that to its own view. </param>
        public GameViewModel(Statistics _playerStats) // ***** added parameter on constructor
        {
            UpdateViewModelState();
            OrderUpCommand = new RelayCommand<object>(OrderUpExecute);
            PassCommand = new RelayCommand<object>(PassExecute);
            StartCommand = new RelayCommand<object>(StartExecute);
            ClickCardCommand = new RelayCommand<object>(ClickCardExecute);
            OrderUpPostKittyCommand = new RelayCommand<Card.Suits>(OrderUpPostKittyExecute);
            PassPostKittyCommand = new RelayCommand<object>(PassPostKittyExecute);
            // Works w/ continue (with player data), otherwise, *TODO* new game needs to create player data.
            if (_playerStats != null)
            {
                _GVMplayerStats = _playerStats; // assign to GameViewModel container for that player's stats object,
                MessageBox.Show("Player Name from JSON: " + _GVMplayerStats.PlayerName);
                PlayerName = _GVMplayerStats.PlayerName;
                _GVMplayerStats.TotalGames++;
                SaveStatsCommand = new SaveStatsCommand(_GVMplayerStats);
            }
            else
            {
                MessageBox.Show("Player has selected new game... Need to create profile."); //temporary
            }
        }

        private void UpdateViewModelState()
        {
            switch (_game.CurrentState)
            {
                case GameState.Initialize:
                    // 
                    break;
                case GameState.Start:
                    Start();
                    break;
                case GameState.TrumpSelectionFromKitty:
                    TrumpSelectionFromKitty();
                    break;
                case GameState.Play:
                    Play();
                    break;
                case GameState.EndOfGame:
                    //EndOfGame();
                    break;
                case GameState.TrumpSelectionPostKitty:
                    TrumpSelectionPostKitty();
                    break;
                case GameState.DealerKittySwap:
                    DealerKittySwap();
                    break;
            }
        }

        private void OrderUpExecute(object parameter)
        {
            _game.OrderUpFromKitty();
            UpdateViewModelState();
            _game.ChangeState(GameState.DealerKittySwap);
            UpdateViewModelState();
        }

        private void PassExecute(object parameter)
        {
            _game.PassFromKitty();
            UpdateViewModelState();
        }

        private void StartExecute(object parameter)
        {
            _game.Start();
            UpdateViewModelState();
            _game.TrumpSelectionFromKitty();
            UpdateViewModelState();
            MessageBox.Show("The Dealer is " + _game.TurnList[3].PlayerName + " and the current top kitty suit is " 
                + _game.Kitty[0].Suit + ".");
        }

        private void OrderUpPostKittyExecute(Card.Suits trumpSuit)
        {
            _game.OrderUpPostKitty(trumpSuit);
            UpdateViewModelState();
            MessageBox.Show("Trump suit is " + _game.TrumpSuit);
        }

        private void PassPostKittyExecute(object parameter)
        {
            _game.PassPostKitty();
            UpdateViewModelState();
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
        private void ClickCardExecute(object parameter)
        {
            if (_game.CurrentState == Game.GameState.DealerKittySwap)
            {
                if (parameter is CardViewModel cardViewModel)
                {
                    SwapWithKitty(cardViewModel);
                    UpdateViewModelState();
                    MessageBox.Show("Trump suit is " + _game.TrumpSuit);
                }
            }
            else if (_game.CurrentState == Game.GameState.Play)
            {
                if (parameter is CardViewModel cardViewModel)
                {
                    PlayCard(cardViewModel);
                    UpdateViewModelState();
                }
            }
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
            Player1Turn = _game.CurrentPlayer == _game.PlayerOne ? Visibility.Visible : Visibility.Collapsed;
            Player2Turn = _game.CurrentPlayer == _game.PlayerTwo ? Visibility.Visible : Visibility.Collapsed;
            Player3Turn = _game.CurrentPlayer == _game.PlayerThree ? Visibility.Visible : Visibility.Collapsed;
            Player4Turn = _game.CurrentPlayer == _game.PlayerFour ? Visibility.Visible : Visibility.Collapsed;
        }

        private void TrumpSelectionPostKitty()
        {
            NonKittySuits = _game.NonKittySuits;
            Player1PostKittyTurn = Visibility.Visible;
            Player1Turn = Visibility.Collapsed;
            Player2Turn = Visibility.Collapsed;
            Player3Turn = Visibility.Collapsed;
            Player4Turn = Visibility.Collapsed;
            Kitty = new KittyViewModel(_game.Kitty);
        }

        private void DealerKittySwap()
        {
            Player1Turn = Visibility.Collapsed;
            Player2Turn = Visibility.Collapsed;
            Player3Turn = Visibility.Collapsed;
            Player4Turn = Visibility.Collapsed;
            Player1CanClickCard = _game.CurrentPlayer == _game.PlayerOne;
            Player2CanClickCard = _game.CurrentPlayer == _game.PlayerTwo;
            Player3CanClickCard = _game.CurrentPlayer == _game.PlayerThree;
            Player4CanClickCard = _game.CurrentPlayer == _game.PlayerFour;
        }

        private void Play()
        {
            Player1Turn = Visibility.Collapsed;
            Player2Turn = Visibility.Collapsed;
            Player3Turn = Visibility.Collapsed;
            Player4Turn = Visibility.Collapsed;
            HasTrumpSuit = Visibility.Visible;
            if (_game.TurnsTaken == 0)
            {
                HasLeadSuit = Visibility.Visible;
            }
            TrumpSuit = _game.TrumpSuit.ToString();
            LeadSuit = _game.LeadSuit.ToString();
            Player1PostKittyTurn = Visibility.Collapsed;
            Player1CanClickCard = _game.CurrentPlayer == _game.PlayerOne;
            Player2CanClickCard = _game.CurrentPlayer == _game.PlayerTwo;
            Player3CanClickCard = _game.CurrentPlayer == _game.PlayerThree;
            Player4CanClickCard = _game.CurrentPlayer == _game.PlayerFour;
            Player1Hand = new HandViewModel(_game.PlayerOne.PlayerHand);
            Player2Hand = new HandViewModel(_game.PlayerTwo.PlayerHand);
            Player3Hand = new HandViewModel(_game.PlayerThree.PlayerHand);
            Player4Hand = new HandViewModel(_game.PlayerFour.PlayerHand);
            PlayArea = new PlayAreaViewModel(_game.PlayArea);
            Kitty = new KittyViewModel(_game.Kitty);
        }

        private void PlayCard(CardViewModel cardViewModel)
        {
            _game.PlayCard(_game.CurrentPlayer, cardViewModel.Card);
            RefreshUI();

            _game.CheckTrickWinner();
            RefreshUI();
        }

        private void SwapWithKitty(CardViewModel cardViewModel)
        {
            _game.SwapWithKitty(_game.CurrentPlayer, cardViewModel.Card);
            RefreshUI();
        }

        private void RefreshUI()
        {
            TeamOne = _game.Team1;
            TeamTwo = _game.Team2;
            TeamOneTricks = _game.TeamOneTricks;
            TeamTwoTricks = _game.TeamTwoTricks;
            TeamOneScore = _game.TeamOneScore;
            TeamTwoScore = _game.TeamTwoScore;

            Kitty = new KittyViewModel(_game.Kitty);
            PlayArea = new PlayAreaViewModel(_game.PlayArea);
            Player1Hand = new HandViewModel(_game.PlayerOne.PlayerHand);
            Player2Hand = new HandViewModel(_game.PlayerTwo.PlayerHand);
            Player3Hand = new HandViewModel(_game.PlayerThree.PlayerHand);
            Player4Hand = new HandViewModel(_game.PlayerFour.PlayerHand);
        }

        // private void EndOfGame()
        // {
        //     MessageBox.Show("End of game.");
        // }
    }
}
