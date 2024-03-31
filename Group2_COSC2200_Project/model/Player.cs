using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group2_COSC2200_Project.model
{

    /// <summary>
    /// Represents a player for the Euchre game.
    /// </summary>
    public class Player
    {

        // Getters / Setters ---------------------------------------------
        /// <summary>
        /// Gets or sets the player's ID.
        /// </summary>
        public int PlayerID { get; set; }

        /// <summary>
        /// Gets or sets the player's name.
        /// </summary>
        public string PlayerName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is a dealer.
        /// </summary>
        public bool IsDealer { get; set; }


        // Constructor
        public Player(int _playerID, string _playerName, bool _isDealer)
        {
            this.PlayerID = _playerID;
            this.PlayerName = _playerName;
            this.IsDealer = _isDealer;
        }

    }
}

