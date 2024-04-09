﻿/// <file>
///   <summary>
///     File Name: BackCommand.cs
///   </summary>
///   <author>
///     Authors: Brody Dentinger, Megan Clarke, Colin Eade, Muhammad Yasir Patel
///   </author>
///   <created>
///     Created: April 9, 2024
///   </created>
///   <lastModified>
///     Last Modified: April 9, 2024
///   </lastModified>
///   <description>
///     Description: This file is a command file to navigate back from the stats page to the main menu page.
///   </description>
/// </file>

using Group2_COSC2200_Project.model;
using Group2_COSC2200_Project.stores;
using Group2_COSC2200_Project.viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Group2_COSC2200_Project.commands
{
    /// <summary>
    /// For returning to the MenuView with the back button from stats.
    /// </summary>
    public class BackCommand : CommandBase
    {
        /// <summary>
        /// The navigation store (current View model) to be altered with the desired new ViewModel.
        /// </summary>
        private readonly NavigationStore _navigationStore;

        /// <summary>
        /// The constructor for the back Command.
        /// </summary>
        /// <param name="navigationStore"> The desired ViewModel to navigate to. </param>
        public BackCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        /// <summary>
        /// Overrirdes the execution in the CommandBase class to execute the back command, which changes the ViewModel
        /// to the Menu.
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object parameter)
        {
            // Navigate to the menu
            _navigationStore.CurrentViewModel = new MenuViewModel(_navigationStore, null);
        }
    }

}
