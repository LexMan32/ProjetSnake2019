using System.Windows;
using System.Windows.Input;

namespace ProjetSnake2019.Vues
{
    /// <summary>
    /// Logique d'interaction pour Menu.xaml
    /// </summary>
    public partial class Main : Window
    {
        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        public Main()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Clique sur le bouton "Jouer", lancement d'une partie.
        /// </summary>
        private void BT_JOUER_Click(object sender, RoutedEventArgs e)
        {
            Game game = new Game();
            game.ShowDialog();
        }

        /// <summary>
        /// Clique sur le bouton "Quitter", fermeture de l'application.
        /// </summary>
        private void BT_QUITTER_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
