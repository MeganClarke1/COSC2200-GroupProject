using Group2_COSC2200_Project.model;
using System.Collections.ObjectModel;

namespace Group2_COSC2200_Project.viewmodel
{
    public class KittyViewModel : ViewModelBase
    {
        public ObservableCollection<CardViewModel> KittyCard { get; }

        public KittyViewModel(List<Card> kitty)
        {
            KittyCard = new ObservableCollection<CardViewModel>();

            foreach (Card card in kitty)
            {
                CardViewModel cardViewModel = new CardViewModel(card);
                KittyCard.Add(cardViewModel);
            }
        }
    }
}
