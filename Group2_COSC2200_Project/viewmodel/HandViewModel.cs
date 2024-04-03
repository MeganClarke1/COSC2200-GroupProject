using Group2_COSC2200_Project.model;
using System.Collections.ObjectModel;

namespace Group2_COSC2200_Project.viewmodel
{
    public class HandViewModel : ViewModelBase
    {
        public ObservableCollection<CardViewModel> Cards { get; }

        public HandViewModel(Hand hand)
        {
            Cards = new ObservableCollection<CardViewModel>();

            foreach (Card card in hand.Cards)
            {
                CardViewModel cardViewModel = new CardViewModel(card);
                Cards.Add(cardViewModel);
            }
        }
    }
}
