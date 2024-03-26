namespace Group2_COSC2200_Project.model
{
    /// <summary>
    /// Represents a card in the Euchre game, including its colour, suit, rank, and game value.
    /// </summary>
    public class Card
    {
        /// <summary>
        /// Defines the two possible colours of a card, which can be Black or Red.
        /// </summary>
        public enum Colours
        {
            Black,
            Red
        }
                                                                                                                       
        /// <summary>
        /// Defines the four suits a card can belong to: Clubs, Diamonds, Hearts, or Spades.
        /// </summary>
        public enum Suits
        {
            Clubs,
            Diamonds,
            Hearts,
            Spades
        }

        /// <summary>
        /// Represents the rank of the card, which can be Nine, Ten, Jack, Queen, King, or Ace.
        /// </summary>
        public enum Ranks
        {
            Nine,
            Ten,
            Jack,
            Queen,
            King,
            Ace
        }

        /// <summary>
        /// Gets the colour of the card.
        /// </summary>
        public Colours Colour { get; }

        /// <summary>
        /// Gets the suit of the card.
        /// </summary>
        public Suits Suit { get; }

        /// <summary>
        /// Gets the rank of the card.
        /// </summary>
        public Ranks Rank { get; }

        /// <summary>
        /// Gets or sets the game-specific value of the card.
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Initializes a new Card instance with specified suit and rank, determining its colour and initial value.
        /// Clubs and Spades are Black; Diamonds and Hearts are Red. Initial values are assigned from 1 (Nine) to 6 (Ace).
        /// </summary>
        /// <param name="suit">The suit of the card.</param>
        /// <param name="rank">The rank of the card.</param>
        public Card(Suits suit, Ranks rank)
        {
            Suit = suit;
            Rank = rank;

            Colour = suit switch
            {
                Suits.Clubs => Colours.Black,
                Suits.Spades => Colours.Black,
                _ => Colours.Red,
            };

            Value = rank switch
            {
                Ranks.Nine => 1,
                Ranks.Ten => 2,
                Ranks.Jack => 3,
                Ranks.Queen => 4,
                Ranks.King => 5,
                Ranks.Ace => 6,
                _ => 0,
            };
        }

        /// <summary>
        /// Returns a string that represents the current Card object.
        /// </summary>
        /// <returns>A string that represents the current card in the format "[Rank] of [Suit]".</returns>
        public override string ToString()
        {
            return $"{Rank} of {Suit}";
        }
    }
}
