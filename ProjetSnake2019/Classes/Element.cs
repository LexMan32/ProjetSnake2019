using ProjetSnake2019.Enumerations;

namespace ProjetSnake2019.Classes
{
    public class Element
    {
        private int positionX;
        private int positionY;
        private Direction direction;
        private TypeElement typeElement;

        public Element(int positionX, int positionY, Direction direction, TypeElement typeElement)
        {
            this.positionX = positionX;
            this.positionY = positionY;
            this.direction = direction;
            this.typeElement = typeElement;
        }

        public int getPositionX()
        {
            return positionX;
        }
        public void setPositionX(int positionX)
        {
            this.positionX = positionX;
        }

        public int getPositionY()
        {
            return positionY;
        }
        public void setPositionY(int positionY)
        {
            this.positionY = positionY;
        }

        public Direction getDirection()
        {
            return direction;
        }
        public void setDirection(Direction direction)
        {
            this.direction = direction;
        }

        public TypeElement getTypeElement()
        {
            return typeElement;
        }
        public void setTypeElement(TypeElement typeElement)
        {
            this.typeElement = typeElement;
        }
    }
}
