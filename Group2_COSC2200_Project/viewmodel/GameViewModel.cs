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
        #region Game Object
        /// <summary>
        /// Constructs a new game object automatically.
        /// </summary>
        private Game _game = new();
        #endregion

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
        /// Command to order a trump suit up after the initial kitty round.
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
        /// Command to change the theme of the game.
        /// </summary>
        public ICommand ChangeThemeCommand { get; private set; }
        #endregion

        #region Properties
        /// <summary>
        /// Represents the background image path for the game's UI, dynamically set based on the current theme.
        /// </summary>
        private string _backgroundImagePath = $"../assets/images/{Theme.GetTheme()}/playing_surface.png";

        /// <summary>
        /// ViewModel instances for each player's hand, facilitating the binding and interaction within the UI.
        /// </summary>
        private HandViewModel _player1Hand, _player2Hand, _player3Hand, _player4Hand;

        /// <summary>
        /// ViewModel for the kitty, managing its representation and interactions within the game's UI.
        /// </summary>
        private KittyViewModel _kitty;

        /// <summary>
        /// ViewModel for the play area, where cards played during a trick are displayed.
        /// </summary>
        private PlayAreaViewModel _playArea;

        /// <summary>
        /// Visibility indicators for the UI elements related to the user's turn.
        /// </summary>
        private Visibility _player1Turn = Visibility.Collapsed;

        /// <summary>
        /// Visibility indicators for the UI elements related to the user's turn.
        /// </summary>
        private Visibility _player1PostKittyTurn = Visibility.Collapsed;

        /// <summary>
        /// Indicates whether player one can interact with their cards, typically set according to whether it is their turn.
        /// </summary>
        private bool _player1CanClickCard;

        /// <summary>
        /// A collection of card suits available for selection as the trump suit.
        /// </summary>
        private List<Card.Suits> _nonKittySuits;

        /// <summary>
        /// Instances of Team representing the two teams competing in the game.
        /// </summary>
        private Team _teamOne, _teamTwo;

        /// <summary>
        /// Counters for the number of tricks won by each team.
        /// </summary>
        private int _teamOneTricks, _teamTwoTricks;

        /// <summary>
        /// The current score for each team, updated throughout the game based on the outcomes of each trick and round.
        /// </summary>
        private int _teamOneScore, _teamTwoScore;

        /// <summary>
        /// Indicates the initial visibility of the Deal button, set to be visible at the beginning.
        /// </summary>
        private Visibility _started = Visibility.Visible;

        /// <summary>
        /// Controls the visibility of the "Back to menu" button, initially hidden until the game ends.
        /// </summary>
        private Visibility _ended = Visibility.Collapsed;

        /// <summary>
        /// Determines the visibility of UI elements indicating the presence of a trump suit, initially hidden.
        /// </summary>
        private Visibility _hasTrumpSuit = Visibility.Collapsed;

        /// <summary>
        /// Controls the visibility of indicators for the lead suit in play, initially set to be hidden.
        /// </summary>
        private Visibility _hasLeadSuit = Visibility.Collapsed;

        /// <summary>
        /// Manages the display of the dealer's status within the UI, initially hidden.
        /// </summary>
        private Visibility _hasDealer = Visibility.Collapsed;

        /// <summary>
        /// Stores the name of the currently selected trump suit, initially empty until a suit is chosen.
        /// </summary>
        private String _trumpSuit = "";

        /// <summary>
        /// Holds the name of the lead suit for the current round of play, initially empty.
        /// </summary>
        private String _leadSuit = "";

        /// <summary>
        /// Contains the name or identifier of the current dealer in the game, starting with an empty value.
        /// </summary>
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
        /// Monitoring BackgroundImagePath for changes
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
        /// Montioring teamOne for changes.
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
        /// Montioring TeamTwo for changes.
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
        /// Montioring TeamOne tricks for changes.
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
        /// Monitoring Ended for changes.
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
        /// Monitoring HasDealer For changes.
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

        #region Constructor
        /// <summary>
        /// The constructor for the GameViewModel.
        /// This constructor serves to instantitate all needed data/commands for beginning a game of Euchre.
        /// This includes button clicks, Updating the ViewModel, and opening a log file.
        /// </summary>
        /// <param name="_playerStats"> Accepts player stats from the menuView and passes that to its own view. </param>
        public GameViewModel(Statistics _playerStats, NavigationStore _navigationStore) // ***** added parameter on constructor
        {
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
        #endregion

        /// <summary>
        /// Updates the ViewModel state based on the current state of the game. This method acts as a central control 
        /// for transitioning between different phases of the game, such as starting the game, selecting the trump suit 
        /// from the kitty, playing cards, and concluding the game. Each game state triggers the corresponding method 
        /// to update the ViewModel and the UI to reflect the new state.
        /// </summary>
        private void UpdateViewModelState()
        {
            RefreshUI();
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
        /// Responds to game action events by updating the ViewModel state to reflect changes in the game. This method 
        /// ensures that the ViewModel remains in sync with the game's current state.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void OnActionHandler(object sender, EventArgs e)
        {
            UpdateViewModelState();
        }

        /// <summary>
        /// Changes the game's theme to the specified theme. 
        /// </summary>
        /// <param name="theme">The name of the theme to be applied to the game's visual elements.</param>
        private void ChangeThemeExecute(string theme)
        {
            Theme.SetTheme(theme);
            CardViewModel.BaseImagePath = $"../assets/images/{theme}";
            RefreshCards();
            BackgroundImagePath = $"../assets/images/{theme}/playing_surface.png";
        }

        /// <summary>
        /// Executes the user action to "order up" a suit as the trump suit during the kitty phase of the game.
        /// </summary>
        /// <param name="parameter">The parameter is not used but included to match the ICommand delegate signature.</param>
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
        /// Initializes the game's UI components for the start of gameplay. This includes hiding the start indicator, 
        /// showing the dealer, setting up the hand view models for all players, and logging the start of the game and 
        /// the initial hands dealt. It also initializes the kitty and play area view models, and updates the UI to 
        /// display the current teams and dealer.
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
            TeamOne = new Team(_game.Team1.TeamId, _game.Team1.TeamPlayers, _game.Team1.Name);
            TeamTwo = new Team(_game.Team2.TeamId, _game.Team2.TeamPlayers, _game.Team2.Name);
            Dealer = _game.TurnList.FirstOrDefault(player => player.IsDealer)?.PlayerName ?? "No Dealer Found";
        }

        /// <summary>
        /// Transitions the game to the TrumpSelectionFromKitty phase. This method updates the UI so that the user has
        /// "Pass" and "Order it up" buttons available to click on their turn.
        /// </summary>
        private void TrumpSelectionFromKitty()
        {
            HasTrumpSuit = Visibility.Collapsed;
            HasLeadSuit = Visibility.Collapsed;
            Player1Turn = _game.CurrentPlayer == _game.PlayerOne ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Manages the phase for selecting the trump suit after the initial kitty phase. This method updates the UI 
        /// so that the user has "Pass" and non-kitty suits available to choose from as buttons when it is their turn.
        /// </summary>
        private void TrumpSelectionPostKitty()
        {
            HasTrumpSuit = Visibility.Collapsed;
            HasLeadSuit = Visibility.Collapsed;
            NonKittySuits = _game.NonKittySuits;
            Player1PostKittyTurn = Visibility.Visible;
            Player1Turn = Visibility.Collapsed;
            Kitty = new KittyViewModel(_game.Kitty);
        }

        /// <summary>
        /// Manages the game phase for the dealer to swap a card with the kitty, adjusting UI elements to indicate 
        /// that it's the current player's turn to make the swap.
        /// </summary>
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
            if (_game.TurnsTaken >= 1)
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

        /// <summary>
        /// Marks the end of the game by setting the visibility of the "Return to menu" button as visible.
        /// </summary>
        private void EndOfGame()
        {
            Ended = Visibility.Visible;
        }

        /// <summary>
        /// Facilitates a player's action to play a card during their turn. This method updates the game state by 
        /// adding the selected card to the play area and then refreshes the UI to reflect the new state.
        /// </summary>
        /// <param name="cardViewModel">The cardViewModel representing the card selected by the player.</param>
        private void PlayCard(CardViewModel cardViewModel)
        {
            _game.PlayCard(_game.CurrentPlayer, cardViewModel.Card);
            RefreshUI();
        }

        /// <summary>
        /// Facilitates the user's action to swap a card from their hand with the top card from the kitty during the 
        /// dealer kitty swap phase. This method updates the game state to reflect the swap and then refreshes the UI.
        /// </summary>
        /// <param name="cardViewModel">The cardViewModel representing the card selected by the dealer for swapping.</param>
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
            if (_game.TurnsTaken >= 1)
            {
                HasLeadSuit = Visibility.Visible;
            }
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

        /// <summary>
        /// Updates the image paths for all cards currently displayed in the game's UI. This includes cards in each 
        /// player's hand, the play area, and the kitty. It's called after a theme change, which requires visual 
        /// updates to the cards. This method iterates through all card view models, triggering a refresh of their 
        /// image paths to reflect the current game state or theme.
        /// </summary>
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
