using ProjetSnake2019.Classes;
using ProjetSnake2019.Enumerations;
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
using System.Windows.Threading;

namespace ProjetSnake2019.Vues
{
    /// <summary>
    /// Logique d'interaction pour Partie.xaml
    /// </summary>
    public partial class Partie : Window
    {
        private Grid gdMenuPause;
        private Label lbScore;
        private Image imPlateau;

        private List<Element> serpent = new List<Element>();
        private Element pomme = new Element(0, 0, Direction.Droite, TypeElement.Pomme);

        private int vitesse;
        private int score;
        private bool gameOver;
        private Direction direction;
        private DispatcherTimer gameTimer;

        private const int NBR_CASE_X = 16;
        private const int NBR_CASE_Y = 16;
        private const int NBR_POINTS = 100;

        public Partie()
        {
            InitializeComponent();

            // Valeurs par défaut pour les paramètres
            vitesse = 4;
            score = 0;
            gameOver = false;
            direction = Direction.Droite;

            // Référence vers le menu pause
            gdMenuPause = (Grid)this.FindName("MENU_PAUSE");
            lbScore = (Label)this.FindName("LB_SCORE");
            imPlateau = (Image)this.FindName("IM_PLATEAU"); 

            // Vitesse du jeu + démarrage du timer
            gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromMilliseconds(5000);
            gameTimer.Tick += raffraichirEcran;
            gameTimer.Start();

            // Démarrer le jeu
            serpent.Add(new Element(8, 8, Direction.Droite, TypeElement.Tete));
            serpent.Add(new Element(7, 8, Direction.Droite, TypeElement.Corp));
            serpent.Add(new Element(6, 8, Direction.Droite, TypeElement.Queue));

            lbScore.Content = score.ToString();

            genererPomme();
        }

        private void genererPomme()
        {
            int maxX = (int)imPlateau.Width / NBR_CASE_X;
            int maxY = (int)imPlateau.Height / NBR_CASE_Y;

            Random random = new Random();
            pomme = new Element(random.Next(0,maxX), random.Next(0,maxY), Direction.Haut, TypeElement.Pomme);
        }

        private void raffraichirEcran()
        {
            if (gameOver)
            {
                this.Close();
            }
            else
            {
                if ((EntreeClavier.estTouchePressee(Key.Right) || EntreeClavier.estTouchePressee(Key.D)) && direction != Direction.Gauche)
                    direction = Direction.Droite;
                else if ((EntreeClavier.estTouchePressee(Key.Left) || EntreeClavier.estTouchePressee(Key.A)) && direction != Direction.Droite)
                    direction = Direction.Gauche;
                else if ((EntreeClavier.estTouchePressee(Key.Up) || EntreeClavier.estTouchePressee(Key.W)) && direction != Direction.Bas)
                    direction = Direction.Haut;
                else if ((EntreeClavier.estTouchePressee(Key.Down) || EntreeClavier.estTouchePressee(Key.S)) && direction != Direction.Haut)
                    direction = Direction.Bas;

                bougerJoueur();
            }

            imPlateau.Invalidate();
        }

        private void BT_REPRENDRE_Click(object sender, RoutedEventArgs e)
        {
            gdMenuPause = (Grid)this.FindName("MENU_PAUSE");
            gdMenuPause.Visibility = Visibility.Hidden;
        }

        private void BT_QUITTER_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
