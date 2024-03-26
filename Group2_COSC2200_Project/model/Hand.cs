namespace Group2_COSC2200_Project.model
{
    /// <summary>
    /// Represents a hand in the Euchre game, including the list of cards in the hand and the playerID
    /// associated with the hand.
    /// </summary>
    public class Hand
    {


        // Attributes

        /// <summary>
        /// The list of card object in the hand
        /// </summary>
        private List<Card> cards;

        /// <summary>
        /// The player ID associated with the hand
        /// </summary>
        private int playerId;


        // Constructor
        public Hand(int playerId)
        {
            this.cards = new List<Card>();
            this.playerId = playerId;
        }


        public int PlayerId
        {
            get { return playerId; }
            set { playerId = value; }
        }

        public void AddCard(Card card)
        {
            cards.Add(card);
        }

        public bool RemoveCard(Card card)
        {
            return cards.Remove(card);
        }

        public List<Card> Cards
        {
            get { return cards; }
        }
    }

}
