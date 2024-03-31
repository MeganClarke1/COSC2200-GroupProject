using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group2_COSC2200_Project.model
{
    class Team
    {
        // using enum to ensure type safety in using only team id 1 or 2
        // There will only ever be 2 teams, so this allows use of predefined variables when using the team constructor
        public enum TeamID
        {
            TeamOne = 1,
            TeamTwo = 2
        }
        // Properties
        public TeamID TeamId{ get; private set; }
        public List<Player> TeamPlayers { get; private set; } 
        public bool MakerStatus { get; private set; }
        public bool AloneStatus { get; private set; }
        public bool CalledTrump { get; private set; }

        // Constructor 
        public Team(TeamID _teamId, List<Player> _teamPlayers, bool _makerStatus, bool _aloneStatus, bool _calledTrump)
        {
            this.TeamId = _teamId;
            this.TeamPlayers = _teamPlayers;
            this.MakerStatus = _makerStatus;
            this.AloneStatus = _aloneStatus;
            this.CalledTrump = _calledTrump;
        }

        // Static method... Because we want to call before a team is created to make the teams for team construction
        // Takes 2 player objects and creates a list of Player objects to be passed to the team constructor
        public static List<Player>createTeam(Player playerOne, Player playerTwo)
        {

            // Create a team list
            List<Player> newTeam = new List<Player>();

            // Add both players to it
            newTeam.Add(playerOne);
            newTeam.Add(playerTwo);

            return newTeam;             
        }
    }
}
