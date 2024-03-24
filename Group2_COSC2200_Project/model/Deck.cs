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
        public void Shuffle() // TODO: Cite this the algo
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
        /// </summary>
        /// <returns>The card that was at the top of the deck.</returns>
        public Card DealCard()
        {
            Card card = Cards[Cards.Count - 1];
            Cards.RemoveAt(Cards.Count - 1);
            return card;
        }
    }
}
