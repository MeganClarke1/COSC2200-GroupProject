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
        }

        // Defining our iCommand Method calls
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
        }

        private bool CanPassExecute(object parameter)
        {
            // Add any condition here that determines whether the button can be clicked
            return true; // Change this condition as per your requirement
        }
    }

}
