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
        public ICommand OrderUpPostKittyCommand {  get; private set; }
        public ICommand PassPostKittyCommand { get; private set; }
        public ICommand StartCommand { get; private set; }
        public ICommand ClickCardCommand { get; private set; }

        private HandViewModel _player1Hand;
        private HandViewModel _player2Hand;
        private HandViewModel _player3Hand;
        private HandViewModel _player4Hand;
        private KittyViewModel _kitty;
        private PlayAreaViewModel _playArea;

        private Visibility _player1Turn = Visibility.Collapsed;
        private Visibility _player2Turn = Visibility.Collapsed;
        private Visibility _player3Turn = Visibility.Collapsed;
        private Visibility _player4Turn = Visibility.Collapsed;

        private Visibility _player1PostKittyTurn = Visibility.Collapsed;

        private bool _player1CanClickCard;
        private bool _player2CanClickCard;
        private bool _player3CanClickCard;
        private bool _player4CanClickCard;

        private List<Card.Suits> _nonKittySuits;

        private Team _teamOne;
        private Team _teamTwo;
        private int _teamOneTricks;
        private int _teamTwoTricks;
        private int _teamOneScore;
        private int _teamTwoScore;
        private Scoreboard _scoreBoard;

        private Visibility _started = Visibility.Visible;

        private Visibility _hasTrumpSuit = Visibility.Collapsed;
        private String _trumpSuit = "";

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

        public List<Card.Suits> NonKittySuits
        {
            get => _nonKittySuits;
            set
            {
                _nonKittySuits = value;
                OnPropertyChanged(nameof(NonKittySuits));
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

        // public bool Player2Turn => _game.CurrentPlayer == _game.PlayerTwo && _game.trumpFromKitty;

        public GameViewModel()
        {   
            UpdateViewModelState();
            OrderUpCommand = new RelayCommand<object>(OrderUpExecute);
            PassCommand = new RelayCommand<object>(PassExecute);
            StartCommand = new RelayCommand<object>(StartExecute);
            ClickCardCommand = new RelayCommand<object>(ClickCardExecute);
            OrderUpPostKittyCommand = new RelayCommand<Card.Suits>(OrderUpPostKittyExecute);
            PassPostKittyCommand = new RelayCommand<object>(PassPostKittyExecute);
        }

        private void UpdateViewModelState()
        {
            RefreshUI();
            switch (_game.CurrentState)
            {
                case Game.GameState.Initialize:
                    // 
                    break;
                case Game.GameState.Start:
                    Start();
                    break;
                case Game.GameState.TrumpSelectionFromKitty:
                    TrumpSelectionFromKitty();
                    break;
                case Game.GameState.Play:
                    Play();
                    break;
                case Game.GameState.EndOfGame:
                    //EndOfGame();
                    break;
                case Game.GameState.TrumpSelectionPostKitty:
                    TrumpSelectionPostKitty();
                    break;
                case Game.GameState.DealerKittySwap:
                    DealerKittySwap();
                    break;
            }
        }

        private void OrderUpExecute(object parameter)
        {
            _game.OrderUpFromKitty();
            UpdateViewModelState();
            MessageBox.Show(_game.CurrentPlayer.PlayerName + " gets to swap one of their cards with the kitty.");
        }

        private void PassExecute(object parameter)
        {
            _game.PassFromKitty();
            UpdateViewModelState();
            MessageBox.Show("Your Turn: " + _game.CurrentPlayer.PlayerName);
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
            MessageBox.Show("Your Turn: " + _game.CurrentPlayer.PlayerName);
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
            HasTrumpSuit = Visibility.Visible;
            TrumpSuit = _game.TrumpSuit.ToString();
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

        public void RefreshUI()
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
