using Group2_COSC2200_Project.view;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Group2_COSC2200_Project.model
{
    public class Game
    {

        public Player PlayerOne { get; private set; }
        public Player PlayerTwo { get; private set; }
        public Player PlayerThree { get; private set; }
        public Player PlayerFour { get; private set; }

        public List<Team> Teams { get; private set; }

        public Deck Deck { get; private set; }
        public List<Card> Kitty {  get; private set; }
        public List<Player> TurnList { get; private set; }
        public Team Team1 { get; private set; }
        public Team Team2 { get; private set; }

        

        public Game()
        {
            
            Teams = new List<Team>();
            Deck = new Deck();
            Kitty = new List<Card>();
        }

        public void Initialize()
        {
            PlayerOne = (new Player(1, "Player 1"));
            PlayerTwo = (new Player(2, "Player 2"));
            PlayerThree = (new Player(3, "Player 3"));
            PlayerFour = (new Player(4, "Player 4"));

            Team1 = new Team(Team.TeamID.TeamOne, Team.createTeam(PlayerOne, PlayerTwo));
            Team2 = new Team(Team.TeamID.TeamTwo, Team.createTeam(PlayerThree, PlayerFour));

            TurnList = GameFunctionality.CreateTurnList(Team1.TeamPlayers, Team2.TeamPlayers);

            GameFunctionality.DealCards(Deck, TurnList);
            Kitty.Add(Deck.DetermineKitty());

            TrumpSelection();

        }

        public void TrumpSelection()
        {
            // Player determination
            Player currentPlayer = TurnList[0];

            if(currentPlayer == PlayerOne)
            {
                GameView myUserControl = new GameView(); // create game view instance

                var button = myUserControl.FindName("Player1OrderUp") as Button; // fetch the player 1 order up button

                button.IsEnabled = true; // set it to true to enable button

                MessageBox.Show("Your turn " + currentPlayer);

            }

            // Enabling buttons here with player determination 

        }
    }
}
