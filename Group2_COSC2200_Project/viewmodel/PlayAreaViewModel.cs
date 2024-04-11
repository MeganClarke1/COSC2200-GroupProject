/// <file>
///   <summary>
///     File Name: PlayAreaViewModel.cs
///   </summary>
///   <author>
///     Authors: Brody Dentinger, Megan Clarke, Colin Eade, Muhammad Yasir Patel
///   </author>
///   <created>
///     Created: Apr 5, 2024
///   </created>
///   <lastModified>
///     Last Modified: April 9, 2024
///   </lastModified>
///   <description>
///     Description: This class represents the PlayAreaViewModel, which is the area where players play cards in a game.
///   </description>
/// </file>

using Group2_COSC2200_Project.model;
using System.Collections.ObjectModel;

namespace Group2_COSC2200_Project.viewmodel
{
    /// <summary>
    /// Represents the ViewModel for the play area in the card game, which is the central area where cards are played 
    /// during a round. This ViewModel manages the visualization of the cards within the play area, using an observable 
    /// collection to enable dynamic updates to the UI as cards are played.
    /// </summary>
    public class PlayAreaViewModel : ViewModelBase
    {
        /// <summary>
        /// Holds the collection of CardViewModels that represent each card played into the play area. 
        /// </summary>
        public ObservableCollection<CardViewModel> PlayedCards { get; }

        /// <summary>
        /// Initializes a new instance of the PlayAreaViewModel class. It converts each card played in the current 
        /// round into a CardViewModel for UI rendering.
        /// </summary>
        /// <param name="playedCardsVMP">A list of Card models representing the cards that have been played in the 
        /// current round.</param>
        public PlayAreaViewModel(List<Card> playedCardsVMP)
        {
            PlayedCards = new ObservableCollection<CardViewModel>();

            // For each card in the list, create a new CardViewModel with that given card, and add it to the collection. 
            foreach (Card card in playedCardsVMP)
            {
                CardViewModel cardViewModel = new CardViewModel(card)
                {
                    IsInPlayArea = true
                };
                PlayedCards.Add(cardViewModel);
            }
        }
    }
}