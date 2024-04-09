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
        private readonly NavigationStore _navigationStore;

        public BackCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public override void Execute(object parameter)
        {

            // Navigate to the menu
            _navigationStore.CurrentViewModel = new MenuViewModel(_navigationStore, null);
        }
    }

}
