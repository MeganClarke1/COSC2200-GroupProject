using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows;

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
        public int PlayerID {  get; set; }
        
        /// <summary>
        /// The player's name.
        /// </summary>
        public string PlayerName { get; set; }

        /// <summary>
        /// Player wins.
        /// </summary>
        public int PlayerWins { get; set; }

        /// <summary>
        /// Player losses.
        /// </summary>
        public int PlayerLosses { get; set; }

        /// <summary>
        /// Player's total games.
        /// </summary>
        public int TotalGames { get; set; }

        /// <summary>
        /// Players current streak.
        /// </summary>
        public int CurrentStreak {  get; set; }

        /// <summary>
        /// Tracks the last game result for determining current streak.
        /// Win = 1
        /// Loss = 2
        /// To Use As: When a player finishes a game, determine the value of the result (win = 1 / loss = 2), compare that to the value of 
        ///            the value of previousGameResult. If it's ==, then that means they're either on a win streak(++), or loss streak (++). 
        ///            Now we can determine streak length, and then type of streak (win/loss), via the current result of the game that was just played.
        /// </summary>
        public LastGameResult previousGameResult { get; set; } 

        public Statistics()
        {

        }

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

            // Construct the file path for the stats.json file in the Data folder ... Intended to work even even this is not being run on local.
            string dataFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            string jsonFilePath = Path.Combine(dataFolderPath, "stats.json");

            // Read the content of the JSON file
            string json = File.ReadAllText(jsonFilePath);

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
            File.WriteAllText(jsonFilePath, updatedJson); 
        }

        /// <summary>
        /// Takes a unique ID to identify the player record in the JSON. Deserializes the JSON into a dictionary, where the key is the player ID.
        /// Check for the existance of the given ID, return a dictionary formatted record for that player. 
        /// </summary>
        /// <param name="uniqueID"> The unique id to fetch the given corresponding statistics record from the JSON.</param>
        /// <returns> playerStatistics - a Statistics object representing the player's stats. Or null if no record found. </returns>
        public static Statistics LoadStatistics(string uniqueID) // **** static now
        {

            // Construct the file path for the stats.json file in the Data folder ... Intended to work even even this is not being run on local.
            string dataFolderPath = Path.Combine(Environment.CurrentDirectory, "data");
            string jsonFilePath = Path.Combine(dataFolderPath, "stats.json");

            string hardCorePathTest = @"C:\Users\brody\Source\Repos\COSC2200-GroupProject\Group2_COSC2200_Project\data\stats.json";

            // Read JSON data into a dictionary with unique ID as the key
            string json = File.ReadAllText(hardCorePathTest);
            
            Dictionary<string, Statistics> statisticsDict = JsonConvert.DeserializeObject<Dictionary<string, Statistics>>(json);

            // Check if the dictionary contains the unique ID
            if (statisticsDict.ContainsKey(uniqueID))
            {
                Statistics playerStatistics = new Statistics(); // Create a new instance of Statistics
                                                                // Populate the properties of the new Statistics object with data from the dictionary
                var statsFromDict = statisticsDict[uniqueID];
                playerStatistics.PlayerID = statsFromDict.PlayerID;
                playerStatistics.PlayerName = statsFromDict.PlayerName;
                playerStatistics.PlayerWins = statsFromDict.PlayerWins;
                playerStatistics.PlayerLosses = statsFromDict.PlayerLosses;
                playerStatistics.TotalGames = statsFromDict.TotalGames;
                playerStatistics.CurrentStreak = statsFromDict.CurrentStreak;
                playerStatistics.previousGameResult = statsFromDict.previousGameResult;
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
