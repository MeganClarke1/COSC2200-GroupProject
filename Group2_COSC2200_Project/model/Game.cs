using System.Windows;

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
            DealerKittySwap,
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
                    TrumpSelectionPostKitty();
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
            GameFunctionality.SetDealer(TurnList);
            GameFunctionality.DealCards(Deck, TurnList);
            Kitty.Add(Deck.DetermineKitty());
        }

        public void TrumpSelectionFromKitty() // HANDLES ONLY BUTTON ENABLING
        {
            CurrentState = GameState.TrumpSelectionFromKitty;
            MessageBox.Show("Current Kitty " + Kitty[0].Suit);
            TurnsTaken = 0;
            CurrentPlayer = TurnList[0];
        }

        // These handle what happens when ANY user clicks order up 
        public void OrderUpFromKitty()
        {
            TrumpSuit = Kitty[0].Suit;
            MessageBox.Show("Trump suit is " + TrumpSuit);
            ChangeState(GameState.DealerKittySwap);
            // TODO :Set Maker status from the team of which the player that ordered up belongs to 
        }

        // These handle what happens when ANY user clicks pass
        public void PassFromKitty()
        {
            TurnsTaken++;
            if (TurnsTaken >= TurnList.Count)
            {
                MessageBox.Show("Pick a trump suit.");
                ChangeState(GameState.TrumpSelectionPostKitty);
                return;
            }
            // Change turns 
            GameFunctionality.NextTurn(TurnList);

            // RESET the current player to the new player's whose turn it is
            CurrentPlayer = TurnList[0];

            // Present the message to the player that its their turn
            MessageBox.Show("Your Turn: " + CurrentPlayer.PlayerName);
        }

        public void TrumpSelectionPostKitty()
        {
            CurrentState = GameState.TrumpSelectionPostKitty;
            TurnsTaken = 0;
            NonKittySuits = GameFunctionality.SetNonKittySuits(Kitty[0]);
            TurnList = GameFunctionality.RotateToFirstTurn(TurnList);
            CurrentPlayer = TurnList[0];
        }

        public void OrderUpPostKitty(Card.Suits trumpSuit)
        {
            TrumpSuit = trumpSuit;
            MessageBox.Show(TrumpSuit.ToString());
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
            MessageBox.Show("Your Turn: " + CurrentPlayer.PlayerName);
        }

        public void DealerKittySwap()
        {
            CurrentState = GameState.DealerKittySwap;
            TurnList = GameFunctionality.RotateToDealer(TurnList);
            CurrentPlayer = TurnList[0];
            MessageBox.Show(CurrentPlayer.PlayerName + " gets to swap one of their cards with the kitty.");
        }

        /// TEST FUNCTION - CAN DELETE LATER *************
        public void AddCardtoPlayAreaTest(Card CardToAdd)
        {
            PlayArea.Add(CardToAdd);
        }

        public void SwapWithKitty(Player currentPlayer, Card card)
        {
            Card kittyCard = Kitty[0];
            Card playerCard = currentPlayer.PlayerHand.RemoveCard(card);
            Kitty.Remove(Kitty[0]);
            Kitty.Add(playerCard);
            currentPlayer.PlayerHand.AddCard(kittyCard);
        }
    }
}
