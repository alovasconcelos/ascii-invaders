using System;

namespace ASCII_Invaders
{
    class Enemy : GameObject
    {
        public Enemy() 
        {
            Sprite = ">o<";
            Color = ConsoleColor.Green;
        }

        public void Destroy()
        {
            Color = ConsoleColor.DarkGray;
            Draw();
            Util.Wait(10);
            Color = ConsoleColor.DarkYellow;
            Draw();
            Util.Wait(10);
            Color = ConsoleColor.Yellow;
            Draw();
            Util.Wait(10);
            Color = ConsoleColor.Red;
            Draw();
            Util.Wait(10);
            Clear();
            Visible = false;

        }
    }
}
