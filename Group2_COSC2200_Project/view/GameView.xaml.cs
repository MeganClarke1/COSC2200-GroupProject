using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Group2_COSC2200_Project.view
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : UserControl
    {

        public String rulesString =
            "Euchre Rules" +
            "\r\nThese are the rules used for this version of Euchre. There are many variations possible, " +
            "so this might not be the exact way you're used to playing. " +
            "\r\n\r\nOverview" +
            "\r\nEuchre is a trick taking game with a trump, " +
            "played by four players in teams of two. Each player plays one card, " +
            "the highest card of the suit led wins the trick, unless someone has played a card of the trump suit. " +
            "The game is played over several rounds until one team has gotten 10 points." +
            "\r\n\r\nDealing" +
            "\r\nEuchre uses a non standard deck of 24 cards. " +
            "It's made up of the 9, 10, Jack, Queen, King and Ace of each suit. " +
            "Five cards are dealt to each player in two rounds of dealing. Once all players have " +
            "their cards, the top card of the deck is turned face up, so it's ready for the next part of the game," +
            " which is..." +
            "\r\n\r\nNaming Trump (Calling Round)" +
            "\r\nAfter the cards are dealt the players must pick " +
            "what will be the trump suit. At this point there is one face up card on the table, (Kitty) the suit of that " +
            "card is the potential trump suit. Going clockwise around the table, each player can either \"Pass\", " +
            "meaning they don't want the suit to become trump," +
            " or they can \"Order it up\" in which case the suit of the card becomes trump. " +
            "The face up card on the table goes to the dealer which takes it and discards one of the " +
            "cards from his hand and then the game is ready to begin. The team that picked the trump are known as the " +
            "\"Makers\"." +
            "\r\n\r\nIf all players pass on the trump card " +
            "then there's another round of naming trumps, where a player may simply name which suit he wants to be trump " +
            "or say pass. If the first three players pass on " +
            "this round as well then the dealer is forced to name a trump. " +
            "\r\n\r\n" +
            "\r\n\r\nRanking of trump cards\r\n" +
            "The trump suit ranks higher than the other suits, " +
            "but within the trump suit the Jack (known as the Right Bower) is the highest card. " +
            "Then, the Jack in the other suit that's the same color as the trump " +
            "is the next best trump card. E.g. if spades are trump then the Jack of clubs would be the next best card, " +
            "known as the Left Bower). After that the rest of the trump cards follow in order from high to low, Ace, King, " +
            "Queen, 10, 9. The Left Bower is considered for all purposes as a member of the trump suit. Just to make it " +
            "clearer, if trump suit was Hearts, the ranking of trump cards would be:" +
            "\r\n\r\nJack of Hearts (Right Bower)\r\nJack of Diamonds (Left Bower)\r\nAce of Hearts\r\nKing of Hearts" +
            "\r\nQueen of Hearts\r\n10 of Hearts\r\n9 of Hearts" +
            "\r\nPlaying\r\n" +
            "Play is like in most trick taking games. " +
            "A player leads with a suit, other players must follow suit if they have it, but are otherwise " +
            "free to play any card if they have nothing in the lead suit. Cards are ranked from high to low, " +
            "trump beats lead suit, lead suit beats other suits." +
            "\r\n\r\nScoring\r\n" +
            "Now remember that the team that picked trumps are the \"Makers\"." + 
            "A team that wins 3 or more tricks wins the hand and gets points, the " +
            "losing team gets no points." +
            "\r\n\r\nWinning\r\nA team wins once it has gotten 10 points.";

        public GameView()
        {
            InitializeComponent();
        }

        private void RulesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Show a message box with the rules
            MessageBox.Show(rulesString, "Game Rules", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
