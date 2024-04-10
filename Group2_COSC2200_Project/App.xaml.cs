/// <file>
///   <summary>
///     File Name: App.xaml.cs
///   </summary>
///   <author>
///     Authors: Brody Dentinger, Megan Clarke, Colin Eade, Muhammad Yasir Patel
///   </author>
///   <created>
///     Created: Feb 27, 2024
///   </created>
///   <lastModified>
///     Last Modified: April 9, 2024
///   </lastModified>
///   <description>
///     Description: This class represents the main entry point for our application.
///   </description>
/// </file>

using Group2_COSC2200_Project.viewmodel;
using System.Windows;
using Group2_COSC2200_Project.stores;

namespace Group2_COSC2200_Project
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private readonly NavigationStore _navigationStore;

        public App()
        {
            // 2. Instantiate the navigationStore upon app startup
            _navigationStore = new NavigationStore();
        }

        /// <summary>
        /// On startup defines the window that will be navigated to by default. This is bound to the view based from the 
        ///     currentViewModel in the navigation store.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore.CurrentViewModel = new MenuViewModel(_navigationStore, null); // The Default view on app startup

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore) // 3. This takes the navigationStore as an arugment 
            };
            MainWindow.Show();
            base.OnStartup(e);
        }
    }

}
