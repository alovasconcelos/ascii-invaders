using System;

namespace ASCII_Invaders
{
    class Cannon : GameObject
    {
        public Cannon()
        {
            Color = ConsoleColor.Blue;
            Sprite = "^-^";
            YPos = Constant.BattleFieldBottom;
            XPos = (Constant.BattleFieldWidth - 2) / 2 - 1;            
        }

    }
}
