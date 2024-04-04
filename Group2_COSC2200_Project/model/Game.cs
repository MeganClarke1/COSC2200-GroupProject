using Group2_COSC2200_Project.view;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using static Group2_COSC2200_Project.model.Card;

namespace Group2_COSC2200_Project.model
{
    public class Game
    {

        public Player PlayerOne { get; private set; }
        public Player PlayerTwo { get; private set; }
        public Player PlayerThree { get; private set; }
        public Player PlayerFour { get; private set; }
        public Team Team1 { get; private set; }
        public Team Team2 { get; private set; }
        public Deck Deck { get; private set; }
        public List<Card> Kitty {  get; private set; }
        public List<Player> TurnList { get; private set; }
        public Card.Suits TrumpSuit { get; private set; }
        public Player CurrentPlayer { get; private set; }
        public bool trumpFromKitty { get; private set; }
        public List<Card> PlayArea { get; private set; }

        public Game()
        {
            Deck = new Deck();
            Kitty = new List<Card>();
            PlayerOne = (new Player(1, "Player 1"));
            PlayerTwo = (new Player(2, "Player 2"));
            PlayerThree = (new Player(3, "Player 3"));
            PlayerFour = (new Player(4, "Player 4"));
            Team1 = new Team(Team.TeamID.TeamOne, Team.createTeam(PlayerOne, PlayerThree));
            Team2 = new Team(Team.TeamID.TeamTwo, Team.createTeam(PlayerTwo, PlayerFour));
            TurnList = GameFunctionality.CreateTurnList(Team1.TeamPlayers, Team2.TeamPlayers);
        }

        public void Start()
        {
            GameFunctionality.DealCards(Deck, TurnList);
            Kitty.Add(Deck.DetermineKitty());
            TrumpSelection();
        }

        public void TrumpSelection() // HANDLES ONLY BUTTON ENABLING
        {
            CurrentPlayer = TurnList[0];
            trumpFromKitty = true;
        }

        // These handle what happens when ANY user clicks order up 
        public void OrderUp()
        {
            // Change Boolean TrumpSelected to True so no more players are asked / have their buttons enabled
            trumpFromKitty = false;

            // Set trump suit property to the Kitty's suit value.
            TrumpSuit = Kitty[0].Suit;

            // TODO :Set Maker status from the team of which the player that ordered up belongs to 

            // Disable all buttons
            MessageBox.Show("Current Kitty " + TrumpSuit.ToString());

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

        public void PlayCard(int idx)
        {
            Card card = CurrentPlayer.PlayerHand.Cards[idx];
            CurrentPlayer.PlayerHand.Cards.RemoveAt(idx);
            PlayArea.Add(card);
        }
    }
}
