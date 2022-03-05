using System;
using System.Collections.Generic;
using System.Text;

namespace ASCII_Invaders
{
    class BattleField
    {
        public void Draw()
        {
            Util.WriteAt(0, 0, "+--------------------------------------------------+");
            for (int row = 1; row < Constant.BattleFieldBottom; row++)
            {
                Util.WriteAt(0, row, "|                                                  |");
            }
            Util.WriteAt(0, Constant.BattleFieldBottom, "+--------------------------------------------------+");
        }
    }
}
