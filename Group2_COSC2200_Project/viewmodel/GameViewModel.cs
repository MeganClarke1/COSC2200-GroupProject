using GalaSoft.MvvmLight.Command;
using Group2_COSC2200_Project.model;
using System.Collections.ObjectModel;
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

        private HandViewModel _player1Hand;
        private HandViewModel _player2Hand;
        private HandViewModel _player3Hand;
        private HandViewModel _player4Hand;
        private KittyViewModel _kitty;
        private Deck _deck;
        private bool _player1Turn;
        private bool _player2Turn;
        private bool _player3Turn;
        private bool _player4Turn;
        private Visibility _started = Visibility.Visible;

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
            OrderUpCommand = new RelayCommand<object>(OrderUpExecute, CanOrderUpExecute);
            PassCommand = new RelayCommand<object>(PassExecute, CanPassExecute);
            StartCommand = new RelayCommand<object>(StartExecute, CanStartExecute);
        }

        private void OrderUpExecute(object parameter)
        {
            _game.OrderUp();
            UpdatePlayerTurn();
        }

        private bool CanOrderUpExecute(object parameter)
        {
            return true;
        }

        private void PassExecute(object parameter)
        {
            _game.Pass();
            UpdatePlayerTurn();
        }

        private bool CanPassExecute(object parameter)
        {
            return true;
        }

        private void StartExecute(object parameter)
        {
            _game.Start();
            Started = Visibility.Collapsed;
            UpdatePlayerTurn();
            Player1Hand = new HandViewModel(_game.PlayerOne.PlayerHand);
            Player2Hand = new HandViewModel(_game.PlayerTwo.PlayerHand);
            Player3Hand = new HandViewModel(_game.PlayerThree.PlayerHand);
            Player4Hand = new HandViewModel(_game.PlayerFour.PlayerHand);
            Kitty = new KittyViewModel(_game.Kitty);
        }

        private bool CanStartExecute(object parameter)
        {
            return true;
        }

        private void UpdatePlayerTurn()
        {
            Player1Turn = _game.CurrentPlayer == _game.PlayerOne && _game.trumpFromKitty;
            Player2Turn = _game.CurrentPlayer == _game.PlayerTwo && _game.trumpFromKitty;
            Player3Turn = _game.CurrentPlayer == _game.PlayerThree && _game.trumpFromKitty;
            Player4Turn = _game.CurrentPlayer == _game.PlayerFour && _game.trumpFromKitty;
        }
    }

}
