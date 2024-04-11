/// <file>
///   <summary>
///     File Name: KittyViewModel.cs
///   </summary>
///   <author>
///     Authors: Brody Dentinger, Megan Clarke, Colin Eade, Muhammad Yasir Patel
///   </author>
///   <created>
///     Created: Apr 2, 2024
///   </created>
///   <lastModified>
///     Last Modified: April 9, 2024
///   </lastModified>
///   <description>
///     Description: This class represents the KittyViewModel, which is the 1 card flipped face up AKA the kitty.
///   </description>
/// </file>

using Group2_COSC2200_Project.model;
using System.Collections.ObjectModel;

namespace Group2_COSC2200_Project.viewmodel
{
    /// <summary>
    /// Represents the ViewModel for the kitty card in the game. This class uses an observable collection to 
    /// dynamically update the UI as the kitty's composition is revealed or changes.
    /// </summary>
    public class KittyViewModel : ViewModelBase
    {
        /// <summary>
        /// Holds the CardViewModel that represents the kitty. This enables dynamic updates of kitty card within the UI.
        /// </summary>
        public ObservableCollection<CardViewModel> KittyCard { get; }


        /// <summary>
        /// Initializes a new instance of the KittyViewModel class, converting the card in the kitty into a 
        /// CardViewModel for UI rendering. 
        /// </summary>
        /// <param name="kitty">A list of Card models representing the cards currently in the kitty.</param>
        public KittyViewModel(List<Card> kitty)
        {
            KittyCard = new ObservableCollection<CardViewModel>();

            // For each card in the list of card objects (the kitty), create a new CardViewModel, and add it to the list.
            foreach (Card card in kitty)
            {
                CardViewModel cardViewModel = new CardViewModel(card)
                {
                    IsInKitty = true
                };
                KittyCard.Add(cardViewModel);
            }
        }
    }
}
