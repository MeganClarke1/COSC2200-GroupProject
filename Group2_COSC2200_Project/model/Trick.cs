using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group2_COSC2200_Project.model
{
    /// <summary>
    /// A class representing 1 Trick. (Each player has went once)
    /// </summary>
    class Trick
    {
        /// <summary>
        /// A list of cards that have been played during the round.
        /// </summary>
        public List<Card> PlayedCards { get; private set; }

        ///<todo>Entire class. Ties closely with round, and game functionality. 
        ///Refer to all other made classes and flow Doc in teams.</todo>
    }
}
