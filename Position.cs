using System;
using System.Collections.Generic;
using System.Text;

namespace ASCII_Invaders
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void MoveLeft()
        {
            if (X > Constant.BattleFieldLeft)
            {
                X--;
            }
        }

        public void MoveRight()
        {
            if (X < Constant.BattleFieldWidth)
            {
                X++;
            }
        }

        public void MoveUp()
        {
            if (Y > Constant.BattleFieldTop)
            {
                Y--;
            }
        }

        public void MoveDown()
        {
            if (Y < Constant.BattleFieldBottom)
            {
                Y++;
            }
        }

        public void SetPosition(Position pos)
        {
            X = pos.X;
            Y = pos.Y;
        }
    }
}
