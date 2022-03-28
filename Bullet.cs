using System;

namespace ASCII_Invaders
{
    /// <summary>
    /// Class <c>Bullet</c> refers to... the bullets :)
    /// </summary>
    class Bullet : GameObject
    {
        public bool Shot { get; set; }
        /// <summary>
        /// Method <c>constructor</c>
        /// </summary>
        public Bullet()
        {
            Sprite = "^";
            Shot = false;
            Color = ConsoleColor.Yellow;
        }
    }
}
