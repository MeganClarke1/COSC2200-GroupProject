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
            Deal,
            TrumpSelectionFromKitty,
            TrumpSelectionPostKitty,
            Round,
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
        public List<Player> TurnList { get; private set; }
        public Player CurrentPlayer { get; private set; }
        public Card.Suits TrumpSuit { get; private set; }
        public Card.Suits LeadSuit { get; private set; }
        public List<Card> PlayArea { get; private set; }

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
                case GameState.Deal:
                    //Deal();
                    break;
                case GameState.TrumpSelectionFromKitty:
                    TrumpSelectionFromKitty(); 
                    break;
                case GameState.TrumpSelectionPostKitty:
                    //TrumpSelectionPostKitty();
                    break;
                case GameState.Round: //added (brody)
                    NewRound();
                    break;
                case GameState.EndOfGame: 
                    EndOfGame();
                    break;
            }
        }

        public void Initialize()
        {
            CurrentState = GameState.Initialize;

            Deck = new Deck();
            Kitty = new List<Card>();
            PlayerOne = (new Player(1, "Player 1"));
            PlayerTwo = (new Player(2, "Player 2"));
            PlayerThree = (new Player(3, "Player 3"));
            PlayerFour = (new Player(4, "Player 4"));
            Team1 = new Team(Team.TeamID.TeamOne, Team.createTeam(PlayerOne, PlayerThree));
            Team2 = new Team(Team.TeamID.TeamTwo, Team.createTeam(PlayerTwo, PlayerFour));
            TurnList = GameFunctionality.CreateTurnList(Team1.TeamPlayers, Team2.TeamPlayers);
            PlayArea = new List<Card>();
        }

        public void Start()
        {
            CurrentState = GameState.Start;

            GameFunctionality.DealCards(Deck, TurnList);
            Kitty.Add(Deck.DetermineKitty());
        }

        public void TrumpSelectionFromKitty() // HANDLES ONLY BUTTON ENABLING
        {
            CurrentState = GameState.TrumpSelectionFromKitty;

            MessageBox.Show("Current Kitty " + TrumpSuit.ToString());
            CurrentPlayer = TurnList[0];
        }

        // These handle what happens when ANY user clicks order up 
        public void OrderUp()
        {
            // Set trump suit property to the Kitty's suit value.
            TrumpSuit = Kitty[0].Suit;

            // TODO :Set Maker status from the team of which the player that ordered up belongs to 
        }

        // These handle what happens when ANY user clicks pass
        public void Pass()
        {
            // Change turns 
            GameFunctionality.NextTurn(TurnList);

            // RESET the current player to the new player's whose turn it is
            CurrentPlayer = TurnList[0];

            // Present the message to the player that its their turn
            MessageBox.Show("Your Turn: " + CurrentPlayer.PlayerName);

        }

        /// TEST FUNCTION - CAN DELETE LATER *************
        public void AddCardtoPlayAreaTest(Card CardToAdd)
        {
            PlayArea.Add(CardToAdd);
        }

        //added (brody)
        // Creates a new deck (effectively shuffling the cards back into the deck), then performs the same functionality as start game.
        public void NewRound()
        {   
            
            // Set the state to Round
            CurrentState = GameState.Round;

            // Recreate a new deck, since the old one is used up.
            Deck newDeck = new Deck();

            // Clear the players hands
            PlayerOne.PlayerHand.Cards.Clear();
            PlayerTwo.PlayerHand.Cards.Clear();
            PlayerThree.PlayerHand.Cards.Clear();
            PlayerFour.PlayerHand.Cards.Clear();

            // Deal new hands.
            GameFunctionality.DealCards(newDeck, TurnList);

            // Clear the kitty, and re-determine a new kitty.
            Kitty.Clear();
            Kitty.Add(newDeck.DetermineKitty());

        }

        public void EndOfGame()
        {
            CurrentState = GameState.EndOfGame;
            MessageBox.Show("10 Points Reached, End of Game.");
        }

    }
}
