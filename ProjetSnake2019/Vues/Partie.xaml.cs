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
using System.Windows.Shapes;

namespace ProjetSnake2019.Vues
{
    /// <summary>
    /// Logique d'interaction pour Partie.xaml
    /// </summary>
    public partial class Partie : Window
    {
        private Grid menuPause;

        public Partie()
        {
            InitializeComponent();

            menuPause = (Grid)this.FindName("MENU_PAUSE");
            menuPause.Visibility = Visibility.Hidden;
        }

        private void BT_REPRENDRE_Click(object sender, RoutedEventArgs e)
        {
            menuPause = (Grid)this.FindName("MENU_PAUSE");
            menuPause.Visibility = Visibility.Hidden;
        }

        private void BT_QUITTER_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
