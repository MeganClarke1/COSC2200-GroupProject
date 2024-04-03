using GalaSoft.MvvmLight.Command;
using Group2_COSC2200_Project.model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Group2_COSC2200_Project.viewmodel
{
    public class GameViewModel : ViewModelBase
    {
        private Game _game = new Game();

        public ICommand OrderUpCommand { get; private set; }
        public ICommand PassCommand { get; private set; }

        private HandViewModel _player1Hand;
        private HandViewModel _player2Hand;
        private HandViewModel _player3Hand;
        private HandViewModel _player4Hand;
        private bool _player1Turn;
        private bool _player2Turn;
        private bool _player3Turn;
        private bool _player4Turn;
        private Player _currentPlayer;
        private bool _trumpFromKitty;
        private KittyViewModel _kitty;

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

        public Player CurrentPlayer
        {
            get => _currentPlayer;
            set
            {
                if (_currentPlayer != value)
                {
                    _currentPlayer = value;
                    OnPropertyChanged(nameof(CurrentPlayer));
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
                    OnPropertyChanged(nameof(Player4Hand));
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

        // public bool Player2Turn => _game.CurrentPlayer == _game.PlayerTwo && _game.trumpFromKitty;

        public GameViewModel()
        {   
            _game.Initialize();
            _player1Hand = new HandViewModel(_game.PlayerOne.PlayerHand);
            _player2Hand = new HandViewModel(_game.PlayerTwo.PlayerHand);
            _player3Hand = new HandViewModel(_game.PlayerThree.PlayerHand);
            _player4Hand = new HandViewModel(_game.PlayerFour.PlayerHand);
            _kitty = new KittyViewModel(_game.Kitty);

            OrderUpCommand = new RelayCommand<object>(OrderUpExecute, CanOrderUpExecute);
            PassCommand = new RelayCommand<object>(PassExecute, CanPassExecute);

            UpdatePlayerTurn();
        }

        private void OrderUpExecute(object parameter)
        {
            _game.OrderUp();
        }

        private bool CanOrderUpExecute(object parameter)
        {
            // Add any condition here that determines whether the button can be clicked
            return true; // Change this condition as per your requirement
        }

        private void PassExecute(object parameter)
        {
            _game.Pass();
            UpdatePlayerTurn();
        }

        private bool CanPassExecute(object parameter)
        {
            // Add any condition here that determines whether the button can be clicked
            return true; // Change this condition as per your requirement
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
