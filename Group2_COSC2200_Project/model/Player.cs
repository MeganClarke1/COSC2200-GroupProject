namespace Group2_COSC2200_Project.model
{
    /// <summary>
    /// Represents a player for the Euchre game.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Gets or sets the player's ID.
        /// </summary>
        public int PlayerID { get; set; }

        /// <summary>
        /// Gets or sets the player's name.
        /// </summary>
        public string PlayerName { get; set; }

        /// <summary>
        /// Gets and sets the player's hand.
        /// </summary>
        public Hand PlayerHand { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is a dealer.
        /// </summary>
        public bool IsDealer { get; set; }

        /// <summary>
        /// Constructor for a Player Object. 
        /// _isDealer can be set to false until Team and TurnList Creation (Game Functionality)
        /// </summary>
        public Player(int _playerID, string _playerName)
        {
            PlayerID = _playerID;
            PlayerName = _playerName;
            PlayerHand = new Hand();
            IsDealer = false;
        }
    }
}
