using System;
using System.Collections.Generic;
using System.Text;

namespace ASCII_Invaders
{
    class Bullet : GameObject
    {
        public bool Shot { get; set; }
        public Bullet()
        {
            Sprite = "^";
            Shot = false;
        }
    }
}
