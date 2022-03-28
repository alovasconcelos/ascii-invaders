using System;

namespace ASCII_Invaders
{
    class Cannon : GameObject
    {
        public Cannon()
        {
            Color = ConsoleColor.Blue;
            Sprite = "^-^";
            Position = new Position((Constant.BattleFieldWidth - 2) / 2 - 1, Constant.BattleFieldBottom);
            PreviousPosition = new Position((Constant.BattleFieldWidth - 2) / 2 - 1, Constant.BattleFieldBottom);
        }

    }
}
