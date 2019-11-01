using System.Windows;

namespace ProjetSnake2019.View
{
    /// <summary>
    /// Logique d'interaction pour Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        /// <summary>
        /// Constructeur de la fenêtre Main.
        /// </summary>
        public Main()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Clique sur le bouton "Play", lancement d'une partie.
        /// </summary>
        private void BtPlayClick(object sender, RoutedEventArgs e)
        {
            Game game = new Game();
            game.ShowDialog();
        }

        /// <summary>
        /// Clique sur le bouton "Quit", fermeture de l'application.
        /// </summary>
        private void BtQuitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
