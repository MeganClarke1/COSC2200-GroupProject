using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group2_COSC2200_Project.model
{
    /// <summary>
    /// Represents a consolidation of objects/properties needed to go from Start Game --> Ready for First Card Played State
    /// </summary>
    public class StartGameState
    {

        /// <summary>
        /// Represents the currentGameDeck
        /// </summary>
        public Deck gameDeck {  get; set; }

        public Team gameTeamOne { get; set; }

        public Team gameTeamTwo { get; set; }

        public List<Player> gameTurnList { get; set; }

        /// <summary>
        /// A constructor for the StartGameState
        /// Constructs an empty StartGameState for manipulation in the static method newStartGameState().
        /// </summary>
        public StartGameState() 
        { 
            
        
        }

        /// <summary>
        /// A static method that can be called before a StartGameState object is instantiated.
        /// Calls all the appropriate constructors and methods for StartGameState object creation.
        /// </summary>
        /// <return> StartGameState object ready to be passed to the View from the ViewModel function that calls this after 
        /// Start Button is pressed in the view. </return>
        public static StartGameState newStartGameState()
        {

            // Instantiate an empty StartGameState object.
            StartGameState startGameState = new StartGameState();

            // Create a new deck and set it to the StartGameState property.
            Deck newDeck = new Deck();
            startGameState.gameDeck = newDeck;

            // Create 4 new players
            Player playerOne = new Player(1, "PlayerOne", false);// TODO: WHEN MERGED WITH COLINS CHANGES WE CAN TAKE "FALSE" OUT AS THATS DEFAULT NOW
            Player playerTwo = new Player(2, "PlayerTwo", false);
            Player playerThree = new Player(3, "PlayerThree", false);
            Player playerFour = new Player(4, "PlayerFour", false);

            // Create 2 teams .... Do we want EACH team as a property here ?
            List<Player>TeamOneList = Team.createTeam(playerOne, playerTwo);
            List<Player> TeamTwoList = Team.createTeam(playerThree, playerFour);
            // Instantiate the 2 new Team Objects, each with a unique id, and a list of Player Objects (the team).
            Team newTeamOne = new Team(Team.TeamID.TeamOne, TeamOneList);
            Team newTeamTwo = new Team(Team.TeamID.TeamTwo, TeamTwoList);
            // Set the teamOne and teamTwo properties of StartGameState to the 2 teams.
            startGameState.gameTeamOne = newTeamOne;
            startGameState.gameTeamTwo = newTeamTwo;

            // Create the TurnList
            List<Player> newTurnList = GameFunctionality.CreateTurnList(TeamOneList, TeamTwoList);

            // Create a new gameFunctionality object w/ the turnList
            GameFunctionality newGFObj = new GameFunctionality(newTurnList);

            // Set the Dealer
            newGFObj.SetDealer();

            // Deal Cards

            // Kitty Identified

            // Instantiate StartGameState object with all determined above
            StartGameState newStartGameState = new StartGameState();

            // Return it
            return newStartGameState;
        }

    }
}
