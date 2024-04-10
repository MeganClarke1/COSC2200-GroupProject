﻿/// <file>
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
using Group2_COSC2200_Project.stores;
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

        #region ICommands
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
        /// Command to execute when save stats button is clicked.
        /// </summary>
        public ICommand SaveStatsCommand { get; private set; }
        /// <summary>
        /// Command to execute when a game ends and the user would like to return to the menu.
        /// </summary>
        public ICommand ReturnCommand { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public ICommand ChangeThemeCommand { get; private set; }

        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        private string _backgroundImagePath = "../assets/images/classic/playing_surface.png";

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

        /// <summary>
        /// Setting the visibility for Turns post initial kitty round to also be unseen.
        /// </summary>
        private Visibility _player1PostKittyTurn = Visibility.Collapsed;

        /// <summary>
        /// Tracking boolean status for is players can click their cards based on if it's their turn.
        /// </summary>
        private bool _player1CanClickCard;

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
        /// 
        /// </summary>
        private Visibility _ended = Visibility.Collapsed;

        /// <summary>
        /// Hiding these properties to create invisible tracks not seen by the user.
        /// </summary>
        private Visibility _hasTrumpSuit = Visibility.Collapsed;
        private Visibility _hasLeadSuit = Visibility.Collapsed;
        private Visibility _hasDealer = Visibility.Collapsed;

        /// <summary>
        /// Intializing empty Strings to store the trump suit and lead suit for comparison during play.
        /// </summary>
        private String _trumpSuit = "";
        private String _leadSuit = "";
        private String _dealer = "";

        /// <summary>
        /// A container for storage of the player's statistics as passed to this view by the MenuView.
        /// </summary>
        private Statistics _GVMplayerStats;

        /// <summary>
        /// A public property for setting via the player's statistics to dyanmically update player one's name on the view.
        /// </summary>
        public String _playerName;
        #endregion

        #region Monitored Properties
        /// <summary>
        /// 
        /// </summary>
        public string BackgroundImagePath
        {
            get => _backgroundImagePath;
            set
            {
                _backgroundImagePath = value;
                OnPropertyChanged(nameof(BackgroundImagePath));
            }
        }

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
        /// 
        /// </summary>
        public Visibility Ended
        {
            get => _ended;
            set
            {
                if (_ended != value)
                {
                    _ended = value;
                    OnPropertyChanged(nameof(Ended));
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
        /// 
        /// </summary>
        public Visibility HasDealer
        {
            get => _hasDealer;
            set
            {
                if (_hasDealer != value)
                {
                    _hasDealer = value;
                    OnPropertyChanged(nameof(HasDealer));
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

        /// <summary>
        /// Monitoring the dealer property for changes.
        /// </summary>
        public String Dealer
        {
            get => _dealer;
            set
            {
                if (_dealer != value)
                {
                    _dealer = "Current Dealer: " + value;
                    OnPropertyChanged(nameof(Dealer));
                }
            }
        }

        #endregion

        /// <summary>
        /// The constructor for the GameViewModel.
        ///     Thhis constructor serves to instantitate all needed data/commands for beginning a game of Euchre.
        ///     This includes button clicks, Updating the ViewModel, and opening a log file.
        /// </summary>
        /// <param name="_playerStats"> Accepts plaer stats from the menuView and passes that to its own view. </param>
        public GameViewModel(Statistics _playerStats, NavigationStore _navigationStore) // ***** added parameter on constructor
        {
            //logWriter = new StreamWriter("../../game_log.txt", true); // Open the log file for appending
            UpdateViewModelState();
            OrderUpCommand = new RelayCommand<object>(OrderUpExecute);
            PassCommand = new RelayCommand<object>(PassExecute);
            StartCommand = new RelayCommand<object>(StartExecute);
            ClickCardCommand = new RelayCommand<object>(ClickCardExecute);
            OrderUpPostKittyCommand = new RelayCommand<Card.Suits>(OrderUpPostKittyExecute);
            PassPostKittyCommand = new RelayCommand<object>(PassPostKittyExecute);
            ChangeThemeCommand = new RelayCommand<string>(ChangeThemeExecute);

            _game.OnAction += OnActionHandler;

            // Works w/ continue (with player data), otherwise, new game needs to create player data.
            if (_playerStats != null)
            {
                _GVMplayerStats = _playerStats; // assign to GameViewModel container for that player's stats object,
                _game.PlayerOneStats = _GVMplayerStats; // Store the GVM player stats in GameView
                PlayerName = _GVMplayerStats.PlayerName;
                SaveStatsCommand = new SaveStatsCommand(_game.PlayerOneStats);
                ReturnCommand = new ReturnPostGameCommand(_navigationStore, _game.PlayerOneStats);
            }
        }

        /// <summary>
        /// A switch to handle the changing states of the game... Will call the corresponding State function in Game class.
        /// </summary>
        private void UpdateViewModelState()
        {
            switch (_game.CurrentState)
            {
                case GameState.Start:
                    Start();
                    break;
                case GameState.TrumpSelectionFromKitty:
                    TrumpSelectionFromKitty();
                    break;
                case GameState.Play:
                    Play();
                    break;
                case GameState.TrumpSelectionPostKitty:
                    TrumpSelectionPostKitty();
                    break;
                case GameState.DealerKittySwap:
                    DealerKittySwap();
                    break;
                case GameState.EndOfGame:
                    EndOfGame();
                    break;
            }
        }

        #region Command Executions

        /// <summary>
        /// Event handler for actions triggered during the game.
        /// </summary>
        private void OnActionHandler(object sender, EventArgs e)
        {
            UpdateViewModelState();
        }

        private void ChangeThemeExecute(string theme)
        {
            CardViewModel.BaseImagePath = $"../assets/images/{theme}";
            RefreshCards();
            BackgroundImagePath = $"../assets/images/{theme}/playing_surface.png";
        }

        private void OrderUpExecute(object parameter)
        {
            _game.OrderUpFromKitty();
            UpdateViewModelState();
            _game.ChangeState(GameState.DealerKittySwap);
            UpdateViewModelState();
        }


        /// <summary>
        /// Executes the command to pass from the kitty.
        /// </summary>
        /// <param name="parameter">The parameter passed to the command.</param>
        private void PassExecute(object parameter)
        {
            _game.PassFromKitty();
            UpdateViewModelState();
        }

        /// <summary>
        /// Executes the command to start the game.
        /// </summary>
        /// <param name="parameter">The parameter passed to the command.</param>
        private void StartExecute(object parameter)
        {
            _game.Start();
            UpdateViewModelState();
            _game.TrumpSelectionFromKitty();
            UpdateViewModelState();
        }

        /// <summary>
        /// Executes the command to order up after viewing the kitty.
        /// </summary>
        /// <param name="trumpSuit">The trump suit selected.</param>
        private void OrderUpPostKittyExecute(Card.Suits trumpSuit)
        {
            _game.OrderUpPostKitty(trumpSuit);
            UpdateViewModelState();
            _game.ChangeState(GameState.Play);
            UpdateViewModelState();
        }

        /// <summary>
        /// Executes the command to pass after viewing the kitty.
        /// </summary>
        /// <param name="parameter">The parameter passed to the command.</param>
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
            if (_game.CurrentState == GameState.DealerKittySwap)
            {
                if (parameter is CardViewModel cardViewModel)
                {
                    SwapWithKitty(cardViewModel);
                    UpdateViewModelState();
                }
            }
            else if (_game.CurrentState == GameState.Play)
            {
                if (parameter is CardViewModel cardViewModel)
                {
                    PlayCard(cardViewModel);
                    UpdateViewModelState();
                }
            }
        }
        #endregion
        #region methods

        /// <summary>
        /// Represents the start of the game, called by our state switch
        /// </summary>
        private void Start()
        {
            // log start time
            Logging.LogStartGame(DateTime.Now); 
            Started = Visibility.Collapsed;
            HasDealer = Visibility.Visible;
            Player1Hand = new HandViewModel(_game.PlayerOne.PlayerHand, true);
            Player2Hand = new HandViewModel(_game.PlayerTwo.PlayerHand, false);
            Player3Hand = new HandViewModel(_game.PlayerThree.PlayerHand, false);
            Player4Hand = new HandViewModel(_game.PlayerFour.PlayerHand, false);

            // Log the player hands dealt
            Logging.LogPlayerHandsDealt(_game.PlayerOne.PlayerHand, _game.PlayerTwo.PlayerHand,
                                         _game.PlayerThree.PlayerHand, _game.PlayerFour.PlayerHand);

            Kitty = new KittyViewModel(_game.Kitty);
            PlayArea = new PlayAreaViewModel(_game.PlayArea);
            TeamOne = new Team(_game.Team1.TeamId, _game.Team1.TeamPlayers);
            TeamTwo = new Team(_game.Team2.TeamId, _game.Team2.TeamPlayers);
            Scoreboard = new Scoreboard();
            Dealer = _game.TurnList.FirstOrDefault(player => player.IsDealer)?.PlayerName ?? "No Dealer Found";
        }

        /// <summary>
        /// Represents the TrumpSelectionFromKitty phase of the game. Called from our state switch
        ///     handles dyanmic turn based rounds.
        /// </summary>
        private void TrumpSelectionFromKitty()
        {
            Player1Turn = _game.CurrentPlayer == _game.PlayerOne ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Represents the trump suit selection after the inital kitty voting round.
        /// </summary>
        private void TrumpSelectionPostKitty()
        {
            NonKittySuits = _game.NonKittySuits;
            Player1PostKittyTurn = Visibility.Visible;
            Player1Turn = Visibility.Collapsed;
            Kitty = new KittyViewModel(_game.Kitty);
        }

        private void DealerKittySwap()
        {
            Player1Turn = Visibility.Collapsed;
            Player1CanClickCard = _game.CurrentPlayer == _game.PlayerOne;
        }

        /// <summary>
        /// Prepares the UI for the current player's turn and updates game elements accordingly.
        /// </summary>
        private void Play()
        {
            Player1Turn = Visibility.Collapsed;
            HasTrumpSuit = Visibility.Visible;
            if (_game.TurnsTaken == 0)
            {
                HasLeadSuit = Visibility.Visible;
            }
            TrumpSuit = _game.TrumpSuit.ToString();
            LeadSuit = _game.LeadSuit.ToString();
            Dealer = _game.TurnList.FirstOrDefault(player => player.IsDealer)?.PlayerName ?? "No Dealer Found";
            Player1PostKittyTurn = Visibility.Collapsed;
            Player1CanClickCard = _game.CurrentPlayer == _game.PlayerOne;
            Player1Hand = new HandViewModel(_game.PlayerOne.PlayerHand, true);
            Player2Hand = new HandViewModel(_game.PlayerTwo.PlayerHand, false);
            Player3Hand = new HandViewModel(_game.PlayerThree.PlayerHand, false);
            Player4Hand = new HandViewModel(_game.PlayerFour.PlayerHand, false);
            PlayArea = new PlayAreaViewModel(_game.PlayArea);
            Kitty = new KittyViewModel(_game.Kitty);
        }

        private void EndOfGame()
        {
            Ended = Visibility.Visible;
        }

        private void PlayCard(CardViewModel cardViewModel)
        {
            _game.PlayCard(_game.CurrentPlayer, cardViewModel.Card);

            // Log the human card played
            //Logging.LogPlayedCard(_game.CurrentPlayer, cardViewModel.Card);

            RefreshUI();
        }

        private void SwapWithKitty(CardViewModel cardViewModel)
        {
            _game.SwapWithKitty(_game.CurrentPlayer, cardViewModel.Card);
            RefreshUI();
        }

        /// <summary>
        /// Refreshes the user interface to reflect the current state of the game.
        /// </summary>
        private void RefreshUI()
        {
            TeamOne = _game.Team1;
            TeamTwo = _game.Team2;
            TeamOneTricks = _game.TeamOneTricks;
            TeamTwoTricks = _game.TeamTwoTricks;
            TeamOneScore = _game.TeamOneScore;
            TeamTwoScore = _game.TeamTwoScore;

            TrumpSuit = _game.TrumpSuit.ToString();
            LeadSuit = _game.LeadSuit.ToString();
            Dealer = _game.TurnList.FirstOrDefault(player => player.IsDealer)?.PlayerName ?? "No Dealer Found";

            Kitty = new KittyViewModel(_game.Kitty);
            PlayArea = new PlayAreaViewModel(_game.PlayArea);
            Player1Hand = new HandViewModel(_game.PlayerOne.PlayerHand, true);
            Player2Hand = new HandViewModel(_game.PlayerTwo.PlayerHand, false);
            Player3Hand = new HandViewModel(_game.PlayerThree.PlayerHand, false);
            Player4Hand = new HandViewModel(_game.PlayerFour.PlayerHand, false);
        }

        private void RefreshCards()
        {
            if (Player1Hand?.Cards != null)
            {
                foreach (CardViewModel cardViewModel in Player1Hand.Cards)
                {
                    cardViewModel.RefreshImagePath();
                }
            }
            if (Player2Hand?.Cards != null)
            {
                foreach (CardViewModel cardViewModel in Player2Hand.Cards)
                {
                    cardViewModel.RefreshImagePath();
                }
            }
            if (Player3Hand?.Cards != null)
            {
                foreach (CardViewModel cardViewModel in Player3Hand.Cards)
                {
                    cardViewModel.RefreshImagePath();
                }
            }
            if (Player4Hand?.Cards != null)
            {
                foreach (CardViewModel cardViewModel in Player4Hand.Cards)
                {
                    cardViewModel.RefreshImagePath();
                }
            }
            if (PlayArea?.PlayedCards != null)
            {
                foreach (CardViewModel cardViewModel in PlayArea.PlayedCards)
                {
                    cardViewModel.RefreshImagePath();
                }
            }
            if (Kitty?.KittyCard != null)
            {
                foreach (CardViewModel cardViewModel in Kitty.KittyCard)
                {
                    cardViewModel.RefreshImagePath();
                }
            }
        }
        #endregion
    }
}
