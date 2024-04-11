/// <file>
///   <summary>
///     File Name: Logging.cs
///   </summary>
///   <author>
///     Authors: Brody Dentinger, Megan Clarke, Colin Eade, Muhammad Yasir Patel
///   </author>
///   <created>
///     Created: April 10, 2024
///   </created>
///   <lastModified>
///     Last Modified: April 10, 2024
///   </lastModified>
///   <description>
///     Description: This class represents general logic for logging various items to an external text file.
///   </description>
/// </file>

using System.IO;

namespace Group2_COSC2200_Project.model
{
    /// <summary>
    /// Represents general logic for logging to an external text file.
    /// </summary>
    public static class Logging
    {
        /// <summary>
        /// The relative path to our log file.
        /// </summary>
        private static readonly string LogFilePath = "../../game_log.txt";

        /// <summary>
        /// A static method to log the start time of a game.
        /// </summary>
        /// <param name="startTime"> The time to be logged. </param>
        public static void LogStartGame(DateTime startTime)
        {
            // Try to open the ext file, and write the current start date assosiated with game starting (Deal button)
            try
            {
                using (StreamWriter writer = new StreamWriter(LogFilePath, true))
                {
                    writer.WriteLine("--------------------------------------------------------");
                    writer.WriteLine($"Game started at: {startTime}");
                    writer.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging start time: {ex.Message}");
            }
        }

        /// <summary>
        /// A static method to log the hands dealt.
        /// </summary>
        /// <param name="startTime"></param>
        public static void LogPlayerHandsDealt(Hand player1Hand, Hand player2Hand,
                                        Hand player3Hand, Hand player4Hand)
        {
            // Try to write to the log file, with the time, as well as the current players for each player after the hands were dealt.
            try
            {
                using (StreamWriter writer = new StreamWriter(LogFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now}: Player hands dealt:");
                    writer.WriteLine($"Player 1 Hand: {string.Join(", ", player1Hand.Cards)}");
                    writer.WriteLine($"Player 2 Hand: {string.Join(", ", player2Hand.Cards)}");
                    writer.WriteLine($"Player 3 Hand: {string.Join(", ", player3Hand.Cards)}");
                    writer.WriteLine($"Player 4 Hand: {string.Join(", ", player4Hand.Cards)}");
                    writer.WriteLine(); 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging player hands dealt: {ex.Message}");
            }
        }

        /// <summary>
        /// Trump suit logging.
        /// </summary>
        /// <param name="message"> The message to be logged. </param>
        public static void LogTrumpSuit(string message)
        {
            // Try to write to the log file the current time, as well as the suit that was selected as trump.
            // To be done on every AI/Player trump selection phase.
            try
            {
                using (StreamWriter writer = new StreamWriter(LogFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now}: Trump Suit Selected: {message}");
                    writer.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging game state: {ex.Message}");
            }
        }

        /// <summary>
        /// For Logging played cards
        /// </summary>
        /// <param name="playerName"> The played who played the card </param>
        /// <param name="card"> The card that was played. </param>
        public static void LogPlayedCard(Player player, Card card)
        {
            // Try to open and write to the log file for the current time, as well as the player and the card they played on their turn.
            try
            {
                string logMessage = $"{DateTime.Now}: {player.PlayerName} played {card.ToString()}";

                using (StreamWriter writer = new StreamWriter(LogFilePath, true))
                {
                    writer.WriteLine(logMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging played card: {ex.Message}");
            }
        }

        /// <summary>
        /// For logging trick winner.
        /// </summary>
        /// <param name="winner"></param>
        public static void LogTrickWinner(string winner)
        {
            // Try to log to the text file the date and time and the winner of the trick.
            try
            {
                string logMessage = $"{DateTime.Now}: Trick Winner - {winner}";

                using (StreamWriter writer = new StreamWriter(LogFilePath, true))
                {
                    writer.WriteLine();
                    writer.WriteLine(logMessage);
                    writer.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging trick winner: {ex.Message}");
            }
        }

        /// <summary>
        /// To log the round winner.
        /// </summary>
        /// <param name="winner"> The winning team </param>
        public static void LogRoundWinner(string winner)
        {
            // Try to log to the text file the date time and the winner of the round.
            // To take place at the end of every round (>= 10 points)
            try
            {
                string logMessage = $"{DateTime.Now}: Round Winner - {winner}";

                using (StreamWriter writer = new StreamWriter(LogFilePath, true))
                {
                    writer.WriteLine();
                    writer.WriteLine(logMessage);
                    writer.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging round winner: {ex.Message}");
            }
        }


    }
}
