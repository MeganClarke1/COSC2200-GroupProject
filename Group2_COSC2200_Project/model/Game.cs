/// <file>
///   <summary>
///     File Name: Game.cs
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
///     Description: This class represents a consolidation of needed data representative of a Game.
///   </description>
/// </file>

using System.Windows;

namespace Group2_COSC2200_Project.model
{
    /// <summary>
    /// Manages the lifecycle and rules of a card game, including setup, state transitions, player actions, and scoring. 
    /// Supports both human and AI players, handling game flow and interactions to facilitate a complete game experience.
    /// </summary>
    public class Game
    {

        #region Properties
        /// <summary>
        /// An enum of gameStates to transition between various states of game.
        /// </summary>
        public enum GameState
        {
            Initialize,
            Start,
            TrumpSelectionFromKitty,
            TrumpSelectionPostKitty,
            DealerKittySwap,
            Play,
            EndOfGame
        }

        /// <summary>
        /// Tracks the current state of the game.
        /// </summary>
        public GameState CurrentState { get; private set; }

        /// <summary>
        /// Event triggered on game actions, allowing for UI updates or logic hooks.
        /// </summary>
        public event EventHandler OnAction;


        /// <summary>
        /// Represents the first player in the game (The user).
        /// </summary>
        public Player PlayerOne { get; private set; }

        /// <summary>
        /// Represents the second player in the game.
        /// </summary>
        public Player PlayerTwo { get; private set; }

        /// <summary>
        /// Represents the third player in the game.
        /// </summary>
        public Player PlayerThree { get; private set; }

        /// <summary>
        /// Represents the fourth player in the game.
        /// </summary>
        public Player PlayerFour { get; private set; }

        /// <summary>
        /// Represents the first team, comprising PlayerOne and PlayerThree.
        /// </summary>
        public Team Team1 { get; private set; }

        /// <summary>
        /// Represents the second team, comprising PlayerTwo and PlayerFour.
        /// </summary>
        public Team Team2 { get; private set; }

        /// <summary>
        /// Represents the deck of cards used in the game.
        /// </summary>
        public Deck Deck { get; private set; }

        /// <summary>
        /// Holds cards not in play that may affect game decisions.
        /// </summary>
        public List<Card> Kitty { get; private set; }

        /// <summary>
        /// Tracks suits not currently selected from the kitty for trump.
        /// </summary>
        public List<Card.Suits> NonKittySuits { get; private set; }

        /// <summary>
        /// Lists players in the order of their turns.
        /// </summary>
        public List<Player> TurnList { get; private set; }

        /// <summary>
        /// Counts the number of turns taken in the current round.
        /// </summary>
        public int TurnsTaken { get; private set; }

        /// <summary>
        /// Indicates the player whose turn is currently active.
        /// </summary>
        public Player CurrentPlayer { get; private set; }

        /// <summary>
        /// Stores the suit selected as trump for the current game.
        /// </summary>
        public Card.Suits TrumpSuit { get; private set; }

        /// <summary>
        /// Stores the lead suit for the current round of play.
        /// </summary>
        public Card.Suits LeadSuit { get; private set; }

        /// <summary>
        /// Contains cards played in the current round, forming the play area.
        /// </summary>
        public List<Card> PlayArea { get; private set; }

        /// <summary>
        /// Counts tricks won by Team1 in the current game.
        /// </summary>
        public int TeamOneTricks { get; private set; }

        /// <summary>
        /// Counts tricks won by Team2 in the current game.
        /// </summary>
        public int TeamTwoTricks { get; private set; }

        /// <summary>
        /// Tracks the number of cards played in the game.
        /// </summary>
        public int PlayedCardsCounter { get; private set; }

        /// <summary>
        /// Stores the current score for Team1.
        /// </summary>
        public int TeamOneScore { get; private set; }

        /// <summary>
        /// Stores the current score for Team2.
        /// </summary>
        public int TeamTwoScore { get; private set; }

