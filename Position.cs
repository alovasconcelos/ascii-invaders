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

        /// <summary>
        /// Moves position left
        /// </summary>
        public void MoveLeft()
        {
            if (X > Constant.BattleFieldLeft)
            {
                X--;
            }
        }

        /// <summary>
        /// Moves position right
        /// </summary>
        public void MoveRight()
        {
            if (X < Constant.BattleFieldWidth - 1)
            {
                X++;
            }
        }

        /// <summary>
        /// Moves position up
        /// </summary>
        public void MoveUp()
        {
            if (Y > Constant.BattleFieldTop)
            {
                Y--;
            }
        }

        /// <summary>
        /// Moves position down
        /// </summary>
        public void MoveDown()
        {
            if (Y < Constant.BattleFieldBottom)
            {
                Y++;
            }
        }

        /// <summary>
        /// Sets absolute position
        /// </summary>
        /// <param name="pos"></param>
        public void SetPosition(Position pos)
        {
            X = pos.X;
            Y = pos.Y;
        }
    }
}
