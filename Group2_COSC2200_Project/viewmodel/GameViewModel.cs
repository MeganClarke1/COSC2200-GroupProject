using Group2_COSC2200_Project.model;
using System.Collections.ObjectModel;

namespace Group2_COSC2200_Project.viewmodel
{
    public class GameViewModel : ViewModelBase
    {
        public ObservableCollection<CardViewModel> Hand { get; private set; }

        public GameViewModel() 
        {
            //Hand = new ObservableCollection<CardViewModel>
            //{
            //    new CardViewModel(new Card(Card.Suits.Hearts, Card.Ranks.Ace)),
            //    new CardViewModel(new Card(Card.Suits.Clubs, Card.Ranks.Ace)),
            //    new CardViewModel(new Card(Card.Suits.Diamonds, Card.Ranks.Ace)),
            //    new CardViewModel(new Card(Card.Suits.Spades, Card.Ranks.Ace)),
            //    new CardViewModel(new Card(Card.Suits.Spades, Card.Ranks.King))
            //};
        }
    }
}
