using ProjetSnake2019.Enumeration;

namespace ProjetSnake2019.Classe
{
    /// <summary>
    /// Configurations de l'application
    /// </summary>
    public class Configuration
    {
        // Configurations pour le jeux
        public const int SNAKE_SQUARE_SIZE = 36;
        public const int SNAKE_START_LENGTH = 3;
        public const int SNAKE_START_SPEED = 400;
        public const int SNAKE_SPEED_THRESHOLD = 100;
        public const Direction SNAKE_START_DIRECTION = Direction.RIGHT;

        // Chemin d'accès vers les images
        public const string PATH_IMG_SNAKE_HEAD = "pack://application:,,,/Resources/Image/SnakeHead.png";
        public const string PATH_IMG_SNAKE_BODY = "pack://application:,,,/Resources/Image/SnakeBody.png";
        public const string PATH_IMG_SNAKE_TAIL = "pack://application:,,,/Resources/Image/SnakeTail.png";
        public const string PATH_IMG_APPLE = "pack://application:,,,/Resources/Image/Apple.png";
    }
}
