using System.Collections;
using System.Windows.Input;

namespace ProjetSnake2019.Classes
{
    internal class EntreeClavier
    {
        // Liste des touches du clavier disponibles
        private static Hashtable tableTouche = new Hashtable();

        /// <summary>
        /// Verifie si une touche du clavier spécifique à été appuyée.
        /// </summary>
        public static bool estTouchePressee(Key touche)
        {
            if (tableTouche[touche] == null)
            {
                return false;
            }

            return (bool)tableTouche[touche];
        }

        /// <summary>
        /// Détecte si une touche du clavier est pressée.
        /// </summary>
        public static void changerEtatTouche(Key touche, bool etat)
        {
            tableTouche[touche] = etat;
        }

    }
}
