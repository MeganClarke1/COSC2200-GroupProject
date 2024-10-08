﻿/// <file>
///   <summary>
///     File Name: Card.cs
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
///     Description: This class represents a Card object for the game.
///   </description>
/// </file>

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
        public Suits Suit { get; set; }

        /// <summary>
        /// Gets the rank of the card.
        /// </summary>
        public Ranks Rank { get; }

        /// <summary>
        /// Gets or sets the game-specific value of the card.
        /// </summary>

        public int Value { get; private set; }

        /// <summary>
        /// Represents the card associated to the player. We are doing that by attaching the players ID to this attribute.
        /// </summary>
        public int CardsAssociatedToPlayers { get; set; }

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

            // Determine the card's colour based on its suit.
            Colour = suit switch
            {
                Suits.Clubs => Colours.Black,   // Clubs are black.
                Suits.Spades => Colours.Black,  // Spades are black.
                _ => Colours.Red,               // All other suits (Diamonds and Hearts) are red.
            };

            // Assign the card's value based on its rank.
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

        /// <summary>
        /// Determines the colour of a card based on its suit.
        /// </summary>
        /// <param name="suit">The suit of the card.</param>
        /// <returns>The colour of the card.</returns>
        private Colours GetColourFromSuit(Suits suit)
        {
            return suit switch
            {
                Suits.Clubs => Colours.Black,
                Suits.Spades => Colours.Black,
                _ => Colours.Red,
            };
        }

        /// <summary>
        /// Sets the value of the card based on its relationship to the trump suit.
        /// </summary>
        /// <param name="trumpSuit">The suit that has been declared as trump for the current round.</param>
        public void SetTrumpSuitValue(Suits trumpSuit)
        {
            // Colour of the trump suit used to determine the left bower
            Colours trumpColour = GetColourFromSuit(trumpSuit);

            if (Suit == trumpSuit)
            {
                if (Rank == Ranks.Jack)
                {
                    Value = 21; // Set the value of the right bower to 21, making it the highest value
                }
                else
                {
                    Value += 13; // Sets Trump suit values: Nine -> 14, Ten -> 15, Queen -> 17, King -> 18, Ace -> 19
                }
            } // Finds the left bower
            else if (Colour == trumpColour && Suit != trumpSuit && Rank == Ranks.Jack)
            {
                Value = 20; // Set the value of the left bower to 20, making it the second highest value
            }
        }

        /// <summary>
        /// Adjusts the value of the card based on its relationship to the lead suit in a trick, 
        /// provided it's not also the trump suit.
        /// </summary>
        /// <param name="leadSuit">The suit that was led in the current trick.</param>
        /// <param name="trumpSuit">The suit that has been declared as trump for the current round, 
        /// which outranks all other suits.</param>
        public void SetLeadSuitValue(Suits leadSuit, Suits trumpSuit)
        {
            if (Suit == leadSuit && Suit != trumpSuit)
            {
                Value += 6;
            }
        }
    }
}
