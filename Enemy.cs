using System;

namespace ASCII_Invaders
{
    /// <summary>
    /// Class <c>GameObject</c> is the base class for game objects
    /// </summary>
    class Enemy : GameObject
    {
        /// <summary>
        /// Method <c>constructor</c>
        /// </summary>
        public Enemy() 
        {
            Sprite = "👾";
            Color = ConsoleColor.Green;
        }

        /// <summary>
        /// Method <c>Destroy</c>draws a little animation and clears the enemy from screen
        /// </summary>
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