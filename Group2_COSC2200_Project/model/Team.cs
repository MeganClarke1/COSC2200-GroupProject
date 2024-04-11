﻿/// <file>
///   <summary>
///     File Name: Team.cs
///   </summary>
///   <author>
///     Authors: Brody Dentinger, Megan Clarke, Colin Eade, Muhammad Yasir Patel
///   </author>
///   <created>
///     Created: Apr 1, 2024
///   </created>
///   <lastModified>
///     Last Modified: April 9, 2024
///   </lastModified>
///   <description>
///     Description: This class represents a Team object for the game.
///   </description>
/// </file>

namespace Group2_COSC2200_Project.model
{
    /// <summary>
    /// Represents a team for the Euchre game.
    /// </summary>
    public class Team
    {
        /// <summary>
        /// Creates an enum for the 2 possible TeamIDs.
        /// using enum to ensure type safety in using only team id 1 or 2
        /// There will only ever be 2 teams, so this allows use of predefined variables when using the team constructor
        /// </summary>
        public enum TeamID
        {
            TeamOne = 1,
            TeamTwo = 2
        }

        /// <summary>
        /// A unique team ID (1 or 2 only).
        /// </summary>
        public TeamID TeamId{ get; private set; }

        /// <summary>
        /// The name of the team.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// A list of player objects representing a team (2 players).
        /// </summary>
        public List<Player> TeamPlayers { get; private set; }

        /// <summary>
        /// Represents if a team has called the trump suit (Are the makers).
        /// </summary>
        public bool MakerStatus {  get; set; }

        /// <summary>
        /// Constructor for a team object.
        /// </summary>
        public Team(TeamID _teamId, List<Player> _teamPlayers, string _name)
        {
            TeamId = _teamId;
            TeamPlayers = _teamPlayers;
            MakerStatus = false;
            Name = _name;
        }

        /// <summary>
        /// Static method... Because we want to call before a team is created to make the teams for team construction
        /// Takes 2 player objects and creates a list of Player objects to be passed to the team constructor
        /// </summary>
        /// <param name="playerOne" the first player on the team
        /// <param name="playerTwo" the second player on the tem
        /// <returns> newTeam the list of Player objects representing a team </returns>
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
