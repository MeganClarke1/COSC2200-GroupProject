using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group2_COSC2200_Project.model
{
    /// <summary>
    /// Represents the entire game round (10 tricks) in the euchre game.
    /// </summary>
    class Round
    {
        /// <summary>
        /// A list of Trick Objects that the round is made up of.
        /// </summary>
        public List <Trick> TricksInRound { get; private set; }

        /// <summary>
        /// The selected trump card for the round.
        /// </summary>
        public Card.Suits? TrumpSuit { get; set; }

        /// <summary>
        /// The Team that selected the trump this round. (Maker Team)
        /// </summary>
        public Team? MakerTeam { get; set; }

        /// <summary>
        /// The Team that chose to go alone. (If applicable) (Alone Team)
        /// </summary>
        public Team? AloneTeam { get; private set; }

        /// <summary>
        /// The Team that won the round.
        /// </summary>
        public Team? WinningTeam { get; set; }

        /// <summary>
        /// The points awarded for this round to the WinningTeam.
        /// </summary>
        public int PointsAwarded { get; set; }

        /// <summary>
        /// Default constructor, creates an empty round for manipulation in the GameFunctionality TrumpSelected().
        /// </summary>
        public Round()
        {
            TricksInRound = new List<Trick>();
            TrumpSuit = null; 
            MakerTeam = null; 
            AloneTeam = null;
            //WinningTeam = new Team(); // Not sure what to do here... Set to empty Team object? or just initiate as null ... or not at all... 
            PointsAwarded = 0;          // an empty round needs to be instantiated for manipulation/editing in the GameFunctionality TrumpSelected()
        }

        public Round(List<Trick> _TrickList, Card.Suits _TrumpSuit, Team _MakerTeam, Team _AloneTeam, Team _WinningTeam, int _PointsAwarded)
        {
            this.TricksInRound = _TrickList;
            this.TrumpSuit = _TrumpSuit;
            this.MakerTeam = _MakerTeam;
            this.AloneTeam  = _AloneTeam;
            this.WinningTeam = _WinningTeam;
            this.PointsAwarded = _PointsAwarded;
        }

        /// <summary>
        /// Takes in a current round object, determines which team has more wins. 
        /// Checks which team is maker, and determines points won off factor. (Maker team win = 1 point, non-maker team = 3 points)
        /// </summary>
        /// <param name="currentRound"> The Round object currently being played to be updated </param>
        /// <param name="TeamOne"> The First team participating in the game </param>
        /// <param name="TeamTwo"> The second team participating in the game </param>
        /// <returns> currentRound a Round object with updated properties </returns>
        public Round DetermineRoundWinnerAndPoints(Round currentRound, Team TeamOne, Team TeamTwo)
        {
            
            // Check which team has maker Status
            Team currentMakerTeam = currentRound.MakerTeam;

            // Initiate team wins counters to 0
            int teamOneWins = 0;
            int teamTwoWins = 0;

            // Loop through the list of trick objects in the currentRound and increment team win counters based on the WinningTeam property in Trick
            foreach (Trick trick in currentRound.TricksInRound)
            {
                // If the WinningTeam of the Tricks team Id property is equal to TeamOne enum ...
                if (trick.TrickWinningTeam.TeamId == Team.TeamID.TeamOne)
                {
                    teamOneWins++;
                }
                else if (trick.TrickWinningTeam.TeamId == Team.TeamID.TeamTwo)
                {
                    teamTwoWins++;
                }
            }

            // Check which team has more wins and set the winning team property of the current round to the team with more wins.
            if (teamOneWins > teamTwoWins)
            {
                currentRound.WinningTeam = TeamOne;
            }
            else
            {
                currentRound.WinningTeam = TeamTwo;
            }

            // Determine points using maker status. Maker team winning a round is 1 point. Non-maker team is 3 points.
            if (currentRound.WinningTeam == currentMakerTeam)
            {
                currentRound.PointsAwarded = 1;
            }
            else if (currentRound.WinningTeam != currentMakerTeam)
            {
                currentRound.PointsAwarded = 3;
            }

            // Set the current round PointsAwarded status 

            return currentRound;
        }
        
    }
}
