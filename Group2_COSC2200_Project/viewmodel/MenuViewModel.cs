/// <file>
///   <summary>
///     File Name: MenuViewModel.cs
///   </summary>
///   <author>
///     Authors: Brody Dentinger, Megan Clarke, Colin Eade, Muhammad Yasir Patel
///   </author>
///   <created>
///     Created: Apr 7, 2024
///   </created>
///   <lastModified>
///     Last Modified: April 9, 2024
///   </lastModified>
///   <description>
///     Description: This class represents the MenuViewModel, which is the menu view, and the first view presented to the user.
///   </description>
/// </file>

using Group2_COSC2200_Project.commands;
using Group2_COSC2200_Project.model;
using Group2_COSC2200_Project.stores;
using System.Windows.Input;

namespace Group2_COSC2200_Project.viewmodel
{
    /// <summary>
    /// Represnts the menu view Model.
    /// </summary>
    public class MenuViewModel : ViewModelBase
    { 
        /// <summary>
        /// StatsView command for stats button on view.
        /// </summary>
        public ICommand StatsViewCommand {  get; }
        /// <summary>
        /// Continue Command for continue button on view.
        /// </summary>
        public ICommand ContinueCommand { get; } 
        
        /// <summary>
        /// Binds the NewGameCommand to the MenuViewModel. Will be used to navigate to the GameView.
        /// </summary>
        /// <param name="navigationStore"> The view to be navigated to </param>
        public MenuViewModel(NavigationStore navigationStore, Statistics playerStats) 
        {

            StatsViewCommand = new StatsPageCommand(navigationStore, playerStats); // **** passing playerStats arg
            ContinueCommand = new FetchStatsCommand(navigationStore, playerStats); // *** passing playerStats arg

        }
    }
}
