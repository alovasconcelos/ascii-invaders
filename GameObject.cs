﻿using System.Linq;

namespace ASCII_Invaders
{
    public class GameObject
    {
        public string Sprite { get; set; }
        public int XPos { get; set; }
        public int YPos { get; set; }

        public GameObject(string sprite = " ", int xPos = 0, int yPos = 0)
        {
            Sprite = sprite;
            XPos = xPos;
            YPos = yPos;
        }

        public virtual void Draw()
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
