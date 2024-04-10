/// <file>
///   <summary>
///     File Name: CardViewModel.cs
///   </summary>
///   <author>
///     Authors: Brody Dentinger, Megan Clarke, Colin Eade, Muhammad Yasir Patel
///   </author>
///   <created>
///     Created: April 1, 2024
///   </created>
///   <lastModified>
///     Last Modified: April 9, 2024
///   </lastModified>
///   <description>
///     Description: This class represents a ViewModel for a Card.
///   </description>
/// </file>

using Group2_COSC2200_Project.model;

namespace Group2_COSC2200_Project.viewmodel
{
    public class CardViewModel : ViewModelBase
    {

        /// <summary>
        /// 
        /// </summary>
        public bool IsPlayerCard { get; set; } 

        /// <summary>
        /// 
        /// </summary>
        public bool IsInPlayArea { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsInKitty { get; set; }

        /// <summary>
        /// Static property to set the base image path
        /// </summary>
        public static string BaseImagePath { get; set; } = $"../assets/images/classic";

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
                if (IsInPlayArea || IsPlayerCard || IsInKitty)
                {
                    return $"{BaseImagePath}/{Card.Rank.ToString().ToLower()}_of_{Card.Suit.ToString().ToLower()}.png";
                }
                else
                {
                    return $"{BaseImagePath}/card_back.png";
                }
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

        public void RefreshImagePath()
        {
            OnPropertyChanged(nameof(ImagePath));
        }
    }
}
