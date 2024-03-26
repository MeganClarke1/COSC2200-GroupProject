using System.Windows;
using Group2_COSC2200_Project.model;

namespace Group2_COSC2200_Project_Tests
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Deck deck = new();
            DeckList.ItemsSource = deck.Cards;
        }
    }
}