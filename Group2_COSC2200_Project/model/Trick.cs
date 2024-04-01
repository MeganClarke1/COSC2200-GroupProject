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

        // Methods
        // TODO: Logic for determining a Trick Winner
        public DetermineTrickWinner()
        {

        }
        

    }
}
