using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group2_COSC2200_Project.model
{
    /// <summary>
    /// Represents the general game logic for the game.
    /// </summary>
    class GameFunctionality
    {

        /// <summary>
        /// Static method to merge 2 team lists into one turn list for game functionality construction
        /// Takes 2 lists of players (teams), and merges them into 1 turnlist, where player turns alternate between teams.
        /// </summary>
        /// <returns> TurnList a list of player Objects indicating turn order. </returns>
        public static List<Player> CreateTurnList(List <Player> TeamOne, List <Player> TeamTwo)
        {
            // Create a turnList
            List<Player> TurnList = new List<Player>();

            // Alternate adding a member from each team to create staggered turn order.
            TurnList.Add(TeamOne[0]);
            TurnList.Add(TeamTwo[0]);
            TurnList.Add(TeamOne[1]);
            TurnList.Add(TeamTwo[1]);

            return TurnList;
        }

        /// <summary>
        /// Takes a TurnList, moves the first player in the list, to the end of the list.
        /// Returns a list of players where the first player is the current turn
        /// </summary>
        /// <param name="TurnList"></param>
        /// <returns> TurnList an updated list of players where the player who just had their turn is moved to the end.</returns>
        public static List<Player> NextTurn(List<Player> TurnList)
        {
            // Fetch the current player whose turn it is
            Player CurrentTurnPlayer = TurnList[0]; 
            
            // Remove the first index position (Current player whose turn it is)
            TurnList.RemoveAt(0);

            // Re-add the removed player to the end of the list.
            // Effectively, removing them from the front, and adding them to the back of the list.
            TurnList.Add(CurrentTurnPlayer);

            return TurnList;
        }

        /// <summary>
        /// Will set the isDealer Status of the player in the last position of the turnList to dealer.
        /// </summary>
        /// <param name="TurnList">a TurnList</param>
        /// <returns>TurnList a TurnList where the player at the end of the list is made dealer.</returns>
        public static List<Player> SetDealer(List<Player> TurnList) 
        { 
            
            // Fetch the last person in the turn list and make them dealer
            Player lastPlayer = TurnList[3];

            // Because the person to the left of the dealer goes first, this means the dealer goes last
            // Therefore, dealer can be determined by whichever player is going last.
            lastPlayer.IsDealer = true;

            return TurnList;
        }

        /// <summary>
        /// Creates a new ROUND object with the selected Trump Card.
        /// Takes the selected Trump, the current player who selected it, and the 2 participating teams.
        /// Checks both teams for the presence of that player object. If the player object is found in that team,
        /// then we know what team selected the Trump, and the rounds MakerStatus (Team object) is set.
        /// Also set the TrumpCard in round object.
        /// Return a new Round Object.
        /// </summary>
        /// <param name="selectedTrumpSuit"> The card Suit property that was chosen as Trump. </param>
        /// <param name="currentPlayer"> The player whose current turn it is (Who selected the trump) </param>
        /// <param name="TeamOne"> The first team participating in the game. </param>
        /// <param name="TeamTwo"> The second team participating in the game. </param>
        /// <returns> newRound a new Round object </returns>
        public static Round TrumpSelected(Card.Suits selectedTrumpSuit, Player currentPlayer, Team TeamOne, Team TeamTwo)
        {

            // Initialize a round object (Empty Values)
            Round newRound = new Round();

            // Loops through both team lists to check for which team selected the trump. Whichever team did, update the
            // Round.MakerStatus to THAT team
            foreach (Player player in TeamOne.TeamPlayers)
            {
                if (player == currentPlayer)
                {
                    newRound.MakerTeam = TeamOne;
                }
            }
            foreach (Player player in TeamTwo.TeamPlayers)
            {
                if (player == currentPlayer)
                {
                    newRound.MakerTeam = TeamTwo;
                }
            }

            // Regardless, update Round.TrumpSuit
            newRound.TrumpSuit = selectedTrumpSuit;

            return newRound;
        }
    }
}
