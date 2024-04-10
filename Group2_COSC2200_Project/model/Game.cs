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
    /// Represents a game. 
    /// </summary>
    public class Game
    {
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
        public GameState CurrentState { get; private set; }
        public event EventHandler OnAction;
        public Player PlayerOne { get; private set; }
        public Player PlayerTwo { get; private set; }
        public Player PlayerThree { get; private set; }
        public Player PlayerFour { get; private set; }
        public Team Team1 { get; private set; }
        public Team Team2 { get; private set; }
        public Deck Deck { get; private set; }
        public List<Card> Kitty {  get; private set; }
        public List<Card.Suits> NonKittySuits { get; private set; }
        public List<Player> TurnList { get; private set; }
        public int TurnsTaken { get; private set; }
        public Player CurrentPlayer { get; private set; }
        public Card.Suits TrumpSuit { get; private set; }
        public Card.Suits LeadSuit { get; private set; }
        public List<Card> PlayArea { get; private set; }
        public int TeamOneTricks { get; private set; }
        public int TeamTwoTricks { get; private set; }
        public int PlayedCardsCounter { get; private set; }
        public int TeamOneScore { get; private set; }
        public int TeamTwoScore { get; private set; }

        public Game()
        {
            ChangeState(GameState.Initialize);
        }

        public void ChangeState(GameState state)
        {
            switch (state)
            {
                case GameState.Initialize:
                    Initialize();
                    break;
                case GameState.Start: 
                    Start(); 
                    break;
                case GameState.TrumpSelectionFromKitty:
                    TrumpSelectionFromKitty(); 
                    break;
                case GameState.TrumpSelectionPostKitty:
                    TrumpSelectionPostKitty();
                    break;
                case GameState.Play:
                    Play();
                    break;
                case GameState.EndOfGame: 
                    EndOfGame();
                    break;
                case GameState.DealerKittySwap:
                    DealerKittySwap();
                    break;
            }
        }

        public void Initialize()
        {
            CurrentState = GameState.Initialize;
            Deck = new Deck();
            Kitty = new List<Card>();
            PlayerOne = new Player(1, "Player 1", false);
            PlayerTwo = new Player(2, "Player 2", true);
            PlayerThree = new Player(3, "Player 3", true);
            PlayerFour = new Player(4, "Player 4", true);
            Team1 = new Team(Team.TeamID.TeamOne, Team.createTeam(PlayerOne, PlayerThree));
            Team2 = new Team(Team.TeamID.TeamTwo, Team.createTeam(PlayerTwo, PlayerFour));
            TurnList = GameFunctionality.CreateTurnList(Team1.TeamPlayers, Team2.TeamPlayers);
            PlayArea = new List<Card>();
            TurnsTaken = 0;
            TeamOneTricks = 0;
            TeamTwoTricks = 0;
            PlayedCardsCounter = 0;
            TeamOneScore = 9;
            TeamTwoScore = 9;
        }

        protected virtual void RaiseOnAction()
        {
            OnAction?.Invoke(this, EventArgs.Empty);
        }

        public void Start()
        {
            CurrentState = GameState.Start;
            GameFunctionality.SetDealer(TurnList);
            GameFunctionality.DealCards(Deck, TurnList);
            Kitty.Add(Deck.DetermineKitty());
        }

        public void TrumpSelectionFromKitty() 
        {
            CurrentState = GameState.TrumpSelectionFromKitty;
            TurnsTaken = 0;
            CurrentPlayer = TurnList[0];
            RaiseOnAction();
            MessageBox.Show("The Dealer is " + TurnList[3].PlayerName + " and the current top kitty suit is "
                + Kitty[0].Suit + ". " + CurrentPlayer.PlayerName + " is up.");
            if (CurrentPlayer.IsAI)
            {
                AIDecisionFromKitty(CurrentPlayer);
            } 
        }

        public void TrumpSelectionPostKitty()
        {
            CurrentState = GameState.TrumpSelectionPostKitty;
            TurnsTaken = 0;
            NonKittySuits = GameFunctionality.SetNonKittySuits(Kitty[0]);
            TurnList = GameFunctionality.RotateToFirstTurn(TurnList);
            Kitty.Clear();
            CurrentPlayer = TurnList[0];
            RaiseOnAction();
            MessageBox.Show("No one ordered it up. "+ CurrentPlayer.PlayerName + " is up.");
            if (CurrentPlayer.IsAI)
            {
                AIDecisionPostKitty(CurrentPlayer);
            }
        }

        public void DealerKittySwap()
        {
            CurrentState = GameState.DealerKittySwap;
            TurnList = GameFunctionality.RotateToDealer(TurnList);
            CurrentPlayer = TurnList[0];
            RaiseOnAction();
            MessageBox.Show(CurrentPlayer.PlayerName +  "can swap one of their cards with the top kitty card.");
            if (CurrentPlayer.IsAI)
            {
                AISwapWithKitty(CurrentPlayer);
            }
        }

        public void Play()
        {
            CurrentState = GameState.Play;
            TurnsTaken = 0;
            GameFunctionality.SetTrumpSuitValues(TrumpSuit, TurnList);
            TurnList = GameFunctionality.RotateToFirstTurn(TurnList);
            CurrentPlayer = TurnList[0];
            RaiseOnAction();
            MessageBox.Show(CurrentPlayer.PlayerName + " will start the round.");
            if (CurrentPlayer.IsAI)
            {
                AIPlayCard(CurrentPlayer);
            }
        }

        // These handle what happens when ANY user clicks order up 
        public void OrderUpFromKitty()
        {
            TrumpSuit = Kitty[0].Suit;
            RaiseOnAction();
            MessageBox.Show(CurrentPlayer.PlayerName + " has ordered it up!");
            // TODO :Set Maker status from the team of which the player that ordered up belongs to 
        }

        // These handle what happens when ANY user clicks pass
        public void PassFromKitty()
        {
            RaiseOnAction();
            MessageBox.Show(CurrentPlayer.PlayerName + " has passed.");
            TurnsTaken++;
            if (TurnsTaken >= TurnList.Count)
            {
                ChangeState(GameState.TrumpSelectionPostKitty);
                return;
            }
            // Change turns 
            GameFunctionality.NextTurn(TurnList);

            // RESET the current player to the new player's whose turn it is
            CurrentPlayer = TurnList[0];
            if (CurrentPlayer.IsAI)
            {
                AIDecisionFromKitty(CurrentPlayer);
            }
        }

        public void OrderUpPostKitty(Card.Suits trumpSuit)
        {
            TrumpSuit = trumpSuit;
            RaiseOnAction();
            MessageBox.Show(CurrentPlayer.PlayerName + " has chosen " + TrumpSuit + ".");
        }

        public void PassPostKitty()
        {
            RaiseOnAction();
            TurnsTaken++;
            if (TurnsTaken >= TurnList.Count)
            {
                MessageBox.Show("Dealer must pick a suit!");
                return;
            }
            MessageBox.Show(CurrentPlayer.PlayerName + " has passed.");
            GameFunctionality.NextTurn(TurnList);
            CurrentPlayer = TurnList[0];
            if (CurrentPlayer.IsAI) 
            {
                AIDecisionPostKitty(CurrentPlayer);
            }
        }

        public void SwapWithKitty(Player currentPlayer, Card card)
        {
            Card kittyCard = Kitty[0];
            currentPlayer.PlayerHand.RemoveCard(card);
            Kitty.Remove(Kitty[0]);
            currentPlayer.PlayerHand.AddCard(kittyCard);
            kittyCard.CardsAssociatedToPlayers = currentPlayer.PlayerID;
            RaiseOnAction();
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
                    MessageBox.Show("The suit lead was: " + LeadSuit + ". You must play a card of that suit if you have one");
                    return;
                }
            }

            // If the play is valid
            // Add the card to the playing area and remove form the player's hand
            PlayArea.Add(currentPlayer.PlayerHand.RemoveCard(card));
            RaiseOnAction();
            MessageBox.Show(CurrentPlayer.PlayerName + " has played their card.");
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

        public void AIDecisionFromKitty(Player currentPlayer)
        {
            TurnsTaken++;

            int sameSuitCount = 0;
            foreach (Card card in currentPlayer.PlayerHand.Cards)
            {
                if (card.Suit == Kitty[0].Suit)
                {
                    sameSuitCount++;
                }
            }
            if (sameSuitCount >= 3)
            {
                TrumpSuit = Kitty[0].Suit;
                RaiseOnAction();
                MessageBox.Show(CurrentPlayer.PlayerName + " has ordered it up!.");
                ChangeState(GameState.DealerKittySwap);
            }
            else
            {
                RaiseOnAction();
                MessageBox.Show(CurrentPlayer.PlayerName + " has passed.");
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

        public void AIDecisionPostKitty(Player currentPlayer)
        {
            TurnsTaken++;

            int[] suitCounts = new int[NonKittySuits.Count];

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

            if (TurnsTaken < TurnList.Count) // Not the dealer
            {
                bool trumpPicked = false;

                for (int i = 0; i < NonKittySuits.Count; i++)
                {
                    if (suitCounts[i] >= 3)
                    {
                        trumpPicked = true;
                        TrumpSuit = NonKittySuits[i];
                        RaiseOnAction();
                        MessageBox.Show(CurrentPlayer.PlayerName + " has chosen " + TrumpSuit + ".");
                        ChangeState(GameState.Play);
                        return;
                    }
                }
                if (!trumpPicked)
                {
                    RaiseOnAction();
                    MessageBox.Show(CurrentPlayer.PlayerName + " has passed.");
                    GameFunctionality.NextTurn(TurnList);
                    CurrentPlayer = TurnList[0];
                    if (CurrentPlayer.IsAI)
                    {
                        AIDecisionPostKitty(CurrentPlayer);
                    }
                }

            } 
            else // Dealer must go
            {
                int highestCounterIndex = 0;

                for (int i = 0; i < NonKittySuits.Count; i++)
                {
                    if (suitCounts[i] > highestCounterIndex)
                    {
                        highestCounterIndex = i;
                    }
                }
                TrumpSuit = NonKittySuits[highestCounterIndex];
                RaiseOnAction();
                MessageBox.Show(CurrentPlayer.PlayerName + " has chosen " + TrumpSuit + ".");
                ChangeState(GameState.Play);
            }
        }

        public void AISwapWithKitty(Player currentPlayer)
        {
            Card kittyCard = Kitty[0];
            Card cardToSwap = null;
            int sameSuitCount = 0;
            foreach (Card card in currentPlayer.PlayerHand.Cards)
            {
                if (card.Suit == kittyCard.Suit)
                {
                    sameSuitCount++;
                }
            }
            if (sameSuitCount >= 2)
            {
                foreach (Card card in currentPlayer.PlayerHand.Cards)
                {
                    if (card.Suit != kittyCard.Suit || !(card.Colour == kittyCard.Colour && card.Rank == Card.Ranks.Jack))
                    {
                        cardToSwap = card;
                        break;
                    }
                }
            }
            else
            {
                cardToSwap = currentPlayer.PlayerHand.Cards[0];
                foreach (Card card in currentPlayer.PlayerHand.Cards)
                {
                    if (card.Value < cardToSwap.Value)
                    {
                        cardToSwap = card;
                    }
                }
            }
            currentPlayer.PlayerHand.RemoveCard(cardToSwap);
            Kitty.Remove(Kitty[0]);
            currentPlayer.PlayerHand.AddCard(kittyCard);
            kittyCard.CardsAssociatedToPlayers = currentPlayer.PlayerID;
            RaiseOnAction();
            MessageBox.Show(CurrentPlayer.PlayerName + " has swapped their card.");
            ChangeState(GameState.Play);
        }

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
            PlayArea.Add(currentPlayer.PlayerHand.RemoveCard(cardToPlay));
            RaiseOnAction();
            MessageBox.Show(CurrentPlayer.PlayerName + " has played their card.");

            TurnsTaken++;
            PlayedCardsCounter++;

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

        public void CheckTrickWinner()
        {
            if (TurnsTaken >= TurnList.Count)
            {
                Team winningTeam = Trick.DetermineTrickWinner(PlayArea, Team1, Team2);
                MessageBox.Show("Trick Winners: " + winningTeam.TeamId.ToString());

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

                if (PlayedCardsCounter >= 20)
                {
                    CheckRoundWinner();
                    return;
                } 
                else
                {
                    MessageBox.Show("The next trick will begin!");
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

        public void CheckRoundWinner()
        {
            string RoundWinner = "";

            if (TeamOneTricks >= 3)
            {
                RoundWinner = Team.TeamID.TeamOne.ToString();

                /*if (TeamOne.MakerStatus == true)
                {
                    TeamOneScore++;
                }
                else
                {
                    TeamOneScore = TeamOneScore + 3;
                }*/

                TeamOneScore++;
            }
            else if (TeamTwoTricks >= 3)
            {
                RoundWinner = Team.TeamID.TeamTwo.ToString();

                /*if (TeamTwo.MakerStatus == true)
                {
                    TeamTwoScore++;
                }
                else
                {
                    TeamTwoScore = TeamTwoScore + 3;
                }*/

                TeamTwoScore++;
            }
            RaiseOnAction();
            MessageBox.Show("Round Winner: " + RoundWinner + " Next Round will Begin when you click ok!");
            if (TeamOneScore >= 10 || TeamTwoScore >= 10)
            {
                CheckGameWinner();
                return;
            } 
            else
            {
                NewRound();
            }
        }

        public void CheckGameWinner()
        {
            if (TeamOneScore >= 10)
            {
                MessageBox.Show("Team 1 has won the game! " + PlayerOne.PlayerName + " and " + PlayerThree.PlayerName + " are the winners!");
            }
            else if (TeamTwoScore >= 10)
            {
                MessageBox.Show("Team 2 has won the game! " + PlayerTwo.PlayerName + " and " + PlayerFour.PlayerName + " are the winners!");
            }
            ChangeState(GameState.EndOfGame);
        }

        //added (brody)
        // Creates a new deck (effectively shuffling the cards back into the deck), then performs the same functionality as start game.
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

        public void EndOfGame()
        {
            CurrentState = GameState.EndOfGame;
            PlayerOne.PlayerHand.Cards.Clear();
            PlayerTwo.PlayerHand.Cards.Clear();
            PlayerThree.PlayerHand.Cards.Clear();
            PlayerFour.PlayerHand.Cards.Clear();
            Kitty.Clear();
            PlayArea.Clear();
        }

    }
}
