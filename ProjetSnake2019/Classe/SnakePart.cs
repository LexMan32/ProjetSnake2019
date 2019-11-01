using ProjetSnake2019.Enumeration;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ProjetSnake2019.Classe
{
    /// <summary>
    /// Classe d'une partie du serpent
    /// </summary>
    public class SnakePart
    {
        // Champs de la classe
        private UIElement uiElement;
        private Point position;
        private TypeSnakePart typeSnakePart;
        private Direction direction;

        /// <summary>
        /// Constructeur d'une nouvelle partie du serpent.
        /// </summary>
        /// <param name="position">Position de la partie du serpent</param>
        /// <param name="typeSnakePart">Type de la partie du serpent</param>
        /// <param name="direction">Direction de la partue du serpent</param>
        public SnakePart(Point position, TypeSnakePart typeSnakePart, Direction direction)
        {
            this.position = position;
            this.typeSnakePart = typeSnakePart;
            this.direction = direction;

            // Mise à jour de l'UI
            updateUiElement();
        }

        /// <summary>
        /// Mise à jour de l'UI de la partie du serpent.
        /// </summary>
        private void updateUiElement()
        {
            switch (typeSnakePart)
            {
                case TypeSnakePart.Head:
                    uiElement = new Image()
                    {
                        Source = new BitmapImage(new Uri(Configuration.PATH_IMG_SNAKE_HEAD)),
                        Width = Configuration.SNAKE_SQUARE_SIZE,
                        Height = Configuration.SNAKE_SQUARE_SIZE
                    };
                    break;
                case TypeSnakePart.Body:
                    uiElement = new Image()
                    {
                        Source = new BitmapImage(new Uri(Configuration.PATH_IMG_SNAKE_BODY)),
                        Width = Configuration.SNAKE_SQUARE_SIZE,
                        Height = Configuration.SNAKE_SQUARE_SIZE
                    };
                    break;
                case TypeSnakePart.Tail:
                    uiElement = new Image()
                    {
                        Source = new BitmapImage(new Uri(Configuration.PATH_IMG_SNAKE_TAIL)),
                        Width = Configuration.SNAKE_SQUARE_SIZE,
                        Height = Configuration.SNAKE_SQUARE_SIZE
                    };
                    break;
            }
        }

        /// <summary>
        /// Getter pour l'élément UI.
        /// </summary>
        /// <returns>Elément UI</returns>
        public UIElement getUiElement()
        {
            return uiElement;
        }

        /// <summary>
        /// Getter pour la position de l'élément.
        /// </summary>
        /// <returns>Position de l'élément</returns>
        public Point getPosition()
        {
            return position;
        }

        /// <summary>
        /// Getter pour le type de l'élément.
        /// </summary>
        /// <returns>Type de l'élément</returns>
        public TypeSnakePart getTypeSnakePart()
        {
            return typeSnakePart;
        }

        /// <summary>
        /// Setter pour le type de l'élément.
        /// </summary>
        /// <param name="typeSnakePart">Type de l'élément</param>
        public void setTypeSnakePart(TypeSnakePart typeSnakePart)
        {
            this.typeSnakePart = typeSnakePart;

            // Mise à jour de l'UI
            updateUiElement();
        }

        /// <summary>
        /// Getter pour la direction de l'élément.
        /// </summary>
        /// <returns>Direction de l'élément</returns>
        public Direction getDirection()
        {
            return direction;
        }
    }
}
