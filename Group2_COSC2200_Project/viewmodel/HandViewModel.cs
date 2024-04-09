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
    /// A handViewModel class for defining a players hand.
    /// </summary>
    public class HandViewModel : ViewModelBase
    {
        /// <summary>
        /// An observable collection of CardViewModel objects. Defining a hand as cardViewModels for full image rendering.
        /// </summary>
        public ObservableCollection<CardViewModel> Cards { get; }

        /// <summary>
        /// A constructor for our HandViewModel
        /// </summary>
        /// <param name="hand"> The hand object to be passed/rendered in the view. </param>
        public HandViewModel(Hand hand)
        {
            Cards = new ObservableCollection<CardViewModel>();

            // Loops through each card object in the Hand object, and adds a new CardViewModel object for each.
            // This allows rendering of all cards in the hand based on how many cards are in that hand.
            foreach (Card card in hand.Cards)
            {
                CardViewModel cardViewModel = new CardViewModel(card);
                Cards.Add(cardViewModel);
            }
        }
    }
}
