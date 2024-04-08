﻿using System.Windows;
using System.Windows.Markup;

namespace Group2_COSC2200_Project.model
{
    public class Game
    {
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
            TeamOneScore = 8;
            TeamTwoScore = 8;
        }

        public void Start()
        {
            CurrentState = GameState.Start;
            GameFunctionality.SetDealer(TurnList);
            GameFunctionality.DealCards(Deck, TurnList);
            Kitty.Add(Deck.DetermineKitty());
        }

        public void TrumpSelectionFromKitty() // HANDLES ONLY BUTTON ENABLING
        {
            CurrentState = GameState.TrumpSelectionFromKitty;
            TurnsTaken = 0;
            CurrentPlayer = TurnList[0];

            if (CurrentPlayer.isAI)
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
            CurrentPlayer = TurnList[0];
        }

        public void DealerKittySwap()
        {
            CurrentState = GameState.DealerKittySwap;
            TurnList = GameFunctionality.RotateToDealer(TurnList);
            CurrentPlayer = TurnList[0];
        }

        public void Play()
        {
            CurrentState = GameState.Play;
            TurnsTaken = 0;
            GameFunctionality.SetTrumpSuitValues(TrumpSuit, TurnList);
            TurnList = GameFunctionality.RotateToFirstTurn(TurnList);
            CurrentPlayer = TurnList[0];
        }

        // These handle what happens when ANY user clicks order up 
        public void OrderUpFromKitty()
        {
            TrumpSuit = Kitty[0].Suit;
            ChangeState(GameState.DealerKittySwap);
            // TODO :Set Maker status from the team of which the player that ordered up belongs to 
        }

        // These handle what happens when ANY user clicks pass
        public void PassFromKitty()
        {
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
            if (CurrentPlayer.isAI)
            {
                AIDecisionFromKitty(CurrentPlayer);
            }
        }

        public void OrderUpPostKitty(Card.Suits trumpSuit)
        {
            TrumpSuit = trumpSuit;
            ChangeState(GameState.Play);
        }

        public void PassPostKitty()
        {
            TurnsTaken++;
            if (TurnsTaken >= TurnList.Count)
            {
                MessageBox.Show("Dealer must pick a suit!");
                return;
            }
            GameFunctionality.NextTurn(TurnList);
            CurrentPlayer = TurnList[0];
        }

        public void SwapWithKitty(Player currentPlayer, Card card)
        {
            Card kittyCard = Kitty[0];
            Card playerCard = currentPlayer.PlayerHand.RemoveCard(card);
            Kitty.Remove(Kitty[0]);
            Kitty.Add(playerCard);
            currentPlayer.PlayerHand.AddCard(kittyCard);
            ChangeState(GameState.Play);
        }

        public void AIDecisionFromKitty(Player currentPlayer)
        {
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
                MessageBox.Show(CurrentPlayer.PlayerName + " has order it up!.");
                ChangeState(GameState.DealerKittySwap);
            }
            else
            {
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
                if (CurrentPlayer.isAI)
                {
                    AIDecisionFromKitty(CurrentPlayer);
                }
            }
        }

        public void AIDecisionPostKitty(Player currentPlayer)
        {
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
                MessageBox.Show(CurrentPlayer.PlayerName + " has order it up!.");
                ChangeState(GameState.DealerKittySwap);
            }
            else
            {
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
                if (CurrentPlayer.isAI)
                {
                    AIDecisionFromKitty(CurrentPlayer);
                }
            }
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
                // if it is the first card then set the lead suit
                LeadSuit = card.Suit;
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
                    if (playerCard.Suit == LeadSuit)
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
            // Increase counters for turns taken and played cards
            TurnsTaken++;
            PlayedCardsCounter++;
            // update the player to the next player in the turn list
            GameFunctionality.NextTurn(TurnList);
            CurrentPlayer = TurnList[0];
        }

        public void CheckTrickWinner()
        {
            if (TurnsTaken >= 4)
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

                TurnsTaken = 0;
                TurnList = GameFunctionality.RotateToTrickWinner(TurnList, GameFunctionality.GetPlayerWithHighCard(PlayArea, TurnList));
                CurrentPlayer = TurnList[0];
                PlayArea.Clear();

                if (PlayedCardsCounter >= 20)
                {
                    CheckRoundWinner();
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
            MessageBox.Show("Round Winner: " + RoundWinner + " Next Round will Begin when you click ok!");
            CheckGameWinner();
            NewRound();
        }

        public void CheckGameWinner()
        {
            if (TeamOneScore >= 10 || TeamTwoScore >= 10)
            {
                ChangeState(GameState.EndOfGame);
            }
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
            Kitty.Add(Deck.DetermineKitty());
            ChangeState(GameState.TrumpSelectionFromKitty);
        }

        public void EndOfGame()
        {
            CurrentState = GameState.EndOfGame;
            MessageBox.Show("10 Points Reached, End of Game.");
        }

    }
}
