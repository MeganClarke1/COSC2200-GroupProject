using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Input;
using System.Windows.Markup;

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
        /// Takes a Statistics object to save to the JSON. Checks whether record currently exists in the JSON by comparing Statistics.PlayerID & 
        /// the playerID of the record in the JSON. Will update OR add the record based on this existence.
        /// Deserializes the entire JSON into a list for logic, then serializes it back to the JSON.
        /// </summary>
        /// <param name="statsToJSON"></param>
        public void SaveStatistics(Statistics statsToJSON)
        {
            // Intitalize a dictionary of Statistics objects... Will Hold the contents of the JSON file where the key is PLayer ID.
            Dictionary<int, Statistics> statisticsDict;

            // Read the content of the JSON file
            string json = File.ReadAllText("stats.json");

            // File is empty, or doesn't exist, so Initialize the statisticsList as an empty list of Statistics objects.
            if (string.IsNullOrWhiteSpace(json)) 
            {
                statisticsDict = new Dictionary<int, Statistics>(); 
            }

            // File Exists/Is Populated, so deserialize it into the dictionary.
            else 
            {
                statisticsDict = JsonConvert.DeserializeObject<Dictionary<int, Statistics>>(json); 
            }

            // Check if the PlayerID already exists in the dictionary, so update the existing record.
            if (statisticsDict.ContainsKey(statsToJSON.PlayerID))
            {
                statisticsDict[statsToJSON.PlayerID] = statsToJSON;
            }

            // Player record doesn't exist, so add a new record.
            else
            {
                statisticsDict.Add(statsToJSON.PlayerID, statsToJSON);
            }

            // Serialize the dictionary back to JSON and write to file
            string updatedJson = JsonConvert.SerializeObject(statisticsDict, Formatting.Indented); 
            File.WriteAllText("stats.json", updatedJson); 
        }

        /// <summary>
        /// Takes a unique ID to identify the player record in the JSON. Deserializes the JSON into a dictionary, where the key is the player ID.
        /// Check for the existance of the given ID, return a dictionary formatted record for that player. 
        /// </summary>
        /// <param name="uniqueID"> The unique id to fetch the given corresponding statistics record from the JSON.</param>
        /// <returns> playerStatistics - a Statistics object representing the player's stats. </returns>
        public Statistics LoadStatistics(int uniqueID)
        {
            // Read JSON data into a dictionary with unique ID as the key
            string json = File.ReadAllText("stats.json");
            Dictionary<int, Statistics> statisticsDict = JsonConvert.DeserializeObject<Dictionary<int, Statistics>>(json);

            // Check if the dictionary contains the unique ID
            if (statisticsDict.ContainsKey(uniqueID))
            {
                Statistics playerStatistics = statisticsDict[uniqueID];

                return playerStatistics; 
            }
            else
            {
                return null; // Return null if not found
            }
        }

        /// <summary>
        /// Takes a unique player ID to identify the corresponding record in the JSON. Resets the stats to default.
        /// 
        /// </summary>
        /// <notes> *** MAY need to also pass it a blank statistics object to make determining the new values easier. </notes>
        /// <param name="uniqueID"></param>
        public void ResetStats(int uniqueID)
        {
            ///<todo> Do We NEED Reset stats? RE: Extra feature...? </todo>
        }

    }
}
