using Group2_COSC2200_Project.viewmodel;
using System.Configuration;
using System.Data;
using System.Windows;
using Group2_COSC2200_Project.model;
using System.Diagnostics;
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

        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore.CurrentViewModel = new MenuViewModel(_navigationStore); // The Default view on app startup

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore) // 3. This takes the navigationStore as an arugment ?
            };
            MainWindow.Show();
            base.OnStartup(e);
        }
    }

}
