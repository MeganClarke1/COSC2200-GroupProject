using Group2_COSC2200_Project.viewmodel;
using System.Configuration;
using System.Data;
using System.Windows;
using Group2_COSC2200_Project.model;
using System.Diagnostics;

namespace Group2_COSC2200_Project
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Debug.WriteLine("Hello");
            String name = "hello";
            Debug.WriteLine(name);
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel()
            };
            MainWindow.Show();
            base.OnStartup(e);
        }
    }

}
