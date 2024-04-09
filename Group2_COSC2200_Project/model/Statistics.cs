/// <file>
///   <summary>
///     File Name: Statistics.cs
///   </summary>
///   <author>
///     Authors: Brody Dentinger, Megan Clarke, Colin Eade, Muhammad Yasir Patel
///   </author>
///   <created>
///     Created: April 2, 2024
///   </created>
///   <lastModified>
///     Last Modified: April 9, 2024
///   </lastModified>
///   <description>
///     Description: This class represents a players statistics
///   </description>
/// </file>

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
using System.Reflection;

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
        public static bool SaveStatistics(Statistics statsToJSON)
        {
            // Step out of the current directory to reach the base directory
            string currentDirectory = Directory.GetCurrentDirectory();

            // Navigate up two levels to reach the base directory
            string baseDirectory2 = Directory.GetParent(Directory.GetParent(currentDirectory).FullName).FullName;

            // Navigate another level up
            string baseDirectory3 = Directory.GetParent(baseDirectory2).FullName;

            // Combine that relative position twice with data and stats file.
            string relativeJSONpath1 = Path.Combine(baseDirectory3, "data");
            string relativeJSONpath2 = Path.Combine(relativeJSONpath1, "stats.json");

            try
            {
                // Read the content of the JSON file
                string json = File.ReadAllText(relativeJSONpath2);

                // Deserialize the JSON into a dictionary with the "500" identifier
                var statsContainer = JsonConvert.DeserializeObject<Dictionary<string, Statistics>>(json);

                // Update the statistics for the player
                statsContainer["500"] = statsToJSON;

                // Serialize the dictionary back to JSON and write to the file
                string updatedJson = JsonConvert.SerializeObject(statsContainer, Formatting.Indented);
                File.WriteAllText(relativeJSONpath2, updatedJson);

                return true; // Return true indicating successful update
            }
            catch (Exception ex)
            {
                // Handle any exceptions (e.g., file not found, serialization error)
                Console.WriteLine($"Error updating statistics file: {ex.Message}");
                return false; // Return false indicating failure
            }
        }



        /// <summary>
        /// Takes a unique ID to identify the player record in the JSON. Deserializes the JSON into a dictionary, where the key is the player ID.
        /// Check for the existance of the given ID, return a dictionary formatted record for that player. 
        /// </summary>
        /// <param name="uniqueID"> The unique id to fetch the given corresponding statistics record from the JSON.</param>
        /// <returns> playerStatistics - a Statistics object representing the player's stats. Or null if no record found. </returns>
        public static Statistics LoadStatistics(string uniqueID) // **** static now
        {

            // Step out of the current directory to reach the base directory
            string currentDirectory = Directory.GetCurrentDirectory();


            // Navigate up two levels to reach the base directory
            string baseDirectory2 = Directory.GetParent(Directory.GetParent(currentDirectory).FullName).FullName;

            // Navigate another level up
            string baseDirectory3 = Directory.GetParent(baseDirectory2).FullName;

            // Combine that relative position twice with data and stats file.
            string relativeJSONpath1 = Path.Combine(baseDirectory3, "data");
            string relativeJSONpath2 = Path.Combine(relativeJSONpath1, "stats.json");


            string hardCorePathTest = @"C:\Users\brody\Source\Repos\COSC2200-GroupProject\Group2_COSC2200_Project\data\stats.json";

            // Read JSON data into a dictionary with unique ID as the key
            string json = File.ReadAllText(relativeJSONpath2);

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
