/// <file>
///   <summary>
///     File Name: KittyViewModel.cs
///   </summary>
///   <author>
///     Authors: Brody Dentinger, Megan Clarke, Colin Eade, Muhammad Yasir Patel
///   </author>
///   <created>
///     Created: Apr 2, 2024
///   </created>
///   <lastModified>
///     Last Modified: April 9, 2024
///   </lastModified>
///   <description>
///     Description: This class represents the KittyViewModel, which is the 1 card flipped face up AKA the kitty.
///   </description>
/// </file>

using Group2_COSC2200_Project.model;
using System.Collections.ObjectModel;

namespace Group2_COSC2200_Project.viewmodel
{
    /// <summary>
    /// The KittyViewModel class is the kitty card to be determined after deck being dealt.
    /// </summary>
    public class KittyViewModel : ViewModelBase
    {
        /// <summary>
        /// An observable collection of Kitty Card Objects.
        /// </summary>
        public ObservableCollection<CardViewModel> KittyCard { get; }

        /// <summary>
        /// Constructor for the KittyViewModel
        /// </summary>
        /// <param name="kitty"> Takes a list of card objects (The list of kitty cards) </param>
        public KittyViewModel(List<Card> kitty)
        {
            KittyCard = new ObservableCollection<CardViewModel>();

            // For each card in the list of card objects (the kitty), create a new CardViewModel, and add it to the list.
            foreach (Card card in kitty)
            {
                CardViewModel cardViewModel = new CardViewModel(card);
                KittyCard.Add(cardViewModel);
            }
        }
    }
}
