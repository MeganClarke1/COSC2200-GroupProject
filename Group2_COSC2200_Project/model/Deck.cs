/// <file>
///   <summary>
///     File Name: Deck.cs
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
///     Description: This class represents a Deck object for the game.
///   </description>
/// </file>

namespace Group2_COSC2200_Project.model
{
    /// <summary>
    /// Represents a deck of cards for the Euchre game.
    /// </summary>
    public class Deck
    {
        /// <summary>
        /// Gets the list of cards in the deck.
        /// </summary>
        public List<Card> Cards { get; private set; }

        /// <summary>
        /// Initializes a new instance of the Deck class, creating a full deck of cards based on the specified suits and ranks.
        /// </summary>
        public Deck()
        {
            Cards = new List<Card>();
            foreach (Card.Suits suit in Enum.GetValues(typeof(Card.Suits)))
            {
                foreach (Card.Ranks rank in Enum.GetValues(typeof(Card.Ranks)))
                {
                    Cards.Add(new Card(suit, rank));
                }
            }
        }

        /// <summary>
        /// Shuffles the cards in the deck using the Fisher-Yates algorithm.
        /// </summary>
        public void Shuffle() // TODO: Cite this algo
        {
            Random rnd = new();
            for (int i = Cards.Count - 1; i > 0; i--)
            {
                int swapIndex = rnd.Next(i + 1);
                (Cards[swapIndex], Cards[i]) = (Cards[i], Cards[swapIndex]);
            }
        }

        /// <summary>
        /// Removes and returns the card at the top of the deck.
        /// Also, Attaches the playerID from player class to the CardsAssociatedToPlayers
        /// </summary>
        /// <returns>The card that was at the top of the deck.</returns>
        public Card DealCard(Player player)
        {
            Card card = Cards[Cards.Count - 1];
            card.CardsAssociatedToPlayers = player.PlayerID;
            Cards.RemoveAt(Cards.Count - 1);
            return card;
        }

        /// <summary>
        /// Takes a deck object and determines the 21st card. 
        /// Because euchre deals out 20 cards, the 21st card would be the kitty.
        /// </summary>
        /// <param name="deck"> The Deck to determine the kitty from. </param>
        /// <returns> kitty - the 21st card in the deck </returns>
        public Card DetermineKitty()
        {
            Card kitty = Cards[1];
            return kitty;
        }
    }
}
