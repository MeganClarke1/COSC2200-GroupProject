/// <file>
///   <summary>
///     File Name: HandViewModel.cs
///   </summary>
///   <author>
///     Authors: Brody Dentinger, Megan Clarke, Colin Eade, Muhammad Yasir Patel
///   </author>
///   <created>
///     Created: Apr 1, 2024
///   </created>
///   <lastModified>
///     Last Modified: April 9, 2024
///   </lastModified>
///   <description>
///     Description: This class represents the HandViewModel, which is the player's hand of cards.
///   </description>
/// </file>

using Group2_COSC2200_Project.model;
using System.Collections.ObjectModel;

namespace Group2_COSC2200_Project.viewmodel
{
    /// <summary>
    /// Represents the ViewModel for a player's hand in the game, managing the visualization of the cards within the 
    /// hand. It utilizes an observable collection of CardViewModel objects to enable dynamic updates to the UI as the 
    /// hand's composition changes.
    /// </summary>
    public class HandViewModel : ViewModelBase
    {
        /// <summary>
        /// Holds the collection of CardViewModels that represent each card in the player's hand.
        /// </summary>
        public ObservableCollection<CardViewModel> Cards { get; }

        /// <summary>
        /// Initializes a new instance of the HandViewModel class, converting each card in the provided hand into a 
        /// CardViewModel for UI rendering.
        /// </summary>
        /// <param name="hand">The Hand model containing the card data.</param>
        /// <param name="isPlayerHand">Indicates whether the hand belongs to the player, 
        /// to control card face visibility</param>
        public HandViewModel(Hand hand, bool isPlayerHand)
        {
            Cards = new ObservableCollection<CardViewModel>();

            // Loops through each card object in the Hand object, and adds a new CardViewModel object for each.
            // This allows rendering of all cards in the hand based on how many cards are in that hand.
            foreach (Card card in hand.Cards)
            {
                CardViewModel cardViewModel = new CardViewModel(card)
                {
                    IsPlayerCard = isPlayerHand
                };
                Cards.Add(cardViewModel);
            }
        }
    }
}