        /// <summary>
        /// Store's the user stats from the GameViewModel
        /// </summary>
        public Statistics PlayerOneStats { get; set; }
        public object MessageBoxButtons { get; private set; }
        public object MessageBoxIcon { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the Game class, setting the game's initial state to 'Initialize'.
        /// </summary>
        public Game()
        {
            ChangeState(GameState.Initialize);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Changes the current state of the game to the specified state and triggers the associated state-specific method.
        /// </summary>
        /// <param name="state">The target GameState to transition to.</param>
        public void ChangeState(GameState state)
        {
            switch (state)
            {
                case GameState.Initialize:
                    Initialize();                   // Initializes the game setup.
                    break;
                case GameState.Start: 
                    Start();                        // Prepares the game to start.
                    break;
                case GameState.TrumpSelectionFromKitty:
                    TrumpSelectionFromKitty();      // Handles the selection of trump from the kitty.
                    break;
                case GameState.DealerKittySwap:
                    DealerKittySwap();              // Handles the dealer swapping cards to with the kitty.
                    break;
                case GameState.TrumpSelectionPostKitty:
                    TrumpSelectionPostKitty();      // Manages trump selection after the kitty phase.
                    break;
                case GameState.Play:
                    Play();                         // Handles the card playing phases.
                    break;
                case GameState.EndOfGame: 
                    EndOfGame();                    // Concludes the game.
                    break;
            }
        }

        /// <summary>
        /// Sets up the game by initializing all components needed to start playing, including setting the game's 
        /// state to 'Initialize', creating a new deck, preparing the kitty, defining players and teams, and setting 
        /// all gameplay-related counters and lists.
        /// </summary>
        public void Initialize()
        {
            CurrentState = GameState.Initialize;
            Deck = new Deck();
            Kitty = new List<Card>();
            PlayerOne = new Player(1, "Player 1", false);
            PlayerTwo = new Player(2, "Player 2", true);
            PlayerThree = new Player(3, "Player 3", true);
            PlayerFour = new Player(4, "Player 4", true);
            Team1 = new Team(Team.TeamID.TeamOne, Team.createTeam(PlayerOne, PlayerThree), "Team 1");
            Team2 = new Team(Team.TeamID.TeamTwo, Team.createTeam(PlayerTwo, PlayerFour), "Team 2");
            TurnList = GameFunctionality.CreateTurnList(Team1.TeamPlayers, Team2.TeamPlayers);
            PlayArea = new List<Card>();
            TurnsTaken = 0;
            TeamOneTricks = 0;
            TeamTwoTricks = 0;
            PlayedCardsCounter = 0;
            TeamOneScore = 0;
            TeamTwoScore = 0;
        }

        /// <summary>
        /// Triggers the OnAction event to notify subscribers of a action within the game, facilitating the update of 
        /// the properties within the GameViewModel, allowing the UI to visually update.
        /// </summary>
        protected virtual void RaiseOnAction()
        {
            OnAction?.Invoke(this, EventArgs.Empty);
        }

        #region Game State Methods
        /// <summary>
        /// Transitions the game to the start state. This involves setting the dealer, dealing cards to all players, 
        /// determining the initial kitty, and incrementing the total game count for player statistics.
        /// Prepares the game for initial interaction post-setup.
        /// </summary>
        public void Start()
        {
            CurrentState = GameState.Start;
            GameFunctionality.SetDealer(TurnList);
            GameFunctionality.DealCards(Deck, TurnList);
            Kitty.Add(Deck.DetermineKitty());
            PlayerOneStats.TotalGames++;
        }

        /// <summary>
        /// Initiates the trump selection phase based on the kitty card. Notifies the user of the current game status, 
        /// highlighting the dealer and prompting the first player for a decision on trump selection. 
        /// Also engages AI decision-making if applicable.
        /// </summary>
        public void TrumpSelectionFromKitty() 
        {
            // Set the current state to the trump selection from kitty state
            CurrentState = GameState.TrumpSelectionFromKitty;
            // Reset turns taken to 0
            TurnsTaken = 0;
            // Set the current player to the first in the turn list
            CurrentPlayer = TurnList[0];
            // Notifty GameViewModel
            RaiseOnAction();

            MessageBox.Show("The Dealer is " + TurnList[3].PlayerName + " \n\rThe current top kitty suit is " +
                        Kitty[0].Suit + ". \n\r" + CurrentPlayer.PlayerName + " is up!", "Trump Selection");

            // If the current player is AI, perform a decision for this stage.
            if (CurrentPlayer.IsAI)
            {
                AIDecisionFromKitty(CurrentPlayer);
            } 
        }

        /// <summary>
        /// Initiates the phase for selecting the trump suit after the initial kitty-based selection has been passed.
        /// Resets turn counters, determines available suits for trump selection excluding the kitty's suit, and sets the
        /// game state to facilitate player decisions on trump. Notifies the game's view model to update UI elements accordingly.
        /// </summary>
        public void TrumpSelectionPostKitty()
        {
            // Change the game state for post kitty
            CurrentState = GameState.TrumpSelectionPostKitty;
            // reset turns taken
            TurnsTaken = 0;
            // Set the non kitty suits for selection
            NonKittySuits = GameFunctionality.SetNonKittySuits(Kitty[0]);
            // Rotate turns
            TurnList = GameFunctionality.RotateToFirstTurn(TurnList);
            // Clear the kitty value
            Kitty.Clear();
            // Current player = the first person in the turn list
            CurrentPlayer = TurnList[0];
            // Notifty GameViewModel
            RaiseOnAction();

            MessageBox.Show("No one ordered it up. "+ CurrentPlayer.PlayerName + " is up.", "Trump Selection");
            
            // Perform the associated AI action if the player is AI
            if (CurrentPlayer.IsAI)
            {
                AIDecisionPostKitty(CurrentPlayer);
            }
        }

        /// <summary>
        /// An action which represents the functionality behind when the dealer swaps a card with the kitty.
        /// Utilizes the turnList and current state to determine dealer, and then resets turnlist.
        /// Also deals with AI logic for this state.
        /// </summary>
        public void DealerKittySwap()
        {
            // Update the game state to DealerKittySwap.
            CurrentState = GameState.DealerKittySwap;
            // Rotate the turn list to bring the dealer to the forefront.
            TurnList = GameFunctionality.RotateToDealer(TurnList);
            // Set the current player as the dealer
            CurrentPlayer = TurnList[0];
            // Notifty GameViewModel
            RaiseOnAction();

            MessageBox.Show(CurrentPlayer.PlayerName +  " can swap one of their cards with the top kitty card.", "Trump Selection");

            // Perform the associated AI action if the player is AI
            if (CurrentPlayer.IsAI)
            {
                AISwapWithKitty(CurrentPlayer);
            }
        }

        /// <summary>
        /// Initiates the play phase of the game, setting the current state to 'Play'. Determines the starting player 
        /// based on turn list rotation and handles initial play actions, including AI behavior if the starting player 
        /// is controlled by AI.
        /// </summary>
        public void Play()
        {
            // Update the game state to Play.
            CurrentState = GameState.Play;
            // Reset the turn count to 0 to initalize a round
            TurnsTaken = 0;
            // Adjust the value of cards based on the selected trump suit
            GameFunctionality.SetTrumpSuitValues(TrumpSuit, TurnList);
            // Determine who starts the round based on turn list rotation
            TurnList = GameFunctionality.RotateToFirstTurn(TurnList);
            // Update the current player to the first in the rotated turn list
            CurrentPlayer = TurnList[0];
            // Notifty GameViewModel
            RaiseOnAction();

            MessageBox.Show(CurrentPlayer.PlayerName + " will start the round.", "Round Start");

            // Perform the associated AI action if the player is AI
            if (CurrentPlayer.IsAI)
            {
                AIPlayCard(CurrentPlayer);
            }
        }

        /// <summary>
        /// Sets the state for end of game and clears all necessary properties to reset the game play.
        /// </summary>
        public void EndOfGame()
        {
            CurrentState = GameState.EndOfGame;
            PlayerOne.PlayerHand.Cards.Clear();
            PlayerTwo.PlayerHand.Cards.Clear();
            PlayerThree.PlayerHand.Cards.Clear();
            PlayerFour.PlayerHand.Cards.Clear();
            Kitty.Clear();
            PlayArea.Clear();
            Team1.MakerStatus = false;
            Team2.MakerStatus = false;
        }
        #endregion

        #region User Actions
        /// <summary>
        /// Handles the action when the user decides to "order up" the suit of the top card from the kitty, 
        /// choosing it as the trump suit for the game. This action triggers a game state update, logs the event, 
        /// and adjusts team maker statuses based on the player's decision.
        /// </summary>
        public void OrderUpFromKitty()
        {
            // Sets the trump suit based on the Kitty suit
            TrumpSuit = Kitty[0].Suit;

            // Notify GameViewModel
            RaiseOnAction();

            MessageBox.Show(CurrentPlayer.PlayerName + " has ordered it up!", "Trump Selection");

            // log trump suit selected
            Logging.LogTrumpSuit(TrumpSuit.ToString());

            // Check if any players on team one have the current player's id and set their maker status to true if they picked the trump
            setMakerStatus();
        }

        /// <summary>
        /// Check if any players on team one have the current player's id and set their maker status to true if they picked the trump
        /// </summary>
        public void setMakerStatus()
        {
            // Check if any players on team one have the current player's id and set their maker status to true if they picked the trump
            if (Team1.TeamPlayers.Any(player => player.PlayerID == CurrentPlayer.PlayerID))
            {
                Team1.MakerStatus = true;
            }
            else if (Team2.TeamPlayers.Any(player => player.PlayerID == CurrentPlayer.PlayerID))
            {
                Team1.MakerStatus = true;
            }
        }

        /// <summary>
        /// Handles the action for when the user decides to pass instead of ordering up the trump from the kitty. 
        /// This action increments the turn counter and checks if all players have passed to move to the next selection 
        /// phase. It manages turn progression and triggers AI decisions if the next player is AI-controlled.
        /// </summary>
        public void PassFromKitty()
        {
            // Notify GameViewModel
            RaiseOnAction();

            MessageBox.Show(CurrentPlayer.PlayerName + " has passed.", "Trump Selection");

            // Add 1 to the turn count, to signify a taken turn
            TurnsTaken++;

            // Check if everyone has taken a turn and switch to the next phase of trump selection if so
            if (TurnsTaken >= TurnList.Count)
            {
                ChangeState(GameState.TrumpSelectionPostKitty);
                return;
            }

            // Change to the next player
            GameFunctionality.NextTurn(TurnList);

            // Set the current player to the new player's whose turn it is
            CurrentPlayer = TurnList[0];

            // Perform the associated AI action if the next player is AI
            if (CurrentPlayer.IsAI)
            {
                AIDecisionFromKitty(CurrentPlayer);
            }
        }

        /// <summary>
        /// Selects the trump suit by the user following the initial kitty phase. This method logs the selection, 
        /// updates game state, and modifies team maker status accordingly.
        /// </summary>
        /// <param name="trumpSuit">The suit selected as trump by the user.</param>

        public void OrderUpPostKitty(Card.Suits trumpSuit)
        {
            // Set the trumpsuit based on what the user decides
            TrumpSuit = trumpSuit;

            // Notify GameViewModel
            RaiseOnAction();

            MessageBox.Show(CurrentPlayer.PlayerName + " has chosen " + TrumpSuit + ".", "Trump Selection");

            // log trump suit selected
            Logging.LogTrumpSuit(TrumpSuit.ToString());
            // Check if any players on team one have the current player's id and set their maker status to true if they picked the trump
            setMakerStatus();
        }

        /// <summary>
        /// Handles the action when the user decides to pass on selecting a trump suit following the initial kitty 
        /// reveal phase. This increments the turn count and checks if all players have passed, requiring the user 
        /// to then select a suit if they are the dealer. Manages the progression of turns and triggers AI decisions 
        /// if the next player is AI-controlled.
        /// </summary>
        public void PassPostKitty()
        {
            // Notify GameViewModel
            RaiseOnAction();

            // Add to the turn count
            TurnsTaken++;

            // Force the user to decide if they are the dealer
            if (TurnsTaken >= TurnList.Count)
            {
                MessageBox.Show("Dealer must pick a suit!", "Warning");
                return;
            }

            MessageBox.Show(CurrentPlayer.PlayerName + " has passed.", "Trump Selection");

            // Move to the next player's turn.
            GameFunctionality.NextTurn(TurnList);

            // Set the current player to the new player's whose turn it is
            CurrentPlayer = TurnList[0];

            // Perform the associated AI action if the next player is AI
            if (CurrentPlayer.IsAI) 
            {
                AIDecisionPostKitty(CurrentPlayer);
            }
        }

        /// <summary>
        /// Facilitates the action of swapping a selected card from the user's hand with the top card of the kitty. 
        /// </summary>
        /// <param name="currentPlayer">The player currently making the swap decision.</param>
        /// <param name="card">The card selected by the player to be swapped out with the kitty card.</param>
        public void SwapWithKitty(Player currentPlayer, Card card)
        {
            // Retrieve the card from the kitty
            Card kittyCard = Kitty[0];

            // Remove the selected card from the player's hand.
            currentPlayer.PlayerHand.RemoveCard(card);

            // Remove the card from the kitty
            Kitty.Remove(Kitty[0]);

            // Add the kitty card to the player's hand
            currentPlayer.PlayerHand.AddCard(kittyCard);

            // Associate the kitty card with the player
            kittyCard.CardsAssociatedToPlayers = currentPlayer.PlayerID;

            // Notify GameViewModel
            RaiseOnAction();

            // Change the state to the Play
            ChangeState(GameState.Play);
        }

        /// <summary>
        /// This method os called when a player plays a card. It first checks if it is the first card to be played in
        /// the round. If it is then the plated card's suit is set to the lead suit. The card is moved to the playing
        /// area and removed from the player's hand. If the player is not the first to play then their hand is checked
        /// to see if they have any cards in the lead suit. These cards must be played as per the rules of the game.
        /// </summary>
        /// <param name="currentPlayer">The current Player object</param>
        /// <param name="card">The selected card object to play</param>
        public void PlayCard(Player currentPlayer, Card card)
        {
            // check if the card is the first played in the round
            if (TurnsTaken == 0)
            {
                // Check if the card played is the Left bower, this means that the lead suit is not the suit on the
                // card, but actually the Trump suit
                if (IsLeftBower(card))
                {
                    LeadSuit = TrumpSuit;
                }
                else
                {
                    LeadSuit = card.Suit;
                }
                
                // Set the card values for lead suit
                GameFunctionality.SetLeadSuitValues(LeadSuit, TrumpSuit, TurnList);
            }
            else
            {
                // if it is not the first card to be played then check if it follows the set lead suit
                bool hasLeadSuit = false;
                // loop through the player hand to see if they have a card in the lead suit
                foreach (var playerCard in currentPlayer.PlayerHand.Cards)
                {
                    // If they do then mark that they have the lead suit
                    if (playerCard.Suit == LeadSuit || (LeadSuit == TrumpSuit && IsLeftBower(card)))
                    {
                        hasLeadSuit = true;
                        break;
                    }
                }

                // When a card is played check if they have a lead suit card in their hand and if the card played
                // matches the lead suit.
                if (hasLeadSuit && card.Suit != LeadSuit)
                {
                    // If the play is not valid display a message box and return to the beginning of the function
                    MessageBox.Show("The suit lead was: " + LeadSuit + ". You must play a card of that suit if you have one", "Warning");
                    return;
                }
            }

            // If the play is valid
            // Add the card to the playing area and remove form the player's hand
            PlayArea.Add(currentPlayer.PlayerHand.RemoveCard(card));
            RaiseOnAction();
            MessageBox.Show(CurrentPlayer.PlayerName + " has played their card.", "Card Played");

            // Log human card played
            Logging.LogPlayedCard(CurrentPlayer, card);

            // Increase counters for turns taken and played cards
            TurnsTaken++;
            PlayedCardsCounter++;
            // update the player to the next player in the turn list
            if (TurnsTaken >= TurnList.Count)
            {
                CheckTrickWinner();
                return;
            } 
            else
            {
                GameFunctionality.NextTurn(TurnList);
                CurrentPlayer = TurnList[0];
                if (CurrentPlayer.IsAI)
                {
                    AIPlayCard(CurrentPlayer);
                }
            }
        }

        /// <summary>
        /// Takes a card object and checks if the card is the Left Bower by matching the colour of the card if
        /// it is a Jack. This is required to comply with play rules.
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public bool IsLeftBower(Card card)
        {
            // delcare a local variable for if the card is the Left Bower
            bool isLeftBower = false;

            // Only check the suit if the card is a Jack
            if (card.Rank == Card.Ranks.Jack)
            {
                // Compare each suit to see if the card is the correct Left Bower for the already determined Trump suit
                switch (TrumpSuit)
                {
                    case Card.Suits.Hearts:
                        isLeftBower = card.Suit == Card.Suits.Diamonds; // Diamonds are the same color as Hearts
                        break;
                    case Card.Suits.Diamonds:
                        isLeftBower = card.Suit == Card.Suits.Hearts; // Hearts are the same color as Diamonds
                        break;
                    case Card.Suits.Clubs:
                        isLeftBower = card.Suit == Card.Suits.Spades; // Spades are the same color as Clubs
                        break;
                    case Card.Suits.Spades:
                        isLeftBower = card.Suit == Card.Suits.Clubs; // Clubs are the same color as Spades
                        break;
                }
            }
            // if the card is not a jack it cannot be a bower and return false
            return false;
        }
        #endregion

        #region AI Actions
        /// <summary>
        /// Executes the AI's decision-making process during the kitty selection phase of the game. The AI evaluates 
        /// its hand to decide whether to "order up" the trump suit based on the suit of the kitty's top card. 
        /// This method increments the turn counter, assesses the AI's hand for suit matching, and either selects the 
        /// trump suit or passes based on a strategic count of suit cards.
        /// </summary>
        /// <param name="currentPlayer">The AI player making the decision.</param>
        public void AIDecisionFromKitty(Player currentPlayer)
        {
            // Increment turns taken
            TurnsTaken++;

            // For each card in the current players hands, if it matches the kitty suit, increment the same suit count.
            int sameSuitCount = 0;
            foreach (Card card in currentPlayer.PlayerHand.Cards)
            {
                if (card.Suit == Kitty[0].Suit)
                {
                    sameSuitCount++;
                }
            }

            // if that count meets/exceeds 3, the trump suit is selected based on the kitty suit.
            if (sameSuitCount >= 3)
            {
                TrumpSuit = Kitty[0].Suit;
                RaiseOnAction();
                MessageBox.Show(CurrentPlayer.PlayerName + " has ordered it up!.", "Trump Selection");
                // log trump suit selected
                Logging.LogTrumpSuit(TrumpSuit.ToString());
                // Check if any players on team one have the current player's id and set their maker status to true if they picked the trump
                setMakerStatus();
                ChangeState(GameState.DealerKittySwap);
            }
            // Else, the action is passed, and the turns taken is manipulated relative to the turnlist count.
            else
            {
                RaiseOnAction();
                MessageBox.Show(CurrentPlayer.PlayerName + " has passed.", "Trump Selection");
                if (TurnsTaken >= TurnList.Count)
                {
                    ChangeState(GameState.TrumpSelectionPostKitty);
                    return;
                }
                GameFunctionality.NextTurn(TurnList);
                CurrentPlayer = TurnList[0];
                if (CurrentPlayer.IsAI)
                {
                    AIDecisionFromKitty(CurrentPlayer);
                }
            }
        }

        /// <summary>
        /// Executes the AI's decision-making for selecting a trump suit after the initial kitty selection phase. 
        /// This method counts the distribution of suits within the AI's hand, excluding the kitty's suit, to decide on
        /// the most advantageous trump suit. If it's the dealer's turn and no trump has been selected, 
        /// the AI must choose a suit. The decision is based on the suit count within its hand, aiming for the suit 
        /// with the highest count or strategic value.
        /// </summary>
        /// <param name="currentPlayer">The AI player making the trump selection decision.</param>
        public void AIDecisionPostKitty(Player currentPlayer)
        {
            TurnsTaken++; // Increment the turn counter.

            // Array to hold counts of each suit in hand, excluding the kitty's suit
            int[] suitCounts = new int[NonKittySuits.Count];

            // Count how many cards of each non-kitty suit the player has
            foreach (Card card in currentPlayer.PlayerHand.Cards)
            {
                for (int i = 0; i < NonKittySuits.Count; i++)
                {
                    if (card.Suit == NonKittySuits[i])
                    {
                        suitCounts[i]++;
                    }
                }
            }

            if (TurnsTaken < TurnList.Count) // If the AI player is not the dealer
            {
                bool trumpPicked = false;

                // Determine if any suit meets the criteria for selection (e.g., having three or more cards of that suit)
                for (int i = 0; i < NonKittySuits.Count; i++)
                {
                    if (suitCounts[i] >= 3)
                    {
                        trumpPicked = true;
                        TrumpSuit = NonKittySuits[i]; // Set the selected trump suit
                        RaiseOnAction();
                        MessageBox.Show(CurrentPlayer.PlayerName + " has chosen " + TrumpSuit + ".", "Trump Selection");
                        Logging.LogTrumpSuit(TrumpSuit.ToString()); // Log the selection
                        setMakerStatus(); // Update maker status accordingly
                        ChangeState(GameState.Play); // Move to the play phase
                        return;
                    }
                }

                // If no trump suit was picked by the AI, proceed to the next player
                if (!trumpPicked)
                {
                    RaiseOnAction();
                    MessageBox.Show(CurrentPlayer.PlayerName + " has passed.", "Trump Selection");
                    GameFunctionality.NextTurn(TurnList);
                    CurrentPlayer = TurnList[0];
                    if (CurrentPlayer.IsAI)
                    {
                        AIDecisionPostKitty(CurrentPlayer); // Call for the next AI player's decision
                    }
                }

            }
            else // Logic for the dealer AI to make a forced trump selection if necessary.
            {
                // Select the suit with the highest count as trump.
                int highestCounterIndex = 0;
                for (int i = 0; i < NonKittySuits.Count; i++)
                {
                    if (suitCounts[i] > suitCounts[highestCounterIndex])
                    {
                        highestCounterIndex = i;
                    }
                }
                TrumpSuit = NonKittySuits[highestCounterIndex]; // Set the selected trump suit.
                RaiseOnAction();
                MessageBox.Show(CurrentPlayer.PlayerName + " has chosen " + TrumpSuit + ".", "Trump Selection");
                setMakerStatus(); // Update maker status accordingly.
                Logging.LogTrumpSuit(TrumpSuit.ToString()); // Log the trump selection.
                ChangeState(GameState.Play); // Move to the play phase.
            }
        }

        /// <summary>
        /// Facilitates the AI player's decision to swap a card from their hand with the top card from the kitty.
        /// The decision process involves assessing the suit distribution in the AI's hand relative to the kitty card's
        /// suit and choosing a card to swap that optimizes the AI's hand strength.
        /// </summary>
        /// <param name="currentPlayer">The AI player conducting the card swap with the kitty.</param>
        public void AISwapWithKitty(Player currentPlayer)
        {
            Card kittyCard = Kitty[0]; // The card from the kitty to be considered for swapping
            Card cardToSwap = null;    // The card from the AI's hand that will be swapped out, initially null
            int sameSuitCount = 0;     // Counter for how many cards in the AI's hand match the kitty card's suit


            // Count the number of cards in the AI's hand that match the suit of the kitty card.
            foreach (Card card in currentPlayer.PlayerHand.Cards)
            {
                if (card.Suit == kittyCard.Suit)
                {
                    sameSuitCount++;
                }
            }

            // Determine the card to swap based on the count of matching suit cards
            if (sameSuitCount >= 2) // If there are two or more cards of the same suit as the kitty card
            {
                // For each card, assign the cards to swap
                foreach (Card card in currentPlayer.PlayerHand.Cards)
                {
                    if (card.Suit != kittyCard.Suit || !(card.Colour == kittyCard.Colour && card.Rank == Card.Ranks.Jack))
                    {
                        cardToSwap = card;
                        break;
                    }
                }
            }
            else // If there are fewer than two cards of the same suit,
            {
                // Select the lowest value card as a default swap choice.
                cardToSwap = currentPlayer.PlayerHand.Cards[0];
                foreach (Card card in currentPlayer.PlayerHand.Cards)
                {
                    if (card.Value < cardToSwap.Value)
                    {
                        cardToSwap = card;
                    }
                }
            }
            // Perform the swap between the selected card and the kitty card.
            currentPlayer.PlayerHand.RemoveCard(cardToSwap);
            Kitty.Remove(Kitty[0]);
            currentPlayer.PlayerHand.AddCard(kittyCard);
            kittyCard.CardsAssociatedToPlayers = currentPlayer.PlayerID;
            RaiseOnAction();
            MessageBox.Show(CurrentPlayer.PlayerName + " has swapped their card.", "Trump Selection");
            ChangeState(GameState.Play);
        }

        /// <summary>
        /// Determines and executes the AI player's card play based on the game's current state. This method selects 
        /// the most advantageous card to play, updates the game state to reflect the played card, and prepares for 
        /// the next turn. It accounts for the rules regarding leading suits and follows with appropriate strategy if 
        /// the AI does not possess a card of the lead suit.
        /// </summary>
        /// <param name="currentPlayer">The AI player making the play.</param>
        public void AIPlayCard(Player currentPlayer)
        {
            Card cardToPlay = null;

            if (TurnsTaken == 0) // AI Player is first turn
            {
                foreach (Card card in currentPlayer.PlayerHand.Cards)
                {
                    if (cardToPlay == null || card.Value > cardToPlay.Value)
                    {
                        cardToPlay = card;
                    }
                }
                LeadSuit = cardToPlay.Suit;
                GameFunctionality.SetLeadSuitValues(LeadSuit, TrumpSuit, TurnList);
            }
            else // AI player is not first turn
            {
                bool hasLeadSuit = false;
                // Check if they have a card in the lead suit
                foreach (Card card in  CurrentPlayer.PlayerHand.Cards)
                {
                    if (card.Suit == LeadSuit)
                    {
                        hasLeadSuit = true;
                        break;
                    }
                }
                // AI player has lead suit and must play a card from that suit
                if (hasLeadSuit)
                {
                    List<Card> playableCards = new List<Card>();

                    foreach (Card card in currentPlayer.PlayerHand.Cards)
                    {
                        // Check for lead suit or left bower
                        if (card.Suit == LeadSuit || (card.Colour == PlayArea[0].Colour && card.Rank == Card.Ranks.Jack))
                        {
                            playableCards.Add(card);
                        }
                    }
                    // Get the highest card in the hand to play
                    foreach (Card card in playableCards)
                    {
                        if (cardToPlay == null || card.Value > cardToPlay.Value)
                        {
                            cardToPlay = card;
                        }
                    }

                }
                // AI player does not have lead suit and can play any card
                else
                {
                    // Get highest card
                    foreach (Card card in currentPlayer.PlayerHand.Cards)
                    {
                        if (cardToPlay == null || card.Value > cardToPlay.Value)
                        {
                            cardToPlay = card;
                        }
                    }
                }
            }
            PlayArea.Add(currentPlayer.PlayerHand.RemoveCard(cardToPlay));  // Add the played card to the play area
            RaiseOnAction();    // Notify GameViewModel

            MessageBox.Show(CurrentPlayer.PlayerName + " has played their card.", "Card Played");

            // Log AI Cards played
            Logging.LogPlayedCard(CurrentPlayer, cardToPlay);

            TurnsTaken++;
            PlayedCardsCounter++;

            // Check for trick completion
            if (TurnsTaken >= TurnList.Count)
            {
                CheckTrickWinner();
                return;
            }
            else // If the trick is not yet complete
            {
                GameFunctionality.NextTurn(TurnList);
                CurrentPlayer = TurnList[0];

                if (CurrentPlayer.IsAI) // If the next player is AI-controlled
                {
                    AIPlayCard(CurrentPlayer);
                }
            }
        }
        #endregion

        /// <summary>
        /// Checks the winner of the current trick and performs necessary actions such as updating scores, logging the winner, 
        /// and proceeding to the next trick or round if applicable.
        /// </summary>
        public void CheckTrickWinner()
        {
            if (TurnsTaken >= TurnList.Count)
            {
                Team winningTeam = Trick.DetermineTrickWinner(PlayArea, Team1, Team2);
                MessageBox.Show("Trick Winners: " + winningTeam.Name, "Trick Won");

                // Log the trick winner
                Logging.LogTrickWinner(winningTeam.TeamId.ToString());

                // Increment the trick counter based on winning team
                if (winningTeam.TeamId == Team.TeamID.TeamOne)
                {
                    TeamOneTricks++;
                    //Scoreboard.IncrementTrickCount(TeamOne);   // **** When trying to do this same method from the Scoreboard obj, doesnt work .... ***
                }
                else if (winningTeam.TeamId == Team.TeamID.TeamTwo)
                {
                    TeamTwoTricks++;
                    //Scoreboard.IncrementTrickCount(TeamTwo);
                }

                RaiseOnAction();

                // All hands have been played out, time to check for a winner!
                if (PlayedCardsCounter >= 20)
                {
                    CheckRoundWinner();
                    return;
                } 

                // Else the game is still occuring - proceed to the next trick in the round.
                else
                {
                    MessageBox.Show("The next trick will begin!", "Next Trick");
                    TurnsTaken = 0;
                    TurnList = GameFunctionality.RotateToTrickWinner(TurnList, GameFunctionality.GetPlayerWithHighCard(PlayArea, TurnList));
                    PlayArea.Clear();
                    CurrentPlayer = TurnList[0];
                    if (CurrentPlayer.IsAI)
                    {
                        AIPlayCard(CurrentPlayer);
                    }
                }
            }
        }

        /// <summary>
        /// Checks the winner of the current round based on the number of tricks won by each team. Updates scores,
        /// logs the winner, and initiates the next round or ends the game if a team reaches the winning score.
        /// </summary>
        public void CheckRoundWinner()
        {
            string RoundWinner = "";

            // If team one won the round, then check if they were the maker's, and increment the points accordingly.
            if (TeamOneTricks >= 3)
            {
                RoundWinner = Team1.Name;

                // Check if team one has maker status, which will determine how many points they score for the round (3 for non-maker)
                if (Team1.MakerStatus == true)
                {
                    TeamOneScore++;
                }
                else
                {
                    TeamOneScore = TeamOneScore + 3;
                }
            }

            // Do the same for team two.
            else if (TeamTwoTricks >= 3)
            {
                RoundWinner = Team2.Name;

                if (Team2.MakerStatus == true)
                {
                    TeamTwoScore++;
                }
                else
                {
                    TeamTwoScore = TeamTwoScore + 3;
                }
            }
            RaiseOnAction();

            // Logging the winning team of the round.
            Logging.LogRoundWinner(RoundWinner);

            if (TeamOneScore >= 10 || TeamTwoScore >= 10)
            {
                if (RoundWinner == Team.TeamID.TeamOne.ToString()) // If the round winner is team one (human team)
                {
                    // Increment player one's rounds won stat
                    PlayerOneStats.PlayerWins++;

                    // Check if their last game result was a win, if so, increment their streak
                    if (PlayerOneStats.previousGameResult == Statistics.LastGameResult.Win)
                    {
                        PlayerOneStats.CurrentStreak++;
                    }
                    // else, their last game was a loss, the streak should be reset.
                    else
                    {
                        PlayerOneStats.CurrentStreak = 0;
                    }

                    // Update their previous game result
                    PlayerOneStats.previousGameResult = Statistics.LastGameResult.Win; 
                }

                // Player one has lost, do the same logic but for losses.
                else
                {
                    PlayerOneStats.PlayerLosses++;

                    if (PlayerOneStats.previousGameResult == Statistics.LastGameResult.Loss)
                    {
                        PlayerOneStats.CurrentStreak++;
                    }
                    else
                    {
                        PlayerOneStats.CurrentStreak = 0;
                    }

                    PlayerOneStats.previousGameResult = Statistics.LastGameResult.Loss;
                }

                CheckGameWinner();
                return;
            } 
            else
            {
                MessageBox.Show("Round Winner: " + RoundWinner + " \n\rNext round will begin when you click ok!", "Round Won");
                NewRound();
            }
        }

        /// <summary>
        /// Checks if a team has reached the winning score of 10 points and displays a message announcing the winning team. 
        /// Changes the game state to EndOfGame.
        /// </summary>
        public void CheckGameWinner()
        {
            // Determine which team has the win condition number of points (10) and provide them a winning message.
            if (TeamOneScore >= 10)
            {
                MessageBox.Show("Team 1 has won the game! \n\r" + PlayerOne.PlayerName + " and " + PlayerThree.PlayerName + " are the winners!", "Game Over");
            }
            else if (TeamTwoScore >= 10)
            {
                MessageBox.Show("Team 2 has won the game! \n\r" + PlayerTwo.PlayerName + " and " + PlayerFour.PlayerName + " are the winners!", "Game Over");
            }
            // Transition to end of game
            ChangeState(GameState.EndOfGame);
        }

        /// <summary>
        /// Creates a new deck (effectively shuffling the cards back into the deck), then performs the same functionality as start game.
        /// </summary>
        public void NewRound()
        {
            PlayedCardsCounter = 0;
            TurnsTaken = 0;

            // Recreate a new deck, since the old one is used up.
            Deck = new Deck();

            // Clear the players hands
            PlayerOne.PlayerHand.Cards.Clear();
            PlayerTwo.PlayerHand.Cards.Clear();
            PlayerThree.PlayerHand.Cards.Clear();
            PlayerFour.PlayerHand.Cards.Clear();

            // Reset Trick Scores
            TeamOneTricks = 0;
            TeamTwoTricks = 0;

            TurnList = GameFunctionality.ChangeDealer(GameFunctionality.RotateToDealer(TurnList)[0],
                                                      GameFunctionality.RotateToFirstTurn(TurnList)[0],
                                                      TurnList);
            CurrentPlayer = TurnList[0];

            // Deal new hands.
            GameFunctionality.DealCards(Deck, TurnList);

            // Clear the kitty, and re-determine a new kitty.
            Kitty.Clear();
            PlayArea.Clear();

            Kitty.Add(Deck.DetermineKitty());
            RaiseOnAction();
            ChangeState(GameState.TrumpSelectionFromKitty);
        }
        #endregion
    }
}
