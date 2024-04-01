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
        public Card? TrumpCard { get; set; }

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
        public Team? WinningTem { get; set; }

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
            TrumpCard = null; 
            MakerTeam = null; 
            AloneTeam = null;
            //WinningTeam = new Team();
            PointsAwarded = 0; 
        }

        public Round(List<Trick> _TrickList, Card _TrumpCard, Team _MakerTeam, Team _AloneTeam, Team _WinningTeam, int _PointsAwarded)
        {
            this.TricksInRound = _TrickList;
            this.TrumpCard = _TrumpCard;
            this.MakerTeam = _MakerTeam;
            this.AloneTeam  = _AloneTeam;
            this.WinningTem = _WinningTeam;
            this.PointsAwarded = _PointsAwarded;
        }

        ///<todo>Create a way to calculate the winner of a round... This will involve logic with the list of tricks and will
        /// likely need to take into account MakerTeam, and AloneTeam.... Could use the TricksInRound list (which has a winning team
        /// property for each trick) to determine how many tricks each team won, then take into account their MakerStatus to
        /// determine if they met the minimum tricks to win for a MakerTeam.
        /// Return the TEAM that won the round. </todo>
        public Team DetermineRoundWinnerAndPoints(List<Trick> TricksInRound, Team TeamOne, Team TeamTwo)
        {
            // Check which team has maker Status

            // Check how many trick wins each team has
            
            return Team WinningTeam;
        }
        
    }
}
