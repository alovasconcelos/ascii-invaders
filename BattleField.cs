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
            Util.WriteAt(0, 1, "|                  ASCII INVADERS                  |");
            Util.WriteAt(0, 2, "+--------------------------------------------------+");
            for (int row = Constant.BattleFieldTop; row <= Constant.BattleFieldBottom; row++)
            {
                Util.WriteAt(0, row, "|                                                  |");
            }
            Util.WriteAt(0, Constant.BattleFieldBottom + 1, "+--------------------------------------------------+");
            Util.WriteAt(0, Constant.BattleFieldStatusBar , "|Sound:on |Stage: |Score:       |Best Score:       |");
            Util.WriteAt(0, Constant.BattleFieldStatusBar + 1, "+--------------------------------------------------+");
        }
    }
}
