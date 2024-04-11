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
    /// <summary>
    /// Provides a ViewModel layer for a Card, enabling the representation of card properties in the UI.
    /// </summary>
    public class CardViewModel : ViewModelBase
    {

        /// <summary>
        /// Indicates whether the card is held by a player.
        /// </summary>
        public bool IsPlayerCard { get; set; }

        /// <summary>
        /// Indicates whether the card is currently placed in the play area.
        /// </summary>
        public bool IsInPlayArea { get; set; }

        /// <summary>
        /// Indicates whether the card is the kitty.
        /// </summary>
        public bool IsInKitty { get; set; }

        /// <summary>
        /// Defines the base path for card images, dynamically adjusted based on the current theme.
        /// </summary>
        public static string BaseImagePath { get; set; } = $"../assets/images/{Theme.GetTheme()}";

        /// <summary>
        /// The Card model this ViewModel represents.
        /// </summary>
        public Card Card { get; private set; }

        /// <summary>
        /// Provides the path to the card's image file, with logic to show the card face or back depending on the card's state.
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

        /// <summary>
        /// Updates the ImagePath property, triggering UI updates for the card's image.
        /// </summary>
        public void RefreshImagePath()
        {
            OnPropertyChanged(nameof(ImagePath));
        }
    }
}
