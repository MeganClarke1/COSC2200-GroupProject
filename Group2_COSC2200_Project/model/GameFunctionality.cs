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
    /// Contains the core functionality and logic specific to managing the flow and rules of a card game. This includes
    /// operations such as dealing cards, determining turn order, evaluating the outcome of each trick, and managing 
    /// game state transitions such as selecting trump suits, rotating dealer responsibilities, and handling card swaps. 
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
        /// Generates a list of card suits excluding the suit of a the kitty card. This is used in game phases where 
        /// the trump suit is being selected after the first rotation of selecting it from the Kitty.
        /// </summary>
        /// <param name="kittyCard">The card whose suit is to be excluded from the list of potential suits.</param>
        /// <returns>A list of Card.Suits representing all suits except the one of the specified kitty card.</returns>
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
        /// Rotates the turn order of players to ensure the dealer is positioned at the beginning of the list.
        /// </summary>
        /// <param name="TurnList">The current list of players in turn order.</param>
        /// <returns>The modified list of players with the dealer rotated to the first position.</returns>
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
        /// Adjusts the turn order so that the game starts with the player immediately following the dealer.
        /// </summary>
        /// <param name="TurnList">The list of players in their current turn order.</param>
        /// <returns>The adjusted list of players with the dealer rotated to the last position.</returns>
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
        /// trump suit. This is used to determine who wins a hand.
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
        /// or every card in the play area determine the high card, placing the high card and the player who owns it in the 
        /// current highest for both containers. Returns player who owns the winning card.
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
        /// Determines this via manipulation of the turnlist by setting the winning player first in that list.
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
        /// Player.dealerStatus, and updating the new dealer's to true.
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
