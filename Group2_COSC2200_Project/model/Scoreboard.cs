/// <file>
///   <summary>
///     File Name: Scoreboard.cs
///   </summary>
///   <author>
///     Authors: Brody Dentinger, Megan Clarke, Colin Eade, Muhammad Yasir Patel
///   </author>
///   <created>
///     Created: April 6, 2024
///   </created>
///   <lastModified>
///     Last Modified: April 9, 2024
///   </lastModified>
///   <description>
///     Description: This class represents a Scoreboard for the game.
///   </description>
/// </file>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Group2_COSC2200_Project.model
{
    /// <summary>
    /// Scoreboard will track tricks/wins for each team.
    /// </summary>
    public  class Scoreboard
    {

        /// <summary>
        ///  Represents a Teams Score (Points). Calculated/updated after a trick has been complete.
        ///  Default value at initialization will be 0.
        /// </summary>
        public int TeamOneScore {  get; set; }
        public int TeamTwoScore { get; set;}

        /// <summary>
        /// Represents the tricks that have been won by each team in a round.
        /// </summary>
        public int TeamOneTricks { get; set; }
        public int TeamTwoTricks {  get; set; }

        public Scoreboard()
        { 

        }

        /// <summary>
        /// Takes the Team object of the Team that won the trick. 
        /// Checks if that winning team's id is team 1 or team 2, and increments the scoreboard property based on that.
        /// </summary>
        /// <param name="TeamWonTrick"> The team that won the trick. </param>
        public void IncrementTrickCount(Team TeamWonTrick)
        {
            if (TeamWonTrick.TeamId == Team.TeamID.TeamOne)
            {
                TeamOneTricks++;
            }
            else if(TeamWonTrick.TeamId == Team.TeamID.TeamTwo)
            {
                TeamTwoTricks++;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void IncrementScoreCount()
        {

        }

        /// <summary>
        /// Used to reset the tricks counters on the scoreboard back to 0 after a round has ended.
        /// </summary>
        public void ResetScoreboardTricks()
        {
            TeamOneTricks = 0;
            TeamTwoTricks = 0;
        }

    }
}
