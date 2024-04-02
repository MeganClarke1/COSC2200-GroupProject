using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group2_COSC2200_Project.model
{
    /// <summary>
    /// Represents a collection of statistics tracking ONE player.
    /// </summary>
    public class Statistics
    {

        /// <summary>
        /// Represents a given value for a win or a loss. FOr use in previousGameResult to ensure type safety.
        /// </summary>
        public enum LastGameResult
        {
            None = 0,
            Win = 1,
            Loss = 2
        }

        /// <summary>
        /// Represnts the player's unique id.
        /// </summary>
        public int PlayerID {  get; private set; }
        
        /// <summary>
        /// The player's name.
        /// </summary>
        public string PlayerName { get; private set; }

        /// <summary>
        /// Player wins.
        /// </summary>
        public int PlayerWins { get; private set; }

        /// <summary>
        /// Player losses.
        /// </summary>
        public int PlayerLosses { get; private set; }

        /// <summary>
        /// Player's total games.
        /// </summary>
        public int TotalGames { get; private set; }

        /// <summary>
        /// Players current streak.
        /// </summary>
        public int CurrentStreak {  get; private set; }

        /// <summary>
        /// Tracks the last game result for determining current streak.
        /// Win = 1
        /// Loss = 2
        /// To Use As: When a player finishes a game, determine the value of the result (win = 1 / loss = 2), compare that to the value of 
        ///            the value of previousGameResult. If it's ==, then that means they're either on a win streak(++), or loss streak (++). 
        ///            Now we can determine streak length, and then type of streak (win/loss), via the current result of the game that was just played.
        /// </summary>
        public LastGameResult previousGameResult { get; private set; } 

        /// <summary>
        /// Constructor - For initialization after a player creates a profile. (Initialized to 0 for stats)
        /// </summary>
        public Statistics(int _PlayerId, string _PlayerName, int _PlayerWins, int _PlayerLosses, int _TotalGames, int _CurrentStreak, LastGameResult _previousGameResult)
        { 
            this.PlayerID = _PlayerId;
            this.PlayerName = _PlayerName;
            this.PlayerWins = _PlayerWins;
            this.PlayerLosses = _PlayerLosses;
            this.TotalGames = _TotalGames;
            this.CurrentStreak = _CurrentStreak;
            this.previousGameResult = _previousGameResult;
        }

        /// <summary>
        /// Takes a Statistics object to save to the JSON.
        /// </summary>
        /// <param name="StatsToJSON"></param>
        public void SaveStatistics (Statistics StatsToJSON)
        {

        }

        /// <summary>
        /// Takes a unique ID to identify the player record in the JSON, returns that record. Parses it to a statistics object and returns it ??
        /// </summary>
        /// <param name="uniqueID"> The unique id to fetch the given corresponding statistics record from the JSON.</param>
        /// <returns> JSONStats - A JSON formatted string representing a player's statistics object (record). </returns>
        public Statistics LoadStatistics(int uniqueID)
        {




            return StatsFromJSON;
        }

        /// <summary>
        /// Takes a unique player ID to identify the corresponding record in the JSON. Resets the stats to default.
        /// 
        /// </summary>
        /// <notes> *** MAY need to also pass it a blank statistics object to make determining the new values easier. </notes>
        /// <param name="uniqueID"></param>
        public void ResetStats(int uniqueID)
        {

        }

    }
}
