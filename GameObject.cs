using System;
using System.Linq;

namespace ASCII_Invaders
{
    public class GameObject
    {
        public string Sprite { get; set; }
        public Position Position { get; set; }
        public Position PreviousPosition { get; set; }
        public bool Visible { get; set; }
        public ConsoleColor Color { get; set; }

        public GameObject()
        {
            Sprite = " ";
            Position = new Position(0, 0);
            PreviousPosition = new Position(0, 0);
            Visible = true;
            Color = ConsoleColor.White;
        }

        /// <summary>
        /// Draws the game object
        /// </summary>
        public void Draw()
        {
            if  (Visible)
            {
                // If there's a previous position set, then clear that 
                if (PreviousPosition.X > 0)
                {
                    Util.WriteAt(PreviousPosition.X, PreviousPosition.Y, " ");
                }
                Util.WriteAt(Position.X, Position.Y, Sprite, Color);
                PreviousPosition.SetPosition(Position);
            }
        }

        /// <summary>
        /// Clears the game object
        /// </summary>
        public void Clear()
        {
            if (Visible)
            {
                Util.WriteAt(Position.X, Position.Y, " ");
            }
        }

        /// <summary>
        /// Moves the game object left
        /// </summary>
        public void MoveLeft()
        {
            Position.MoveLeft();
            Draw();
        }

        /// <summary>
        /// Moves the game object right
        /// </summary>
        public void MoveRight()
        {
            Position.MoveRight();
            Draw();
        }

        /// <summary>
        /// Moves the game object up
        /// </summary>
        public void MoveUp()
        {
            Position.MoveUp();
            Draw();
        }

        /// <summary>
        /// Moves the game object down
        /// </summary>
        public void MoveDown()
        {
            Position.MoveDown();
            Draw();
        }
    }
}
