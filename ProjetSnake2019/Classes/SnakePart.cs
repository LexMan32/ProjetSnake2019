using ProjetSnake2019.Enumeration;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ProjetSnake2019.Classes
{
    public class SnakePart
    {
        private UIElement uiElement;
        private Point position;
        private TypeSnakePart typeSnakePart;

        public SnakePart(Point position, TypeSnakePart typeSnakePart)
        {
            this.position = position;
            this.typeSnakePart = typeSnakePart;

            updateUiElement();
        }

        private void updateUiElement()
        {
            switch (typeSnakePart)
            {
                case TypeSnakePart.Head:
                    uiElement = new Image()
                    {
                        Source = new BitmapImage(new Uri(Configuration.PATCH_IMG_SNAKE_HEAD)),
                        Width = Configuration.SNAKE_SQUARE_SIZE,
                        Height = Configuration.SNAKE_SQUARE_SIZE
                    };
                    break;
                case TypeSnakePart.Body:
                    uiElement = new Image()
                    {
                        Source = new BitmapImage(new Uri(Configuration.PATCH_IMG_SNAKE_BODY)),
                        Width = Configuration.SNAKE_SQUARE_SIZE,
                        Height = Configuration.SNAKE_SQUARE_SIZE
                    };
                    break;
                case TypeSnakePart.Tail:
                    uiElement = new Image()
                    {
                        Source = new BitmapImage(new Uri(Configuration.PATCH_IMG_SNAKE_TAIL)),
                        Width = Configuration.SNAKE_SQUARE_SIZE,
                        Height = Configuration.SNAKE_SQUARE_SIZE
                    };
                    break;
            }
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

        public TypeSnakePart getTypeSnakePart()
        {
            return typeSnakePart;
        }
        public void setTypeSnakePart(TypeSnakePart typeSnakePart)
        {
            this.typeSnakePart = typeSnakePart;

            updateUiElement();
        }
    }
}
