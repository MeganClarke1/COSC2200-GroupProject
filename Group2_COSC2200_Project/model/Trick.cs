using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
        /// Takes a list of played cards, the current turn list, and both Team objects.
        ///     Determines the highest card of all played cards, stores the player who that card belongs to. (Winning Player)
        ///     Loops through both teams to find which team the winning player belongs to. (Comparing player IDs)
        ///     Returns the team who owns the winning player. (Winning Team)
        /// </summary>
        /// <param name="PlayedCards"> The List of Card objects that have been played this trick. </param>
        /// <param name="TeamOne"> The current team one of the game class. </param>
        /// <param name="TeamTwo"> The current team two of the game class. </param>
        /// <returns> WinningTeam - The Team object who won the trick. </returns>
        public static Team DetermineTrickWinner(List<Card> PlayedCards, 
                                                Team TeamOne, Team TeamTwo)
        {   
            // Initialize the highCard value for comparison.
            int highCardValue = 0;
            Card highCard = null;

            // Compare each card in played cards to the current high card value. Reassign if higher.
            foreach(var card in PlayedCards)
            {
                if (card.Value > highCardValue)
                {
                    highCardValue = card.Value;
                    highCard = card;
                }
            }

            // Fetch the player who owned the card that has the highest value.
            int winningPlayerId = highCard.CardsAssociatedToPlayers;

            MessageBox.Show("Player with ID " + winningPlayerId + " has won the hand");

            // Initialize an empty winningTeam to hold the result.
            Team winningTeam = null;

            // For each player in each team, check if their id matches the winningPlayer's ID. If so, set their team to be the winner.
            foreach (Player player in TeamOne.TeamPlayers)
            {
                if (winningPlayerId == player.PlayerID)
                {
                    winningTeam = TeamOne;
                }
            }
            foreach (Player player in TeamTwo.TeamPlayers)
            {
                if (winningPlayerId == player.PlayerID)
                {
                    winningTeam = TeamTwo;
                }
            }
            
            return winningTeam;
        }
    }
}
