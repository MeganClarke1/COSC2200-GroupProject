using Group2_COSC2200_Project.model;

namespace Group2_COSC2200_Project.viewmodel
{
    public class CardViewModel : ViewModelBase
    {

        /// <summary>
        /// Static property to set the base image path
        /// </summary>
        public static string BaseImagePath { get; set; } = "../assets/images/classic";

        /// <summary>
        /// The Card model this ViewModel represents.
        /// </summary>
        public Card Card { get; private set; }

        /// <summary>
        /// Path to the image representing the Card.
        /// </summary>
        public string ImagePath
        {
            get
            {
                return $"{BaseImagePath}/{Card.Rank.ToString().ToLower()}_of_{Card.Suit.ToString().ToLower()}.png";
            }
        }

        /// <summary>
        /// Initializes a new instance of the CardViewModel class with a specified Card.
        /// </summary>
        /// <param name="card">The Card model to represent.</param>
        public CardViewModel(Card card)
        {
            Card = card;
        }
    }
}
