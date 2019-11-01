using ProjetSnake2019.Classe;
using ProjetSnake2019.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ProjetSnake2019.View
{
    /// <summary>
    /// Logique d'interaction pour Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        // Champs nécéssaires au fonctionnement du jeux
        private System.Windows.Threading.DispatcherTimer gameTickTimer = new System.Windows.Threading.DispatcherTimer();
        private Random rnd = new Random();
        private List<SnakePart> snakeParts = new List<SnakePart>();
        private SnakeFood snakeFood = null;
        private Direction snakeDirection;
        private int snakeLength;
        private int currentScore;
        private bool isSnakeAuthorizedToMove;
        private bool isAuthorizedToDrawSnake;

        /// <summary>
        /// Constructeur de la fenêtre Game.
        /// </summary>
        public Game()
        {
            InitializeComponent();

            // Masquage des menus Pause et End
            MenuPause.Visibility = Visibility.Hidden;
            MenuEnd.Visibility = Visibility.Hidden;

            // Attribution de la méthode liée au timer
            gameTickTimer.Tick += GameTickTimerTick;
        }

        /// <summary>
        /// Démarrage d'une nouvelle partie.
        /// </summary>
        private void StartNewGame()
        {
            // Nettoyage du corps du serpent
            foreach (SnakePart snakeBodyPart in snakeParts)
            {
                if (snakeBodyPart.getUiElement() != null) { 
                    GameArea.Children.Remove(snakeBodyPart.getUiElement());
                }
            }
            snakeParts.Clear();

            // Nettoyage de la nourriture
            if (snakeFood != null)
            {
                GameArea.Children.Remove(snakeFood.getUiElement());
            }

            // Initilisation des variables de la partie
            snakeLength = Configuration.SNAKE_START_LENGTH;
            currentScore = 0;
            snakeDirection = Direction.Right;

            // Ajout des éléments du serpent
            snakeParts.Add(new SnakePart(new Point(Configuration.SNAKE_SQUARE_SIZE * 3, Configuration.SNAKE_SQUARE_SIZE * 5), TypeSnakePart.Tail, Configuration.SNAKE_START_DIRECTION));
            snakeParts.Add(new SnakePart(new Point(Configuration.SNAKE_SQUARE_SIZE * 4, Configuration.SNAKE_SQUARE_SIZE * 5), TypeSnakePart.Body, Configuration.SNAKE_START_DIRECTION));
            snakeParts.Add(new SnakePart(new Point(Configuration.SNAKE_SQUARE_SIZE * 5, Configuration.SNAKE_SQUARE_SIZE * 5), TypeSnakePart.Head, Configuration.SNAKE_START_DIRECTION));

            // Initilisation de l'interval du timer
            gameTickTimer.Interval = TimeSpan.FromMilliseconds(Configuration.SNAKE_START_SPEED);

            // Autorisation de dessiner le serpent 
            isAuthorizedToDrawSnake = true;

            // Dessiner les éléments du jeux
            DrawSnake();
            DrawSnakeFood();

            // Mise à jour du statut du jeux
            UpdateGameStatus();
       
            // Démarrage du timer
            gameTickTimer.IsEnabled = true;
        }

        /// <summary>
        /// Affichage du serpent.
        /// </summary>
        private void DrawSnake()
        {
            // Pour chaucune des parties du serpent
            foreach (SnakePart snakePart in snakeParts)
            {
                // Ajouter la partie au canvas
                GameArea.Children.Add(snakePart.getUiElement());
                Canvas.SetTop(snakePart.getUiElement(), snakePart.getPosition().Y);
                Canvas.SetLeft(snakePart.getUiElement(), snakePart.getPosition().X);

                // Si la partie du serpent est la tête ou le corps, calcul de la rotation d'après leur direction
                if (snakePart.getTypeSnakePart() == TypeSnakePart.Head || snakePart.getTypeSnakePart() == TypeSnakePart.Tail)
                {
                    switch (snakePart.getDirection())
                    {
                        case Direction.Left:
                            snakePart.getUiElement().RenderTransform = new RotateTransform(180, 18, 18);
                            break;
                        case Direction.Right:
                            snakePart.getUiElement().RenderTransform = new RotateTransform(0, 18, 18);
                            break;
                        case Direction.Up:
                            snakePart.getUiElement().RenderTransform = new RotateTransform(270, 18, 18);
                            break;
                        case Direction.Down:
                            snakePart.getUiElement().RenderTransform = new RotateTransform(90, 18, 18);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Affichage de la nourriture pour le serpent.
        /// </summary>
        private void DrawSnakeFood()
        {
            // Création d'une nouvelle nourriture
            snakeFood = new SnakeFood(GetNextFoodPosition());
            
            // Ajouter l'élément au canvas
            GameArea.Children.Add(snakeFood.getUiElement());
            Canvas.SetTop(snakeFood.getUiElement(), snakeFood.getPosition().Y);
            Canvas.SetLeft(snakeFood.getUiElement(), snakeFood.getPosition().X);
        }

        /// <summary>
        /// Déplacement du serpent
        /// </summary>
        private void MoveSnake()
        {
            // Si la grandeur du serpent n'a pas changé, on supprimer la queue et la
            //  dernière partie du corp
            if (snakeParts.Count == snakeLength)
            {
                GameArea.Children.Remove(snakeParts[0].getUiElement());
                GameArea.Children.Remove(snakeParts[1].getUiElement());
                snakeParts.RemoveAt(0);
            }

            // Pour chaque partie du serpent, on supprimer les éléments du canvas
            foreach (SnakePart snakePart in snakeParts)
            {
                GameArea.Children.Remove(snakePart.getUiElement());
            }

            // La dernière partie du corps devient la queue
            snakeParts[0].setTypeSnakePart(TypeSnakePart.Tail);

            // Récupération de la tête actuelle
            SnakePart snakeHead = snakeParts[snakeParts.Count - 1];

            // La tête devient le corps
            snakeHead.setTypeSnakePart(TypeSnakePart.Body);

            // Récupération des positions actuelle de la tête
            double nextX = snakeHead.getPosition().X;
            double nextY = snakeHead.getPosition().Y;
            
            // Calcul des prochaines positions de la tête
            switch (snakeDirection)
            {
                case Direction.Left:
                    nextX -= Configuration.SNAKE_SQUARE_SIZE;
                    break;
                case Direction.Right:
                    nextX += Configuration.SNAKE_SQUARE_SIZE;
                    break;
                case Direction.Up:
                    nextY -= Configuration.SNAKE_SQUARE_SIZE;
                    break;
                case Direction.Down:
                    nextY += Configuration.SNAKE_SQUARE_SIZE;
                    break;
            }

            // Création de la nouvelle tête
            snakeParts.Add(new SnakePart(new Point(nextX, nextY), TypeSnakePart.Head, snakeDirection));

            // Vérification d'une collision avec les murs ou le corps
            DoCollisionCheckWall();

            // Si le serpent est autorisé a se dessiner, le dessine
            if (isAuthorizedToDrawSnake)
            {
                DrawSnake();
            }

            // Vérification d'une collision avec de la nourriture
            DoCollisionCheckFood();
        }

        /// <summary>
        /// Cacul de la position de la prochaine nourriture.
        /// </summary>
        /// <returns>La valeur de la prochaine position</returns>
        private Point GetNextFoodPosition()
        {
            // Calcul de la valeur maximum pour les positions
            int maxX = (int)(GameArea.ActualWidth / Configuration.SNAKE_SQUARE_SIZE);
            int maxY = (int)(GameArea.ActualHeight / Configuration.SNAKE_SQUARE_SIZE);

            // Génération des prochaines positions
            int foodX = rnd.Next(0, maxX) * Configuration.SNAKE_SQUARE_SIZE;
            int foodY = rnd.Next(0, maxY) * Configuration.SNAKE_SQUARE_SIZE;

            // Vérification de la présence de la nourriture sur le serpent
            foreach (SnakePart snakePart in snakeParts)
            {
                if ((snakePart.getPosition().X == foodX) && (snakePart.getPosition().Y == foodY))
                {
                    return GetNextFoodPosition();
                }
            }

            // Retourne les valeurs de la prochaine position
            return new Point(foodX, foodY);
        }

        /// <summary>
        /// Evénement lors de la pression d'une touche du clavier.
        /// </summary>
        private void WindowKeyUp(object sender, KeyEventArgs e)
        {
            // Si le serpent est autorisé a changer de direction
            if (isSnakeAuthorizedToMove) {
                // Changement de la direction
                switch (e.Key)
                {
                    case Key.Up:
                    case Key.W:
                        if (snakeDirection != Direction.Down)
                            snakeDirection = Direction.Up;
                        break;
                    case Key.Down:
                    case Key.S:
                        if (snakeDirection != Direction.Up)
                            snakeDirection = Direction.Down;
                        break;
                    case Key.Left:
                    case Key.A:
                        if (snakeDirection != Direction.Right)
                            snakeDirection = Direction.Left;
                        break;
                    case Key.Right:
                    case Key.D:
                        if (snakeDirection != Direction.Left)
                            snakeDirection = Direction.Right;
                        break;
                    case Key.Escape:
                        MenuPause.Visibility = Visibility.Visible;
                        gameTickTimer.IsEnabled = false;
                        break;
                }

                // Suppression de l'autorisation de changement
                isSnakeAuthorizedToMove = false;
            }
        }

        /// <summary>
        /// Vérification de collision pour le serpent.
        /// </summary>
        private void DoCollisionCheckWall()
        {
            // Récupération de la tête du serpent
            SnakePart snakeHead = snakeParts[snakeParts.Count - 1];

            // Vérification d'une collision avec les murs
            if ((snakeHead.getPosition().Y < 0) || (snakeHead.getPosition().Y >= GameArea.ActualHeight) ||
            (snakeHead.getPosition().X < 0) || (snakeHead.getPosition().X >= GameArea.ActualWidth))
            {
                EndGame();
            }

            // Vérification d'une collision avec une partie du serpent
            foreach (SnakePart snakeBodyPart in snakeParts.Take(snakeParts.Count - 1))
            {
                if ((snakeHead.getPosition().X == snakeBodyPart.getPosition().X) && (snakeHead.getPosition().Y == snakeBodyPart.getPosition().Y))
                {
                    EndGame();
                }     
            }
        }

        /// <summary>
        /// Vérification d'une collision avec la nourriture.
        /// </summary>
        private void DoCollisionCheckFood()
        {
            // Récupération de la tête du serpent
            SnakePart snakeHead = snakeParts[snakeParts.Count - 1];

            // Vérification d'une collision avec la nourriture
            if ((snakeHead.getPosition().X == Canvas.GetLeft(snakeFood.getUiElement())) && (snakeHead.getPosition().Y == Canvas.GetTop(snakeFood.getUiElement())))
            {
                EatSnakeFood();
            }
        }

        /// <summary>
        /// Action lorsque le seprent mange une nourriture.
        /// </summary>
        private void EatSnakeFood()
        {
            // Aggrandir le serpent
            snakeLength++;

            // Augmenter le score
            currentScore += 10;

            // Cacul et affectation du timer (accélération)
            int timerInterval = Math.Max(Configuration.SNAKE_SPEED_THRESHOLD, (int)gameTickTimer.Interval.TotalMilliseconds - 10);
            gameTickTimer.Interval = TimeSpan.FromMilliseconds(timerInterval);

            // Suppresion du canvas de la nourriture actuelle
            GameArea.Children.Remove(snakeFood.getUiElement());

            // Affichage d'une nouvelle nourriture
            DrawSnakeFood();

            // Raffraichir les status du jeux
            UpdateGameStatus();
        }

        /// <summary>
        /// Mise à jour des status du jeux.
        /// </summary>
        private void UpdateGameStatus()
        {
            // Mise à jour des labels
            LbScore.Content = currentScore.ToString();
            LbScoreEnd.Content = currentScore.ToString();
        }

        /// <summary>
        /// Fin de la partie.
        /// </summary>
        private void EndGame()
        {
            // Suppresion de l'autorisation de dessiner le serpent
            isAuthorizedToDrawSnake = false;

            // Arrêt du timer
            gameTickTimer.IsEnabled = false;

            // Affichage du menu de fin de partie
            MenuEnd.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Evénement lorsque la fenètre du jeux a terminé de se charger.
        /// </summary>
        private void WindowContentRendered(object sender, EventArgs e)
        {
            // Démarrage d'une partie
            StartNewGame();
        }

        /// <summary>
        /// Evànement lors du tick du timer.
        /// </summary>
        private void GameTickTimerTick(object sender, EventArgs e)
        {
            // Autorisation au déplacement
            isSnakeAuthorizedToMove = true;

            // Déplacement du serpent
            MoveSnake();
        }

        /// <summary>
        /// Clique sur le bouton "Resume".
        /// </summary>
        private void BtResumeClick(object sender, RoutedEventArgs e)
        {
            // Cacher le menu de pause
            MenuPause.Visibility = Visibility.Hidden;

            // Démarrage du timer
            gameTickTimer.IsEnabled = true;
        }

        /// <summary>
        /// Clique sur le bouton "Replay".
        /// </summary>
        private void BtReplayClick(object sender, RoutedEventArgs e)
        {
            // Cacher le menu de fin de partie
            MenuEnd.Visibility = Visibility.Hidden;

            // Démarrage d'une nouvelle partie
            StartNewGame();
        }

        /// <summary>
        /// Clique sur le bouton "Quit", fermeture de la fenêtre.
        /// </summary>
        private void BtQuitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
