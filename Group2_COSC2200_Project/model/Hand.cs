/// <file>
///   <summary>
///     File Name: Hand.cs
///   </summary>
///   <author>
///     Authors: Brody Dentinger, Megan Clarke, Colin Eade, Muhammad Yasir Patel
///   </author>
///   <created>
///     Created: March 24, 2024
///   </created>
///   <lastModified>
///     Last Modified: April 9, 2024
///   </lastModified>
///   <description>
///     Description: This class represents a Hand object for the game.
///   </description>
/// </file>

namespace Group2_COSC2200_Project.model
{
    /// <summary>
    /// Represents a hand of cards held by a player in the Euchre game.
    /// </summary>
    public class Hand
    {
        /// <summary>
        /// A list containing the cards in the player's hand.
        /// </summary>
        public List<Card> Cards { get; set; }

        /// <summary>
        /// Initializes a new empty hand.
        /// </summary>
        public Hand()
        {
           Cards = new List<Card>();
        }

        /// <summary>
        /// Adds a card to the player's hand.
        /// </summary>
        /// <param name="card">The card to be added.</param>
        public void AddCard(Card card)
        {
            Cards.Add(card);
        }

        /// <summary>
        /// Removes the specified card from the player's hand.
        /// </summary>
        /// <param name="card">The card to be removed.</param>
        /// <returns>The removed card.</returns>
        public Card RemoveCard(Card card)
        {
            Cards.Remove(card);
            return card;
        }
    }
}
