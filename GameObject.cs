using System.Linq;

namespace ASCII_Invaders
{
    public class GameObject
    {
        public string Sprite { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }

        public bool Visible { get; set; }

        public GameObject(string sprite = " ", int xPos = 0, int yPos = 0, bool visible = true)
        {
            Sprite = sprite;
            XPos = xPos;
            YPos = yPos;
            Visible = visible;
        }

        public void Draw()
        {
            if  (Visible)
            {
                Util.WriteAt(XPos, YPos, Sprite);
            }
        }

        public void Clear()
        {
            if (Visible)
            {
                Util.WriteAt(XPos, YPos, string.Concat(Enumerable.Repeat(" ", Sprite.Length)));
            }
        }

        public bool MoveLeft()
        {
            if (XPos > 1)
            {
                XPos--;
                return true;
            }
            return false;
        }

        public bool MoveRight()
        {
            if (XPos < 51 - Sprite.Length)
            {
                XPos++;
                return true;
            }
            return false;
        }
        public bool MoveDown()
        {
            if (YPos < Constant.BattleFieldBottom)
            {
                YPos++;
                return true;
            }
            return false;
        }
    }
}
