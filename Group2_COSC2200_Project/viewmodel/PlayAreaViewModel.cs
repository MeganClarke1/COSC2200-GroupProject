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
    /// The playAreaViewModel for center area (cards played)
    /// </summary>
    public class PlayAreaViewModel : ViewModelBase
    {
        /// <summary>
        /// An observable collection of cardViewModel objects to be rendered dynamically to the view.
        /// </summary>
        public ObservableCollection<CardViewModel> PlayedCards { get; }

        /// <summary>
        /// The Constructor, uses a list of CardViewModel objects to render multiple cards to the PlayArea.
        /// </summary>
        /// <param name="playedCardsVMP"> playedCards - a list of Card Objects </param>
        public PlayAreaViewModel(List<Card> playedCardsVMP)
        {
            PlayedCards = new ObservableCollection<CardViewModel>();

            // For each card in the list, create a new CardViewModel with that given card, and add it to the collection. 
            foreach (Card card in playedCardsVMP)
            {
                CardViewModel cardViewModel = new CardViewModel(card);
                PlayedCards.Add(cardViewModel);
            }
        }
    }
}