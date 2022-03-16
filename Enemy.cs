namespace ASCII_Invaders
{
    class Enemy : GameObject
    {
        public bool Alive { get; set; }
        public Enemy(string sprite = ">o<", int xPos = 0, int yPos = 0) :
            base(sprite, xPos, yPos)
        {
            Alive = true;
        }


        public override void Draw()
        {
            if (!Alive)
            {
                Clear();
                return;
            }
            base.Draw();
        }
    }
}
