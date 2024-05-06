using System;
using System.IO;
using System.Media;

namespace ASCII_Invaders
{
    /// <summary>
    /// Class <c>Game</c> contains objects and methods to control the game
    /// </summary>
    class Game
    {
        public bool PlaySound { get; set; }

        private bool keepRunning;
        public  int aliveEnemies;
        private  int _score;
        public  int Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
                if (_score > _bestScore)
                {
                    BestScore = _score;
                }
            }
        }

        private  int _bestScore;
        public  int BestScore
        {
            get
            {
                return _bestScore;
            }
            set
            {
                _bestScore = value;
                Util.WriteBestScore(BestScore);
            }
        }

        public  int Level { get; set; }
        private  bool enemiesGoLeft;
        private  bool enemiesGoDown;
        private  BattleField battleField;
        private  Cannon cannon;
        private  ConsoleKey keyPressed;

        private  Bullet[] bullets = new Bullet[Constant.Bullets];
        private  Enemy[,] enemies = new Enemy[Constant.EnemiesRows, Constant.EnemiesPerRow];

        private  float enemiesSpeed = 10f;
        private  Random random = new Random();

        /// <summary>
        /// Runs the game
        /// </summary>
        public void Run()
        {
            Init();

            // Game loop
            while (keepRunning)
            {
                if (Level == 0)
                {
                    NextLevel();
                }

                // Check if any key was pressed
                CheckKeypressed();

                // Execute 60 times per second
                Util.Wait(Constant.OneSecond / 60);
                Update();
            }

            // The end
            Finish();
        }

        /// <summary>
        /// Loads enemies to the enemies array
        /// </summary>
        private  void LoadEnemies()
        {
            for (var row = 0; row < Constant.EnemiesRows; row++)
            {
                for (var col = 0; col < Constant.EnemiesPerRow; col++)
                {
                    var enemy = new Enemy();
                    enemy.Position.Y = 3 + row;
                    enemy.Position.X = 10 + 5 * col;
                    enemies[row, col] = enemy;
                }
            }
        }

        /// <summary>
        /// Game initialization
        /// </summary>
        public void Init()
        {
            // Clear screen and hide the cursor
            Console.Clear();
            Console.CursorVisible = false;
            Console.TreatControlCAsInput = true; // disable CTRL-C
            keepRunning = true; // game loop control
            battleField = new BattleField(); // the battle field
            cannon = new Cannon(); // the cannnon

            PlaySound = true;
            battleField.Draw();

            battleField.ShowSplashScreen();

            enemiesGoLeft = true;
            enemiesGoDown = false;
            Score = 0;

            BestScore = Util.ReadBestScore();
        }

        /// <summary>
        /// Plays a sound
        /// </summary>
        /// <param name="file"></param>
        public void PlayWavFile(Stream file)
        {
            if (!PlaySound)
            {
                return;
            }
            // Create new SoundPlayer in the using statement.
            using (SoundPlayer player = new SoundPlayer(file))
            {
                // Use PlaySync to load and then play the sound.
                player.Play();
            }
        }

        /// <summary>
        /// Go to the next level
        /// </summary>
        private void NextLevel()
        {
            if (Level == Constant.FinalLevel)
            {
                // Last level reached - Congratulations
                PlayWavFile(Resource1.congrats);
                battleField.Congratulations();
                Level = 0;
            }

            // Increment level number
            Level++;

            // Load bullets
            for (var b = 0; b < Constant.Bullets; b++)
            {
                bullets[b] = new Bullet();
            }

            // Load enemies
            LoadEnemies();
            aliveEnemies = Constant.EnemiesPerRow * Constant.EnemiesRows;
            battleField.ShowLevelSplashScreen(Level);
        }

        /// <summary>
        /// The end of the game
        /// </summary>
        private  void Finish()
        {
            Console.Clear();
            Console.CursorVisible = true;
            Console.WriteLine("Bye...");
        }

        /// <summary>
        /// Fires a bullet
        /// </summary>
        private  void Shoot()
        {
            foreach (var bullet in bullets)
            {
                if (!bullet.Shot)
                {
                    // found an unfired bullet
                    bullet.Shot = true;
                    bullet.Position.X = cannon.Position.X + 1;
                    bullet.Position.Y = cannon.Position.Y - 1;
                    PlayWavFile(Resource1.hit);
                    return;
                }
            }

        }

        /// <summary>
        /// Check if any key were pressed
        /// </summary>
        private  void CheckKeypressed()
        {
            if (Console.KeyAvailable)
            {
                keyPressed = Console.ReadKey(true).Key;
                switch (keyPressed)
                {
                    case ConsoleKey.LeftArrow:
                        cannon.MoveLeft();
                        break;
                    case ConsoleKey.RightArrow:
                        cannon.MoveRight();
                        break;
                    case ConsoleKey.Spacebar:
                        Shoot();
                        break;
                    case ConsoleKey.M:
                        PlaySound = !PlaySound;
                        break;
                    case ConsoleKey.P:
                        battleField.Pause();
                        break;
                    case ConsoleKey.Escape:
                        if (battleField.Confirm("Press Y to exit or any other key to continue"))
                        {
                            keepRunning = false;
                        }
                        break;
                }
                Update();
            }
        }

        /// <summary>
        /// Randomize the enemies speed
        /// </summary>
        /// <returns>randomized enemy speed</returns>
        private  float RandomizeEnemiesSpeed()
        {
            enemiesSpeed = enemiesSpeed - (float)random.NextDouble() * Level;
            return enemiesSpeed;
        }

        private  bool TheEnemyLanded(Enemy enemy)
        {
            if (enemy.Position.Y == Constant.BattleFieldBottom)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Updates the enemies positions on the battle field
        /// </summary>
        private  void UpdateEnemies()
        {
            var goLeft = enemiesGoLeft;

            var goDown = false;
            enemiesSpeed = Constant.EnemiesTimer;
            for (var row = 0; row < Constant.EnemiesRows; row++)
            {
                for (var col = 0; col < Constant.EnemiesPerRow; col++)
                {
                    if (enemiesGoDown)
                    {
                        enemies[row, col].MoveDown();
                    }
                    if (enemiesGoLeft)
                    {
                        enemies[row, col].MoveLeft();
                    }
                    else
                    {
                        enemies[row, col].MoveRight();
                    }

                    if (TheEnemyLanded(enemies[row, col]))
                    {
                        PlayWavFile(Resource1.game_over);
                        battleField.GameOver(Score);
                        Level = 0;
                        Score = 0;
                        return;
                    }
                    if (enemies[row, col].Visible)
                    {
                        if (enemies[row, col].Position.X == Constant.BattleFieldLeft)
                        {
                            goLeft = false;
                            goDown = true;
                        }
                        if (enemies[row, col].Position.X == Constant.BattleFieldWidth - 2)
                        {
                            goLeft = true;
                            goDown = true;
                        }
                    }
                }
            }
            enemiesGoLeft = goLeft;
            enemiesGoDown = goDown;
        }

        /// <summary>
        /// Check if any enemy has been hitted
        /// </summary>
        /// <param name="bullet"></param>
        /// <returns>True if any enemy has been hitted</returns>
         bool CheckEnemyHit(Bullet bullet)
        {
            for (var row = 0; row < Constant.EnemiesRows; row++)
            {
                for (var col = 0; col < Constant.EnemiesPerRow; col++)
                {
                    if (enemies[row, col].Visible &&
                        enemies[row, col].Position.Y == bullet.Position.Y &&
                        (enemies[row, col].Position.X == bullet.Position.X ||
                         enemies[row, col].Position.X + 1 == bullet.Position.X ||
                         enemies[row, col].Position.X + 2 == bullet.Position.X
                        ))
                    {
                        Score += Level * row;
                        PlayWavFile(Resource1.explosion);
                        enemies[row, col].Destroy();
                        aliveEnemies--;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Updates the bullets positions on the battle field
        /// </summary>
         void UpdateBullets()
        {
            for (var b = 0; b < Constant.Bullets; b++)
            {
                if (bullets[b].Shot)
                {
                    bullets[b].Draw();
                    if (CheckEnemyHit(bullets[b]))
                    {
                        bullets[b].Shot = false;
                    }
                    Util.Wait(17);
                    bullets[b].Clear();
                    if (bullets[b].Position.Y-- == Constant.BattleFieldTop)
                    {
                        bullets[b].Shot = false;
                    }
                }
            }
        }

        /// <summary>
        /// Updates the game screen
        /// </summary>
        void Update()      
         {
            cannon.Draw();
            if (aliveEnemies == 0)
            {
                NextLevel();
            }
            if (RandomizeEnemiesSpeed() < 5f)
            {
                UpdateEnemies();
            }
            UpdateBullets();
            battleField.UpdateStatusBar(PlaySound, Level, Score, BestScore);
        }
    }
}
