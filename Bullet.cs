using System;

namespace ASCII_Invaders
{
    class Bullet : GameObject
    {
        public bool Shot { get; set; }
        public Bullet()
        {
            Sprite = "^";
            Shot = false;
            Color = ConsoleColor.Yellow;
        }
    }
}
