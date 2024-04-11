/// <file>
///   <summary>
///     File Name: GameFunctionality.cs
///   </summary>
///   <author>
///     Authors: Brody Dentinger, Megan Clarke, Colin Eade, Muhammad Yasir Patel
///   </author>
///   <created>
///     Created: March 24, 2024
///   </created>
///   <lastModified>
///     Last Modified: April 10, 2024
///   </lastModified>
///   <description>
///     Description: This class represents the game logic for the game.
///   </description>
/// </file>

namespace Group2_COSC2200_Project.model
{
    /// <summary>
    /// Represents the general game logic for the game.
    /// </summary>
    public class GameFunctionality
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
            // For each player in th the turn list
            foreach (Player player in turnList)
            {
                // For each card of the player's hands
                foreach (Card card in player.PlayerHand.Cards)
                {
                    // Set the card value based on the Trump suit.
                    card.SetTrumpSuitValue(trumpSuit);
                }
            }
        }

        /// <summary>
        /// Loops through the turnlist to access each players hands, and sets the value of their cards based on the lead suit and 
        ///     trump suit. This is used to determine who wins a hand.
        /// </summary>
        /// <param name="leadSuit"> The current lead suit </param>
        /// <param name="trumpSuit"> The current trump suit </param>
        /// <param name="turnList"> The current turnlist </param>
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

        /// <summary>
        /// Used to get the player with the high card out of all played cards in the play area.
        ///     for every card in the play area determine the high card, placing the high card and the player who owns it in the 
        ///     current highest for both containers. Returns player who owns the winning card.
        /// </summary>
        /// <param name="playArea"> The current play area </param>
        /// <param name="turnList"> The current Turn List </param>
        /// <returns></returns>
        public static Player GetPlayerWithHighCard(List<Card> playArea, List<Player> turnList)
        {
            // Containers used to compare values against. Will end up with highest card and player who it belongs to.
            Card highCard = null;
            Player playerWithHighCard = null;

            // for each card in the play area
            for (int i = 0; i < playArea.Count; i++)
            {
                // if the card is higher value, replace that value
                if (highCard == null || playArea[i].Value > highCard.Value)
                {
                    highCard = playArea[i];
                }
            }

            // Also, update the player who it belongs to.
            foreach (Player player in turnList)
            {
                if (player.PlayerID == highCard.CardsAssociatedToPlayers)
                {
                    playerWithHighCard = player;
                    break;
                }
            }
            return playerWithHighCard;
        }

        /// <summary>
        /// Once a trick ends, the person who won that trick starts the next trick.
        ///     Determines this via manipulation of the turnlist by setting the winning player first in that list.
        /// </summary>
        /// <param name="turnList"> The current turn list </param>
        /// <param name="winningPlayer"> The current winning player. </param>
        /// <returns></returns>
        public static List<Player> RotateToTrickWinner(List<Player> turnList, Player winningPlayer)
        {
            while (turnList[0] != winningPlayer)
            {
                turnList = NextTurn(turnList);
            }
            return turnList;
        }

        /// <summary>
        /// Used to change the dealer status. Determined by accessing the current dealer in the turn list, changing their 
        ///     Player.dealerStatus, and updating the new dealer's to true.
        /// </summary>
        /// <param name="currentDealer"> the current dealer </param>
        /// <param name="newDealer"> The new dealer </param>
        /// <param name="turnList"> the turnlist </param>
        /// <returns></returns>
        public static List<Player> ChangeDealer(Player currentDealer, Player newDealer, List<Player> turnList)
        {
            // If the current dealer 
            if (currentDealer.IsDealer)
            {
                // Change it to false
                currentDealer.IsDealer = false;
            }
            // If not
            if (!newDealer.IsDealer)
            {
                // Set it to true
                newDealer.IsDealer = true;
            }
            return RotateToFirstTurn(turnList);
        }
    }
}
