using System;
using System.Linq;
using System.Threading;

namespace ASCII_Invaders
{
    class Program
    {
        public static int screenTop = Console.CursorTop;
        public static int screenLeft = Console.CursorLeft;

        private static bool keepRunning;
        private static bool _playSound;

        public static bool PlaySound
        {
            get
            {
                return _playSound;
            }
            set
            {
                _playSound = value;
                Util.WriteAt(Constant.BattleFieldSoundStatusCol, Constant.BattleFieldStatusBar, _playSound ? "On " : "Off");
            }
        }

        private static int _score;
        public static int Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
                Util.WriteAt(Constant.BattleFieldScoreCol, Constant.BattleFieldStatusBar, Score.ToString().PadLeft(6, '0'));
                if (_score > _bestScore)
                {
                    BestScore = _score;
                }
            }
        }

        private static int _bestScore;

        public static int BestScore
        {
            get
            {
                return _bestScore;
            }
            set
            {
                _bestScore = value;
                Util.WriteAt(Constant.BattleFieldBestScoreCol, Constant.BattleFieldStatusBar, _bestScore.ToString().PadLeft(6, '0'));
                Util.WriteBestScore();
            }
        }

        private static int _level;

        public static int Stage
        {
            get
            {
                return _level;
            }
            set
            {
                _level = value;
                Util.WriteAt(Constant.BattleFieldStageCol, Constant.BattleFieldStatusBar, _level.ToString());
            }
        }

        private static bool enemiesGoLeft;
        private static bool enemiesGoDown;

        private static BattleField battleField;
        private static Cannon cannon;
        private static ConsoleKey keyPressed;

        private static Bullet[] bullets = new Bullet[Constant.Bullets];
        private static Enemy[,] enemies = new Enemy[Constant.EnemiesRows, Constant.EnemiesPerRow];

        private static int enemiesTick = Constant.EnemiesTimer;

        private static void LoadEnemies()
        {
            for(var row = 0; row < Constant.EnemiesRows; row++)
            {
                for(var col = 0; col < Constant.EnemiesPerRow; col++)
                {
                    var enemy = new Enemy();
                    enemy.YPos = 3 + row;
                    enemy.XPos = 10 + 5 * col;
                    enemies[row, col] = enemy;
                }
            }
        }

        private static void Init()
        {
            // Clear screen and hide the cursor
            Console.Clear();
            Console.CursorVisible = false;
            Console.TreatControlCAsInput = true; // disable CTRL-C
            keepRunning = true; // game loop control
            battleField = new BattleField(); // the battle field
            cannon = new Cannon(); // the cannnon
            
            // Bullets
            for (var b = 0; b < Constant.Bullets; b++)
            {
                bullets[b] = new Bullet();
            }

            LoadEnemies();
            enemiesGoLeft = true;
            enemiesGoDown = false;
            PlaySound = true;
            battleField.Draw();
            Score = 0;
            Stage = 1;
            BestScore = Util.ReadBestScore();
        }

        private static void Finish()
        {
            Console.Clear();
            Console.CursorVisible = true;
            Console.WriteLine("Bye...");
        }

        private static void Shoot()
        {
            foreach(var bullet in bullets)
            {
                if (!bullet.Shot)
                {
                    // found an unfired bullet
                    bullet.Shot = true;
                    bullet.XPos = cannon.XPos + 1;
                    bullet.YPos = cannon.YPos - 1;
                    Util.PlaySound(Constant.ShotSound);
                    return;
                }
            }
            // no bullet available to shoot

        }

        private static void CheckKeypressed()
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
                }
            }
        }

        private static void UpdateEnemies()
        {
            if (enemiesTick-- > 0) {
                return;
            }
            enemiesTick = Constant.EnemiesTimer;
            for (var row = 0; row < Constant.EnemiesRows; row++)
            {
                for (var col = 0; col < Constant.EnemiesPerRow; col++)
                {
                    if (enemies[row, col].Alive)
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
                    }
                }
            }
            enemiesGoDown = false;
            if (enemies[0, 0].XPos == 1)
            {
                enemiesGoLeft = false;
                enemiesGoDown = true;
            }
            if (enemies[0, 0].XPos == 18)
            {
                enemiesGoLeft = true;
                enemiesGoDown = true;
            }
        }

        static bool CheckEnemyHit(Bullet bullet)
        {
            for (var row = 0; row < Constant.EnemiesRows; row++)
            {
                for (var col = 0; col < Constant.EnemiesPerRow; col++)
                {
                    var enemy = enemies[row, col];
                    if (enemy.Alive &&
                        enemy.YPos == bullet.YPos &&
                        (enemy.XPos == bullet.XPos ||
                         enemy.XPos + 1 == bullet.XPos ||
                         enemy.XPos + 2 == bullet.XPos
                        ))
                    {
                        enemies[row, col].Alive = false;
                        enemies[row, col].Clear();
                        Score++;
                        Util.PlaySound(Constant.ExplodionSound);
                        return true;
                    }
                }
            }
            return false;
        }

         static void UpdateBullets()
        {
            for(var b = 0; b < Constant.Bullets; b++)
            {
                
                if (bullets[b].Shot)
                {
                    bullets[b].Draw();
                    if (CheckEnemyHit(bullets[b]))
                    {
                        bullets[b].Shot = false;
                    }
                    Thread.Sleep(5);
                    bullets[b].Clear();
                    if (bullets[b].YPos-- == Constant.BattleFieldTop)
                    {
                        bullets[b].Shot = false;
                    }
                }                
            }
        }
        static void Main(string[] args)
        {
            Init();
            
            // Game loop
            while (keepRunning)
            {
                cannon.Draw();
                UpdateBullets();
                Thread.Sleep(20);
                UpdateEnemies();
                CheckKeypressed();
             }

            // The end
            Finish();
        }
    }
}
