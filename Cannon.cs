using System;
using System.Collections.Generic;
using System.Text;

namespace ASCII_Invaders
{
    class Cannon : GameObject
    {
        public Cannon()
        {
            Sprite = "^-^";
            YPos = Constant.BattleFieldBottom - 1;
            XPos = (Constant.BattleFieldWidth - 2) / 2 - 1;
            
        }

    }
}
