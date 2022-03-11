namespace ASCII_Invaders
{
    class Enemy : GameObject
    {
        public bool Alive { get; set; }
        public Enemy()
        {
            Sprite = ">o<";
            Alive = true;
        }
        public void Draw()
        {
            if (Alive)
            {
                base.Draw();
            }
        }
    }
}
