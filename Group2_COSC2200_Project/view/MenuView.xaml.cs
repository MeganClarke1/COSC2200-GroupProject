/// <file>
///   <summary>
///     File Name: MenuView.xaml.cs
///   </summary>
///   <author>
///     Authors: Brody Dentinger, Megan Clarke, Colin Eade, Muhammad Yasir Patel
///   </author>
///   <created>
///     Created: April 5, 2024
///   </created>
///   <lastModified>
///     Last Modified: April 9, 2024
///   </lastModified>
///   <description>
///     Description: This file contains the code behind for our Menu View. 
///   </description>
/// </file>

using Group2_COSC2200_Project.viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Group2_COSC2200_Project.view
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class MenuView : UserControl
    {
        public MenuView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The on click event of the rules menu item from the game View. Will present a messagebox with the rules.
        /// </summary>
        /// <param name="sender"> The Sending object. </param>
        /// <param name="e"> The routed event object. </param>
        private void RulesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Show a message box with the rules
            MessageBox.Show(GameView.rulesString, "Game Rules", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// On click event for the user guide.
        /// </summary>
        /// <param name="sender"> The Sending object. </param>
        /// <param name="e"> The routed event object. </param>
        private void UserGuideItem_Click(object sender, RoutedEventArgs e)
        {
            string pdfPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EuchreUserGuideV1.0.pdf");
            // Create a new ProcessStartInfo
            var psi = new System.Diagnostics.ProcessStartInfo()
            {
                FileName = pdfPath,
                UseShellExecute = true // Important to set this to true
            };

            // Start the process with the ProcessStartInfo
            try
            {
                System.Diagnostics.Process.Start(psi);
            }
            catch (Exception ex)
            {
                // Handle exceptions, for example, log them or show to the user
                MessageBox.Show($"Failed to open the PDF: {ex.Message}");
            }
        }
    }
}

