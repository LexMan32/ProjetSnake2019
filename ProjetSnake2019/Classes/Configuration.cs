using ProjetSnake2019.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSnake2019.Classes
{
    public class Configuration
    {
        public const int SNAKE_SQUARE_SIZE = 36;

        public const int SNAKE_START_LENGTH = 3;
        public const int SNAKE_START_SPEED = 400;
        public const int SNAKE_SPEED_THRESHOLD = 100;
        public const Direction SNAKE_START_DIRECTION = Direction.Right;

        public const string PATCH_IMG_SNAKE_HEAD = "pack://application:,,,/Resources/Image/SnakeHead.png";
        public const string PATCH_IMG_SNAKE_BODY = "pack://application:,,,/Resources/Image/SnakeBody.png";
        public const string PATCH_IMG_SNAKE_TAIL = "pack://application:,,,/Resources/Image/SnakeTail.png";
        public const string PATCH_IMG_APPLE = "pack://application:,,,/Resources/Image/Apple.png";
    }
}
