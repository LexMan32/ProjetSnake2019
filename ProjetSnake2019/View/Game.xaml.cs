using ProjetSnake2019.Classes;
using ProjetSnake2019.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ProjetSnake2019.Vues
{
    /// <summary>
    /// Logique d'interaction pour Partie.xaml
    /// </summary>
    public partial class Game : Window
    {
        private System.Windows.Threading.DispatcherTimer gameTickTimer = new System.Windows.Threading.DispatcherTimer();
        private Random rnd = new Random();
        private int snakeLength;
        private int currentScore;
        private Direction snakeDirection;
        private bool isSnakeAuthorizedToMove;
        private bool isAuthorizedToDrawSnake;

        private List<SnakePart> snakeParts = new List<SnakePart>();
        private SnakeFood snakeFood = null;

        public Game()
        {
            InitializeComponent();
            gameTickTimer.Tick += GameTickTimer_Tick;
            MENU_PAUSE.Visibility = Visibility.Hidden;
            MENU_FIN.Visibility = Visibility.Hidden;
        }

        private void StartNewGame()
        {
            foreach (SnakePart snakeBodyPart in snakeParts)
            {
                if (snakeBodyPart.getUiElement() != null) { 
                    GameArea.Children.Remove(snakeBodyPart.getUiElement());
                }
            }

            snakeParts.Clear();

            if (snakeFood != null)
            {
                GameArea.Children.Remove(snakeFood.getUiElement());
            }

            snakeLength = 3;
            currentScore = 0;
            snakeDirection = Direction.Right;

            snakeParts.Add(new SnakePart(new Point(Configuration.SNAKE_SQUARE_SIZE * 3, Configuration.SNAKE_SQUARE_SIZE * 5), TypeSnakePart.Tail, Configuration.SNAKE_START_DIRECTION));
            snakeParts.Add(new SnakePart(new Point(Configuration.SNAKE_SQUARE_SIZE * 4, Configuration.SNAKE_SQUARE_SIZE * 5), TypeSnakePart.Body, Configuration.SNAKE_START_DIRECTION));
            snakeParts.Add(new SnakePart(new Point(Configuration.SNAKE_SQUARE_SIZE * 5, Configuration.SNAKE_SQUARE_SIZE * 5), TypeSnakePart.Head, Configuration.SNAKE_START_DIRECTION));

            gameTickTimer.Interval = TimeSpan.FromMilliseconds(Configuration.SNAKE_START_SPEED);

            isAuthorizedToDrawSnake = true;

            DrawSnake();

            DrawSnakeFood();

            UpdateGameStatus();
       
            gameTickTimer.IsEnabled = true;
        }

        private void DrawSnake()
        {
            foreach (SnakePart snakePart in snakeParts)
            {
                GameArea.Children.Add(snakePart.getUiElement());

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
                
                Canvas.SetTop(snakePart.getUiElement(), snakePart.getPosition().Y);
                Canvas.SetLeft(snakePart.getUiElement(), snakePart.getPosition().X);
            }
        }

        private void DrawSnakeFood()
        {
            snakeFood = new SnakeFood(GetNextFoodPosition());
            
            GameArea.Children.Add(snakeFood.getUiElement());
            Canvas.SetTop(snakeFood.getUiElement(), snakeFood.getPosition().Y);
            Canvas.SetLeft(snakeFood.getUiElement(), snakeFood.getPosition().X);

        }

        private void MoveSnake()
        {
            while (snakeParts.Count == snakeLength)
            {
                GameArea.Children.Remove(snakeParts[0].getUiElement());
                GameArea.Children.Remove(snakeParts[1].getUiElement());
                snakeParts.RemoveAt(0);
            }

            foreach (SnakePart snakePart in snakeParts)
            {
                GameArea.Children.Remove(snakePart.getUiElement());
            }

            snakeParts[0].setTypeSnakePart(TypeSnakePart.Tail);

            SnakePart snakeHead = snakeParts[snakeParts.Count - 1];
            double nextX = snakeHead.getPosition().X;
            double nextY = snakeHead.getPosition().Y;
            snakeHead.setTypeSnakePart(TypeSnakePart.Body);

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

            snakeParts.Add(new SnakePart(new Point(nextX, nextY), TypeSnakePart.Head, snakeDirection));

            DoCollisionCheckWall();

            if (isAuthorizedToDrawSnake)
            {
                DrawSnake();
            }

            DoCollisionCheckFood();
        }

        private Point GetNextFoodPosition()
        {
            int maxX = (int)(GameArea.ActualWidth / Configuration.SNAKE_SQUARE_SIZE);
            int maxY = (int)(GameArea.ActualHeight / Configuration.SNAKE_SQUARE_SIZE);
            int foodX = rnd.Next(0, maxX) * Configuration.SNAKE_SQUARE_SIZE;
            int foodY = rnd.Next(0, maxY) * Configuration.SNAKE_SQUARE_SIZE;

            foreach (SnakePart snakePart in snakeParts)
            {
                if ((snakePart.getPosition().X == foodX) && (snakePart.getPosition().Y == foodY))
                    return GetNextFoodPosition();
            }

            return new Point(foodX, foodY);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (isSnakeAuthorizedToMove) { 
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
                        MENU_PAUSE.Visibility = Visibility.Visible;
                        gameTickTimer.IsEnabled = false;
                        break;
                }

                isSnakeAuthorizedToMove = false;
            }
        }

        private void DoCollisionCheckWall()
        {
            SnakePart snakeHead = snakeParts[snakeParts.Count - 1];

            if ((snakeHead.getPosition().Y < 0) || (snakeHead.getPosition().Y >= GameArea.ActualHeight) ||
            (snakeHead.getPosition().X < 0) || (snakeHead.getPosition().X >= GameArea.ActualWidth))
            {
                EndGame();
            }

            foreach (SnakePart snakeBodyPart in snakeParts.Take(snakeParts.Count - 1))
            {
                if ((snakeHead.getPosition().X == snakeBodyPart.getPosition().X) && (snakeHead.getPosition().Y == snakeBodyPart.getPosition().Y))
                {
                    EndGame();
                }     
            }
        }

        private void DoCollisionCheckFood()
        {
            SnakePart snakeHead = snakeParts[snakeParts.Count - 1];

            if ((snakeHead.getPosition().X == Canvas.GetLeft(snakeFood.getUiElement())) && (snakeHead.getPosition().Y == Canvas.GetTop(snakeFood.getUiElement())))
            {
                EatSnakeFood();
            }
        }

        private void EatSnakeFood()
        {
            snakeLength++;
            currentScore += 10;
            int timerInterval = Math.Max(Configuration.SNAKE_SPEED_THRESHOLD, (int)gameTickTimer.Interval.TotalMilliseconds - 10);
            gameTickTimer.Interval = TimeSpan.FromMilliseconds(timerInterval);
            GameArea.Children.Remove(snakeFood.getUiElement());
            DrawSnakeFood();
            UpdateGameStatus();
        }

        private void UpdateGameStatus()
        {
            LB_SCORE.Content = currentScore.ToString();
            LB_SCORE_FIN.Content = currentScore.ToString();
        }

        private void EndGame()
        {
            isAuthorizedToDrawSnake = false;
            gameTickTimer.IsEnabled = false;
            MENU_FIN.Visibility = Visibility.Visible;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            StartNewGame();
        }

        private void GameTickTimer_Tick(object sender, EventArgs e)
        {
            MoveSnake();
            isSnakeAuthorizedToMove = true;
        }

        private void BT_REPRENDRE_Click(object sender, RoutedEventArgs e)
        {
            MENU_PAUSE.Visibility = Visibility.Hidden;
            gameTickTimer.IsEnabled = true;
        }

        private void BT_REJOUER_Click(object sender, RoutedEventArgs e)
        {
            MENU_FIN.Visibility = Visibility.Hidden;
            StartNewGame();
        }

        private void BT_QUITTER_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
