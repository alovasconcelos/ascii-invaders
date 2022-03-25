using System;
using System.Linq;

namespace ASCII_Invaders
{
    public class GameObject
    {
        public string Sprite { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }
        public bool Visible { get; set; }
        public ConsoleColor Color { get; set; }

        public GameObject()
        {
            Sprite = " ";
            XPos = 0;
            YPos = 0;
            Visible = true;
            Color = ConsoleColor.White;

        }

        public void Draw()
        {
            if  (Visible)
            {
                Util.WriteAt(XPos, YPos, Sprite, Color);
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
                Clear();
                XPos--;
                return true;
            }
            return false;
        }

        public bool MoveRight()
        {
            if (XPos < 51 - Sprite.Length)
            {
                Clear();
                XPos++;
                return true;
            }
            return false;
        }
        public bool MoveDown()
        {
            if (YPos < Constant.BattleFieldBottom)
            {
                Clear();
                YPos++;
                return true;
            }
            return false;
        }
    }
}
