using System.Net.Security;

namespace Group2_COSC2200_Project.model
{
    /// <summary>
    /// Represents the general game logic for the game.
    /// </summary>
    class GameFunctionality
    {
        /// <summary>
        /// Static method to merge 2 team lists into one turn list for game functionality construction
        /// Takes 2 lists of players (teams), and merges them into 1 turnlist, where player turns alternate between teams.
        /// </summary>
        /// <returns> TurnList a list of player Objects indicating turn order. </returns>
        public static List<Player> CreateTurnList(List<Player> TeamOne, List<Player> TeamTwo)
        {
            // Create a turnList
            List<Player> TurnList = new List<Player>();

            // Alternate adding a member from each team to create staggered turn order.
            TurnList.Add(TeamOne[0]);
            TurnList.Add(TeamTwo[0]);
            TurnList.Add(TeamOne[1]);
            TurnList.Add(TeamTwo[1]);

            return TurnList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kittyCard"></param>
        /// <returns></returns>
        public static List<Card.Suits> SetNonKittySuits(Card kittyCard)
        {
            List<Card.Suits> nonKittySuits = new List<Card.Suits>();

            foreach (Card.Suits suit in Enum.GetValues(typeof(Card.Suits)))
            {
                if (suit != kittyCard.Suit)
                {
                    nonKittySuits.Add(suit);
                }
            }
            return nonKittySuits;
        }

        /// <summary>
        /// Takes a TurnList, moves the first player in the list, to the end of the list.
        /// Returns a list of players where the first player is the current turn
        /// </summary>
        /// <param name="TurnList"></param>
        /// <returns> TurnList an updated list of players where the player who just had their turn is moved to the end.</returns>
        public static List<Player> NextTurn(List<Player> TurnList)
        {
            // Fetch the current player whose turn it is
            Player CurrentTurnPlayer = TurnList[0];

            // Remove the first index position (Current player whose turn it is)
            TurnList.RemoveAt(0);

            // Re-add the removed player to the end of the list.
            // Effectively, removing them from the front, and adding them to the back of the list.
            TurnList.Add(CurrentTurnPlayer);

            return TurnList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TurnList"></param>
        /// <returns></returns>
        public static List<Player> RotateToDealer(List<Player> TurnList)
        {
            if (TurnList[0].IsDealer)
            {
                return TurnList;
            }
            while (!TurnList[0].IsDealer)
            {
                NextTurn(TurnList);
            }
            return TurnList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TurnList"></param>
        /// <returns></returns>
        public static List<Player> RotateToFirstTurn(List<Player> TurnList)
        {
            if (TurnList[TurnList.Count - 1].IsDealer)
            {
                return TurnList;
            }
            while (!TurnList[TurnList.Count - 1].IsDealer)
            {
                NextTurn(TurnList);
            }

            return TurnList;
        }


        /// <summary>
        /// Will set the isDealer Status of the player in the last position of the turnList to dealer.
        /// </summary>
        /// <param name="TurnList">a TurnList</param>
        /// <returns>TurnList a TurnList where the player at the end of the list is made dealer.</returns>
        public static List<Player> SetDealer(List<Player> TurnList)
        {

            // Fetch the last person in the turn list and make them dealer
            Player lastPlayer = TurnList[3];

            // Because the person to the left of the dealer goes first, this means the dealer goes last
            // Therefore, dealer can be determined by whichever player is going last.
            lastPlayer.IsDealer = true;

            return TurnList;
        }

        /// <summary>
        /// Creates a new ROUND object with the selected Trump Card.
        /// Takes the selected Trump, the current player who selected it, and the 2 participating teams.
        /// Checks both teams for the presence of that player object. If the player object is found in that team,
        /// then we know what team selected the Trump, and the rounds MakerStatus (Team object) is set.
        /// Also set the TrumpCard in round object.
        /// Return a new Round Object.
        /// </summary>
        /// <param name="selectedTrumpSuit"> The card Suit property that was chosen as Trump. </param>
        /// <param name="currentPlayer"> The player whose current turn it is (Who selected the trump) </param>
        /// <param name="TeamOne"> The first team participating in the game. </param>
        /// <param name="TeamTwo"> The second team participating in the game. </param>
        /// <returns> newRound a new Round object </returns>
        public static Round TrumpSelected(Card.Suits selectedTrumpSuit, Player currentPlayer, Team TeamOne, Team TeamTwo)
        {

            // Initialize a round object (Empty Values)
            Round newRound = new Round();

            // Loops through both team lists to check for which team selected the trump. Whichever team did, update the
            // Round.MakerStatus to THAT team
            foreach (Player player in TeamOne.TeamPlayers)
            {
                if (player == currentPlayer)
                {
                    newRound.MakerTeam = TeamOne;
                }
            }
            foreach (Player player in TeamTwo.TeamPlayers)
            {
                if (player == currentPlayer)
                {
                    newRound.MakerTeam = TeamTwo;
                }
            }

            // Regardless, update Round.TrumpSuit
            newRound.TrumpSuit = selectedTrumpSuit;

            return newRound;
        }


        /// <summary>
        /// Updates the value of each card in every player's hand based on the specified trump suit.
        /// </summary>
        /// <param name="trumpSuit">The suit that has been declared as trump for the current round.</param>
        /// <param name="turnList">A list of players participating in the current round, whose cards will be evaluated.</param>
        public static void SetTrumpSuitValues(Card.Suits trumpSuit, List<Player> turnList)
        {
            foreach (Player player in turnList)
            {
                foreach (Card card in player.PlayerHand.Cards)
                {
                    card.SetTrumpSuitValue(trumpSuit);
                }
            }
        }

        public static void SetLeadSuitValues(Card.Suits leadSuit, Card.Suits trumpSuit, List<Player> turnList)
        {
            foreach (Player player in turnList)
            {
                foreach (Card card in player.PlayerHand.Cards)
                {
                    card.SetLeadSuitValue(leadSuit, trumpSuit);
                }
            }
        }

        /// <summary>
        /// Deals cards to players in the specified turn order, following the order of 3 cards in a row to each
        /// player, then two cards in a row to each player.
        /// </summary>
        /// <param name="deck">The deck of cards to deal from.</param>
        /// <param name="turnList">The list of players in the turn order, representing who receives cards.</param>
        public static void DealCards(Deck deck, List<Player> turnList)
        {
            deck.Shuffle();
            foreach (Player player in turnList)
            {
                // Deal 3 cards in a row to each player
                for (int i = 0; i < 3; i++)
                {
                    player.PlayerHand.AddCard(deck.DealCard(player));
                }
            }
            foreach (Player player in turnList)
            {
                // Deal 2 cards in a row to each player
                for (int i = 0; i < 2; i++)
                {
                    player.PlayerHand.AddCard(deck.DealCard(player));
                }
            }
        }

        // public static Player GetPlayerWithHighCard(List<Card> playArea)
        // {
        //     Card highCard = null;
        // 
        //     foreach(Card card in playArea)
        //     {
        // 
        //     }
        // }

        /*// 2 Options: Loop through each player (bad performance) or, simply use the turnList to call the function in order of the turnlist
        // Eg. TurnList[0].PassOrOrderUp
        //     TurnList[1].PassOrOrderUp
        public static bool PassOrOrderUp(List<Player>turnList)
        {
            // Reference the property in game to see if we still need to ask for trump ( still false, meaning no one else has ordered up )
            if (! game.gameTrumpOrdered)
            {
                // Check the current player in the turnlist
                Player currentPlayer = turnList[0];

                // Enable buttons for currentPlayer 
                if (currentPlayer == playerOne) // playerOne should reference the player Object in Game properties
                {
                    // Enable player 1's buttons... Will need specific names on each button to reference

                    // Prompt that user
                    MessageBox.Show("Your Turn " + playerOne);
                }

            }
            else
            {
                // disable all buttons
            }
        }

        public static void playerOneOrder()
        {
            // change the gameTrumpOrder status to true
            Game.gameTrumpOrdered = true;

            // set trump suit property of game
            Game.TrumpSuit = Card trumpCard.suit;

            //disable all buttons
        }

        public static void playerOnePass()
        {

        }*/

    }
}
