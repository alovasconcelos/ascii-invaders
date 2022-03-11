using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASCII_Invaders
{
    public class GameObject
    {
        public string Sprite { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }

        public void Draw()
        {
            Util.WriteAt(XPos, YPos, Sprite);
        }

        public void Clear()
        {
            Util.WriteAt(XPos, YPos, string.Concat(Enumerable.Repeat(" ", Sprite.Length)));
        }

        public void MoveLeft()
        {
            if (XPos > 1)
            {
                Clear();
                XPos--;
                Draw();
            }
        }

        public void MoveRight()
        {
            if (XPos < 51 - Sprite.Length)
            {
                Clear();
                XPos++;
                Draw();
            }
        }
        public void MoveDown()
        {
            if (YPos < Constant.BattleFieldBottom)
            {
                Clear();
                YPos++;
                Draw();
            }
        }

    }
}
