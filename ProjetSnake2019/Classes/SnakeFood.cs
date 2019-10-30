using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ProjetSnake2019.Classes
{
    public class SnakeFood
    {
        private UIElement uiElement;
        private Point position;

        public SnakeFood(Point position)
        {
            this.position = position;

            uiElement = new Image()
            {
                Source = new BitmapImage(new Uri(Configuration.PATCH_IMG_APPLE)),
                Width = Configuration.SNAKE_SQUARE_SIZE,
                Height = Configuration.SNAKE_SQUARE_SIZE
            };
        }

        public UIElement getUiElement()
        {
            return uiElement;
        }

        public Point getPosition()
        {
            return position;
        }
        public void setPosition(Point position)
        {
            this.position = position;
        }
    }
}
