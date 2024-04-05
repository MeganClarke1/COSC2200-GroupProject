using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group2_COSC2200_Project.model
{
    /// <summary>
    /// A class representing 1 Trick. (Each player has went once)
    /// </summary>
    class Trick
    {
        /// <summary>
        /// A list of cards that have been played during the round.
        /// </summary>
        public List<Card> PlayedCards { get; private set; }

        public Card WinningCard { get; private set; }

        public Card.Suits LeadSuit {  get; private set; }

        public Team TrickWinningTeam { get; private set; }

        // Constructor
        // TODO: decide if we need a parameterized or if blank constructor is fine, or both.
        public Trick()
        {

        }

        /// <summary>
        /// Takes a list of Card objects, iterates through them, and compares their Value property to the current Highest Value.
        /// If it is higher, reassigns the HighCardValue to that Card's current Value.
        /// Also, put that entire Card object in the HighCard variable.
        /// Will return the Card Object with the highest Value property.
        /// </summary>
        /// <param name="PlayedCards"> A list of card objects that have been played in a trick. </param>
        /// <returns> highCard - A card object with the highest .Value. </returns>
        public static Card DetermineTrickWinner(List<Card> PlayedCards)
        {   
            int highCardValue = 0;
            Card highCard = null;

            foreach(var card in PlayedCards)
            {
                if (card.Value > highCardValue)
                {
                    highCardValue = card.Value;
                    highCard = card;
                }
            }

            return highCard;
        }


    }
}
