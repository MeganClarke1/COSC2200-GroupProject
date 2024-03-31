using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group2_COSC2200_Project.model
{
    class GameFunctionality
    {   
        // A list of players where the first player in the list is the current turn.
        public List<Player> TurnList{ get; private set; }

        // Static method to merge 2 team lists into one turn list for game functionality construction
        // Takes 2 lists of players (teams), and merges them into 1 turnlist, where players on the same team 
        // have their turns spaced out with the other team
        public static List<Player> CreateTurnList(List <Player> TeamOne, List <Player> TeamTwo)
        {
            // Create a turnList
            List<Player> TurnList = new List<Player>();

            TurnList.Add(TeamOne[0]);
            TurnList.Add(TeamTwo[0]);
            TurnList.Add(TeamOne[1]);
            TurnList.Add(TeamTwo[1]);

            return TurnList;
        }
        
        // Takes a TurnList, moves the first player in the list, to the end of the list.
        // Returns a list of players where the first player is the current turn
        public List<Player> NextTurn(List<Player> TurnList)
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

        public List<Player> SetDealer(List<Player> TurnList) 
        { 
            
            // Fetch the last person in the turn list and make them dealer
            Player lastPlayer = TurnList[3];

            // Because the person to the left of the dealer goes first, this means the dealer goes last
            // Therefore, dealer can be determined by whichever player is going last.
            lastPlayer.IsDealer = true;

            return TurnList;
        } 
    }
}
