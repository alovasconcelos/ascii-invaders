using System;
using System.Threading;

namespace ASCII_Invaders
{
    class Program
    {
        public static int screenTop = Console.CursorTop;
        public static int screenLeft = Console.CursorLeft;

        private static bool keepRunning;
        private static bool _playSound;
        private static float EnemiesTimer = 10;

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

        private static int _level = 0;

        public static int Level
        {
            get
            {
                return _level;
            }
            set
            {
                _level = value;
                Util.WriteAt(Constant.BattleFieldLevelCol, Constant.BattleFieldStatusBar, _level.ToString());
            }
        }

        private static bool enemiesGoLeft;
        private static bool enemiesGoDown;

        private static BattleField battleField;
        private static Cannon cannon;
        private static ConsoleKey keyPressed;

        private static Bullet[] bullets = new Bullet[Constant.Bullets];
        private static Enemy[,] enemies = new Enemy[Constant.EnemiesRows, Constant.EnemiesPerRow];

        private static float enemiesTick = 10f;
        private static Random random = new Random();

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
            
            PlaySound = true;
            battleField.Draw();

            ShowSplashScreen();

            enemiesGoLeft = true;
            enemiesGoDown = false;
            Score = 0;  

            BestScore = Util.ReadBestScore();
        }

        private static void NextLevel()
        {
            if (Level == Constant.FinalLevel)
            {
                // Last level reached - Congratulations
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

            ShowLevelSplashScreen();
        }

        private static void ShowSplashScreen()
        {
            for (var row = Constant.BattleFieldBottom - 13; row > Constant.BattleFieldTop; row--)
            {
                Util.WriteAt(7, row, "╔═══╗        ╔══╗╔══╗");
                Util.WriteAt(7, row + 1, "║╔═╗║        ╚╣╠╝╚╣╠╝");
                Util.WriteAt(7, row + 2, "║║ ║║╔══╗╔══╗ ║║  ║║ ");
                Util.WriteAt(7, row + 3, "║╚═╝║║══╣║╔═╝ ║║  ║║ ");
                Util.WriteAt(7, row + 4, "║╔═╗║╠══║║╚═╗╔╣╠╗╔╣╠╗");
                Util.WriteAt(7, row + 5, "╚╝ ╚╝╚══╝╚══╝╚══╝╚══╝");
                Util.WriteAt(7, row + 6, "       ╔══╗               ╔╗           ");
                Util.WriteAt(7, row + 7, "       ╚╣╠╝               ║║           ");
                Util.WriteAt(7, row + 8, "        ║║ ╔═╗ ╔╗╔╗╔══╗ ╔═╝║╔══╗╔═╗╔══╗");
                Util.WriteAt(7, row + 9, "        ║║ ║╔╗╗║╚╝║╚ ╗║ ║╔╗║║╔╗║║╔╝║══╣");
                Util.WriteAt(7, row + 10, "       ╔╣╠╗║║║║╚╗╔╝║╚╝╚╗║╚╝║║║═╣║║ ╠══║");
                Util.WriteAt(7, row + 11, "       ╚══╝╚╝╚╝ ╚╝ ╚═══╝╚══╝╚══╝╚╝ ╚══╝");
                Util.WriteAt(7, row + 12, "               alovasconcelos.github.io");
                Util.WriteAt(7, row + 13, "                                       ");
                Thread.Sleep(Constant.OneSecond / 10);
            }
            Thread.Sleep(Constant.OneSecond * 3);
            ClearBattleField();
        }

        private static void GameOver()
        {
            ClearBattleField();
            for (var row = Constant.BattleFieldBottom - 12; row > Constant.BattleFieldTop; row--)
            {
                Util.WriteAt(7, row, "╔═══╗             ");
                Util.WriteAt(7, row + 1, "║╔═╗║             ");
                Util.WriteAt(7, row + 2, "║║ ╚╝╔══╗ ╔╗╔╗╔══╗");
                Util.WriteAt(7, row + 3, "║║╔═╗╚ ╗║ ║╚╝║║╔╗║");
                Util.WriteAt(7, row + 4, "║╚╩═║║╚╝╚╗║║║║║║═╣");
                Util.WriteAt(7, row + 5, "╚═══╝╚═══╝╚╩╩╝╚══╝");
                Util.WriteAt(7, row + 6, "        ╔══╗╔╗╔╗╔══╗╔═╗");
                Util.WriteAt(7, row + 7, "        ║╔╗║║╚╝║║╔╗║║╔╝");
                Util.WriteAt(7, row + 8, "        ║╚╝║╚╗╔╝║║═╣║║ ");
                Util.WriteAt(7, row + 9, "        ╚══╝ ╚╝ ╚══╝╚╝ ");
                Util.WriteAt(7, row + 10, "                       ");
                Util.WriteAt(7, row + 11, "    Your score: " + Score.ToString().PadLeft(6, '0'));
                Util.WriteAt(7, row + 12, "                       ");
                Thread.Sleep(Constant.OneSecond / 10);
            }
            Thread.Sleep(Constant.OneSecond * 5);
            Level = 0;
            Score = 0;
            ClearBattleField();
        }

        private static void ShowLevelSplashScreen()
        {
            for (var row = Constant.BattleFieldBottom - 6; row > Constant.BattleFieldTop; row--)
            {
                                          
                Util.WriteAt(7, row,     "#       ######  #    #  ######  #     ");
                Util.WriteAt(7, row + 1, "#       #       #    #  #       #     ");
                Util.WriteAt(7, row + 2, "#       #####   #    #  #####   #     ");
                Util.WriteAt(7, row + 3, "#       #       #    #  #       #     ");
                Util.WriteAt(7, row + 4, "#       #        #  #   #       #     ");
                Util.WriteAt(7, row + 5, "######  ######    ##    ######  ######");
                Util.WriteAt(7, row + 6, "                                      ");
                Thread.Sleep(Constant.OneSecond / 10);
            }

            switch (Level)
            {
                case 1:
                                                                   
                    Util.WriteAt(23, Constant.BattleFieldTop + 8,  "  #");
                    Util.WriteAt(23, Constant.BattleFieldTop + 9,  " ##");
                    Util.WriteAt(23, Constant.BattleFieldTop + 10, "# #");
                    Util.WriteAt(23, Constant.BattleFieldTop + 11, "  #");
                    Util.WriteAt(23, Constant.BattleFieldTop + 12, "  #");
                    Util.WriteAt(23, Constant.BattleFieldTop + 13, "  #");
                    Util.WriteAt(23, Constant.BattleFieldTop + 14, "#####");
                    break;
                case 2:
                    Util.WriteAt(22, Constant.BattleFieldTop + 8, " ##### ");
                    Util.WriteAt(22, Constant.BattleFieldTop + 9, "#     #");
                    Util.WriteAt(22, Constant.BattleFieldTop + 10, "      #");
                    Util.WriteAt(22, Constant.BattleFieldTop + 11, " ##### ");
                    Util.WriteAt(22, Constant.BattleFieldTop + 12, "#      ");
                    Util.WriteAt(22, Constant.BattleFieldTop + 13, "#      ");
                    Util.WriteAt(22, Constant.BattleFieldTop + 14, "#######");
                    break;
                case 3:
                    Util.WriteAt(22, Constant.BattleFieldTop + 8, " ##### ");
                    Util.WriteAt(22, Constant.BattleFieldTop + 9, "#     #");
                    Util.WriteAt(22, Constant.BattleFieldTop + 10, "      #");
                    Util.WriteAt(22, Constant.BattleFieldTop + 11, " ##### ");
                    Util.WriteAt(22, Constant.BattleFieldTop + 12, "      #");
                    Util.WriteAt(22, Constant.BattleFieldTop + 13, "#     #");
                    Util.WriteAt(22, Constant.BattleFieldTop + 14, " ##### ");
                    break;
                case 4:
                    Util.WriteAt(22, Constant.BattleFieldTop + 8, "#     ");
                    Util.WriteAt(22, Constant.BattleFieldTop + 9, "#    #");
                    Util.WriteAt(22, Constant.BattleFieldTop + 10, "#    #");
                    Util.WriteAt(22, Constant.BattleFieldTop + 11, "#######");
                    Util.WriteAt(22, Constant.BattleFieldTop + 12, "     #");
                    Util.WriteAt(22, Constant.BattleFieldTop + 13, "     #");
                    Util.WriteAt(22, Constant.BattleFieldTop + 14, "     #");
                    break;
                case 5:
                    Util.WriteAt(22, Constant.BattleFieldTop + 8, "#######");
                    Util.WriteAt(22, Constant.BattleFieldTop + 9, "#      ");
                    Util.WriteAt(22, Constant.BattleFieldTop + 10, "#      ");
                    Util.WriteAt(22, Constant.BattleFieldTop + 11, " ##### ");
                    Util.WriteAt(22, Constant.BattleFieldTop + 12, "      #");
                    Util.WriteAt(22, Constant.BattleFieldTop + 13, "#     #");
                    Util.WriteAt(22, Constant.BattleFieldTop + 14, " ##### ");
                    break;
                case 6:
                    Util.WriteAt(22, Constant.BattleFieldTop + 8, " ##### ");
                    Util.WriteAt(22, Constant.BattleFieldTop + 9, "#     #");
                    Util.WriteAt(22, Constant.BattleFieldTop + 10, "#      ");
                    Util.WriteAt(22, Constant.BattleFieldTop + 11, "###### ");
                    Util.WriteAt(22, Constant.BattleFieldTop + 12, "#     #");
                    Util.WriteAt(22, Constant.BattleFieldTop + 13, "#     #");
                    Util.WriteAt(22, Constant.BattleFieldTop + 14, " ##### ");
                    break;
                case 7:
                    Util.WriteAt(22, Constant.BattleFieldTop + 8, "#######");
                    Util.WriteAt(22, Constant.BattleFieldTop + 9, "#    # ");
                    Util.WriteAt(22, Constant.BattleFieldTop + 10, "    #  ");
                    Util.WriteAt(22, Constant.BattleFieldTop + 11, "   #   ");
                    Util.WriteAt(22, Constant.BattleFieldTop + 12, "  #    ");
                    Util.WriteAt(22, Constant.BattleFieldTop + 13, "  #    ");
                    Util.WriteAt(22, Constant.BattleFieldTop + 14, "  #    ");
                    break;
                case 8:
                    Util.WriteAt(22, Constant.BattleFieldTop + 8, " ##### ");
                    Util.WriteAt(22, Constant.BattleFieldTop + 9, "#     #");
                    Util.WriteAt(22, Constant.BattleFieldTop + 10, "#     #");
                    Util.WriteAt(22, Constant.BattleFieldTop + 11, " ##### ");
                    Util.WriteAt(22, Constant.BattleFieldTop + 12, "#     #");
                    Util.WriteAt(22, Constant.BattleFieldTop + 13, "#     #");
                    Util.WriteAt(22, Constant.BattleFieldTop + 14, " ##### ");
                    break;
                case 9:
                    Util.WriteAt(22, Constant.BattleFieldTop + 8, " ##### ");
                    Util.WriteAt(22, Constant.BattleFieldTop + 9, "#     #");
                    Util.WriteAt(22, Constant.BattleFieldTop + 10, "#     #");
                    Util.WriteAt(22, Constant.BattleFieldTop + 11, " ######");
                    Util.WriteAt(22, Constant.BattleFieldTop + 12, "      #");
                    Util.WriteAt(22, Constant.BattleFieldTop + 13, "#     #");
                    Util.WriteAt(22, Constant.BattleFieldTop + 14, " ##### ");
                    break;
            }
            Thread.Sleep(Constant.OneSecond * 2);
            ClearBattleField();
        }

        private static void ClearBattleField()
        {
            for (var row = Constant.BattleFieldBottom; row >= Constant.BattleFieldTop; row--)
            {
                Util.WriteAt(1, row, "                                                  ");
                Thread.Sleep(Constant.OneSecond / 20);
            }
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
                    Util.PlaySound(Resource1.hit);
                    return;
                }
            }
            // no bullet available to shoot

        }

        private static void ConfirmExit()
        {
            // Clear the battlefield
            ClearBattleField();
            Util.WriteAt(4, 10, "Press Y to exit or any other key to continue");
            if (Console.ReadKey().Key.Equals(ConsoleKey.Y)) {
                keepRunning = false;
            }
            Util.WriteAt(4, 10, "                                            ");
            cannon.Draw();
            UpdateBullets();
            UpdateEnemies();
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
                    case ConsoleKey.Escape:
                        ConfirmExit();
                        break;
                }
            }
        }

        private static float RandomizeEnemiesSpeed()
        {
            enemiesTick = enemiesTick - (float)random.NextDouble() * _level;
            return enemiesTick;
        }

        private static bool TheEnemyLanded(int row)
        {
            if (row == Constant.BattleFieldBottom)
            {
                return true;
            }
            return false;
        }

        private static void UpdateEnemies()
        {
            if (RandomizeEnemiesSpeed() > 0)
            {
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

                        if (TheEnemyLanded(enemies[row, col].YPos))
                        {
                            GameOver();
                            return;
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
            if (enemies[0, Constant.EnemiesPerRow -1].XPos == 48)
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
                        Score += Level * row;
                        Util.PlaySound(Resource1.explosion);

                        return true;
                    }
                }
            }
            return false;
        }

        static bool ThereIsNoEnemyLeft()
        {
            foreach(var enemy in enemies)
            {
                if (enemy.Alive)
                {
                    return false;
                }
            }
            return true;
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
                        if (ThereIsNoEnemyLeft())
                        {
                            NextLevel();
                            return;
                        }
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
                if (Level == 0)
                {
                    NextLevel();
                }
                cannon.Draw();
                UpdateBullets();
                Thread.Sleep(Constant.OneSecond / 50);
                UpdateEnemies();
                CheckKeypressed();
             }

            // The end
            Finish();
        }
    }
}
