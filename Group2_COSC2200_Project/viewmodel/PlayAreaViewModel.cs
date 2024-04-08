using Group2_COSC2200_Project.model;
using System.Collections.ObjectModel;

namespace Group2_COSC2200_Project.viewmodel
{
    public class PlayAreaViewModel : ViewModelBase
    {
        public ObservableCollection<CardViewModel> PlayedCards { get; }

        public PlayAreaViewModel(List<Card> playedCardsVMP)
        {
            PlayedCards = new ObservableCollection<CardViewModel>();

            foreach (Card card in playedCardsVMP)
            {
                CardViewModel cardViewModel = new CardViewModel(card);
                PlayedCards.Add(cardViewModel);
            }
        }
    }
}