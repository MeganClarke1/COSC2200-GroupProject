﻿using GalaSoft.MvvmLight.Command;
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

        private Deck _deck;

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
        private int _teamTwoScore = 9;
        private Scoreboard _scoreBoard;
        private int _playedCardsCntr;
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

        // public bool Player2Turn => _game.CurrentPlayer == _game.PlayerTwo && _game.trumpFromKitty;

        public GameViewModel()
        {   
            UpdateViewModelState();
            OrderUpCommand = new RelayCommand<object>(OrderUpExecute, CanOrderUpExecute);
            PassCommand = new RelayCommand<object>(PassExecute, CanPassExecute);
            StartCommand = new RelayCommand<object>(StartExecute, CanStartExecute);
            ClickCardCommand = new RelayCommand<object>(ClickCardExecute, CanClickCardExecute);
            OrderUpPostKittyCommand = new RelayCommand<Card.Suits>(OrderUpPostKittyExecute, CanOrderUpPostKittyExecute);
            PassPostKittyCommand = new RelayCommand<object>(PassPostKittyExecute, CanPassPostKittyExecute);
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
                case Game.GameState.Round:
                    NewRound();   
                    break;
                case Game.GameState.EndOfGame:
                    EndOfGame();
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
        }

        private bool CanOrderUpExecute(object parameter)
        {
            return true;
        }

        private void PassExecute(object parameter)
        {
            _game.PassFromKitty();
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

        private void OrderUpPostKittyExecute(Card.Suits trumpSuit)
        {
            _game.OrderUpPostKitty(trumpSuit);
        }

        private bool CanOrderUpPostKittyExecute(Card.Suits trumpSuit)
        {
            return true;
        }

        private void PassPostKittyExecute(object parameter)
        {
            _game.PassPostKitty();
        }

        private bool CanPassPostKittyExecute(object parameter)
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
            Player1Turn = _game.CurrentPlayer == _game.PlayerOne ? Visibility.Visible : Visibility.Collapsed;
            Player2Turn = _game.CurrentPlayer == _game.PlayerTwo ? Visibility.Visible : Visibility.Collapsed;
            Player3Turn = _game.CurrentPlayer == _game.PlayerThree ? Visibility.Visible : Visibility.Collapsed;
            Player4Turn = _game.CurrentPlayer == _game.PlayerFour ? Visibility.Visible : Visibility.Collapsed;
        }

        private void TrumpSelectionPostKitty()
        {
            NonKittySuits = _game.NonKittySuits;
            Player1PostKittyTurn = _game.CurrentPlayer == _game.PlayerOne ? Visibility.Visible : Visibility.Collapsed;
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
                }
            } 
            else
            {
                PlayCard(parameter);
            }
        }

        private bool CanClickCardExecute(object parameter)
        {
            return true;
        }

            if (_game.PlayArea.Count < 4) {
                // This is fetching a cardViewModel when a card is clicked
                // Therefore, must fetch that cardViewModel's .Card property (which is the card object)
                if (parameter is CardViewModel cardViewModel)
                {
                    Card clickedCard = cardViewModel.Card;
                    _game.AddCardtoPlayAreaTest(clickedCard);
                    PlayArea = new PlayAreaViewModel(_game.PlayArea);

                    // Remove the card from the hand that the card belonged to
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
                    //UpdatePlayerTurn();
                }
            }
            // 4 cards have been played
            else
            {
                MessageBox.Show("Everyone has played a card. Time to see who wins!");
                Team winningTeam = Trick.DetermineTrickWinner(_game.PlayArea, _game.Team1, _game.Team2);
                MessageBox.Show("Winning Team: " + winningTeam.TeamId);

                // Increment the trick counter based on winning team
                if (winningTeam.TeamId == Team.TeamID.TeamOne) {
                    TeamOneTricks++;
                    //Scoreboard.IncrementTrickCount(TeamOne);   // **** When trying to do this same method from the Scoreboard obj, doesnt work .... ***
                }
                else if(winningTeam.TeamId == Team.TeamID.TeamTwo)
                {
                    TeamTwoTricks++;
                    //Scoreboard.IncrementTrickCount(TeamTwo);
                }
            }
        }

        private void SwapWithKitty(CardViewModel cardViewModel)
        {
            _game.SwapWithKitty(_game.CurrentPlayer, cardViewModel.Card);
            Kitty = new KittyViewModel(_game.Kitty);
            Player1Hand = new HandViewModel(_game.PlayerOne.PlayerHand);
            Player2Hand = new HandViewModel(_game.PlayerTwo.PlayerHand);
            Player3Hand = new HandViewModel(_game.PlayerThree.PlayerHand);
            Player4Hand = new HandViewModel(_game.PlayerFour.PlayerHand);
        }

        private void NewRound()
        {
            string RoundWinner = "";

            if(TeamOneTricks >= 3)
            {
                RoundWinner = Team.TeamID.TeamOne.ToString();

                /*if (TeamOne.MakerStatus == true)
                {
                    TeamOneScore++;
                }
                else
                {
                    TeamOneScore = TeamOneScore + 3;
                }*/

                TeamOneScore++;
            }
            else if(TeamTwoTricks >= 3)
            {
                RoundWinner = Team.TeamID.TeamTwo.ToString();

                /*if (TeamTwo.MakerStatus == true)
                {
                    TeamTwoScore++;
                }
                else
                {
                    TeamTwoScore = TeamTwoScore + 3;
                }*/

                TeamTwoScore++;
            }

            MessageBox.Show("Round Winner: " + RoundWinner + " Next Round will Begin when you click ok!");

            if (TeamOneScore >= 10 || TeamTwoScore >= 10)
            {
                _game.ChangeState(Game.GameState.EndOfGame);
                UpdateViewModelState();
            }

            Deck = _game.Deck;
            Player1Hand = new HandViewModel(_game.PlayerOne.PlayerHand);
            Player2Hand = new HandViewModel(_game.PlayerTwo.PlayerHand);
            Player3Hand = new HandViewModel(_game.PlayerThree.PlayerHand);
            Player4Hand = new HandViewModel(_game.PlayerFour.PlayerHand);
            Kitty = new KittyViewModel(_game.Kitty);

            TeamOneTricks = 0;
            TeamTwoTricks = 0;
        }

        private void EndOfGame()
        {
            MessageBox.Show("End of game.");
        }
    }
}
