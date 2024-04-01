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
        
        // TODO: Add property for Hands and Kitty Card object 

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
            Player playerOne = new Player(1, "PlayerOne");
            Player playerTwo = new Player(2, "PlayerTwo");
            Player playerThree = new Player(3, "PlayerThree");
            Player playerFour = new Player(4, "PlayerFour");

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
            // Set the Dealer
            List <Player> newTurnListDealerSet = GameFunctionality.SetDealer(newTurnList);
            // Set the StartGameState TurnList to the dealer set TurnList
            startGameState.gameTurnList = newTurnListDealerSet;

            // TODO: Deal Cards ... return and set Hand objects to StartGameState properties
            GameFunctionality.DealCards(newDeck, newTurnListDealerSet);

            // TODO: Kitty Identified ... return Card object (kitty) and set to StartGameState properties


            // Instantiate StartGameState object with all determined above
            StartGameState newStartGameState = new StartGameState();

            // Return it
            return newStartGameState;
        }

    }
}
