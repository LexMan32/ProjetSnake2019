using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ProjetSnake2019.Classe
{
    /// <summary>
    /// Classe d'une nourriture pour le serpent
    /// </summary>
    public class SnakeFood
    {
        // Champs de la classe
        private UIElement uiElement;
        private Point position;

        /// <summary>
        /// Constructeur d'une nouvelle nourriture pour le serpent.
        /// </summary>
        /// <param name="position">Position de la nourriture</param>
        public SnakeFood(Point position)
        {
            this.position = position;

            // Mise à jour de l'UI
            uiElement = new Image()
            {
                Source = new BitmapImage(new Uri(Configuration.PATH_IMG_APPLE)),
                Width = Configuration.SNAKE_SQUARE_SIZE,
                Height = Configuration.SNAKE_SQUARE_SIZE
            };
        }

        /// <summary>
        /// Getter pour l'élément UI.
        /// </summary>
        /// <returns>Elément UI</returns>
        public UIElement GetUiElement()
        {
            return uiElement;
        }

        /// <summary>
        /// Getter pour la position de la nourriture.
        /// </summary>
        /// <returns>Position de la nourriture</returns>
        public Point GetPosition()
        {
            return position;
        }
    }
}
