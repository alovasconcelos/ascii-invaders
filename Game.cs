using System;
using System.IO;
using System.Media;

namespace ASCII_Invaders
{
    class Game
    {
        private bool keepRunning;

        private int clockTicking = 0;

        public  int aliveEnemies;

        public bool PlaySound { get; set; }

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

            ShowSplashScreen();

            enemiesGoLeft = true;
            enemiesGoDown = false;
            Score = 0;

            BestScore = Util.ReadBestScore();
        }

        private void PlayWavFile(Stream file)
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


        private void NextLevel()
        {
            if (Level == Constant.FinalLevel)
            {
                // Last level reached - Congratulations
                Congratulations();
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
            ShowLevelSplashScreen();
        }


        private  void Congratulations()
        {
            PlayWavFile(Resource1.congrats);
            for (var row = Constant.BattleFieldBottom - 8; row > Constant.BattleFieldTop; row--)
            {
                Util.WriteAt(7, row,     "╔═══╗                     ╔╗     ", ConsoleColor.Red);
                Util.WriteAt(7, row + 1, "║╔═╗║                    ╔╝╚╗    ", ConsoleColor.Red);
                Util.WriteAt(7, row + 2, "║║ ╚╝╔══╗╔═╗ ╔══╗╔═╗╔══╗ ╚╗╔╝╔══╗", ConsoleColor.Red);
                Util.WriteAt(7, row + 3, "║║ ╔╗║╔╗║║╔╗╗║╔╗║║╔╝╚ ╗║  ║║ ║══╣", ConsoleColor.Red);
                Util.WriteAt(7, row + 4, "║╚═╝║║╚╝║║║║║║╚╝║║║ ║╚╝╚╗ ║╚╗╠══║", ConsoleColor.Red);
                Util.WriteAt(7, row + 5, "╚═══╝╚══╝╚╝╚╝╚═╗║╚╝ ╚═══╝ ╚═╝╚══╝", ConsoleColor.Red);
                Util.WriteAt(7, row + 6, "             ╔═╝║                ", ConsoleColor.Red);
                Util.WriteAt(7, row + 7, "             ╚══╝                ", ConsoleColor.Red);
                Util.ClearLine(row + 8);
                Util.Wait(Constant.OneSecond / 10);

            }
            Level = 0;
            Util.Wait(Constant.OneSecond * 3);
            ClearBattleField();
        }
        private  void ShowSplashScreen()
        {
            for (var row = Constant.BattleFieldBottom - 13; row > Constant.BattleFieldTop; row--)
            { 
                Util.WriteAt(7, row,      "╔═══╗        ╔══╗╔══╗", ConsoleColor.Red);
                Util.WriteAt(7, row + 1,  "║╔═╗║        ╚╣╠╝╚╣╠╝", ConsoleColor.Red);
                Util.WriteAt(7, row + 2,  "║║ ║║╔══╗╔══╗ ║║  ║║ ", ConsoleColor.Red);
                Util.WriteAt(7, row + 3,  "║╚═╝║║══╣║╔═╝ ║║  ║║ ", ConsoleColor.Red);
                Util.WriteAt(7, row + 4,  "║╔═╗║╠══║║╚═╗╔╣╠╗╔╣╠╗", ConsoleColor.Red);
                Util.WriteAt(7, row + 5,  "╚╝ ╚╝╚══╝╚══╝╚══╝╚══╝", ConsoleColor.Red);
                Util.WriteAt(7, row + 6,  "       ╔══╗               ╔╗           ", ConsoleColor.Red);
                Util.WriteAt(7, row + 7,  "       ╚╣╠╝               ║║           ", ConsoleColor.Red);
                Util.WriteAt(7, row + 8,  "        ║║ ╔═╗ ╔╗╔╗╔══╗ ╔═╝║╔══╗╔═╗╔══╗", ConsoleColor.Red);
                Util.WriteAt(7, row + 9,  "        ║║ ║╔╗╗║╚╝║╚ ╗║ ║╔╗║║╔╗║║╔╝║══╣", ConsoleColor.Red);
                Util.WriteAt(7, row + 10, "       ╔╣╠╗║║║║╚╗╔╝║╚╝╚╗║╚╝║║║═╣║║ ╠══║", ConsoleColor.Red);
                Util.WriteAt(7, row + 11, "       ╚══╝╚╝╚╝ ╚╝ ╚═══╝╚══╝╚══╝╚╝ ╚══╝", ConsoleColor.Red);
                Util.WriteAt(7, row + 12, "               alovasconcelos.github.io", ConsoleColor.DarkBlue);
                Util.ClearLine(row + 13);
                Util.Wait(Constant.OneSecond / 10);
            }
            Util.Wait(Constant.OneSecond * 3);
            ClearBattleField();
        }

        private  void GameOver()
        {
            ClearBattleField();
            PlayWavFile(Resource1.game_over);
            for (var row = Constant.BattleFieldBottom - 12; row > Constant.BattleFieldTop; row--)
            {
                Util.WriteAt(7, row,      "╔═══╗             ", ConsoleColor.Red);
                Util.WriteAt(7, row + 1,  "║╔═╗║             ", ConsoleColor.Red);
                Util.WriteAt(7, row + 2,  "║║ ╚╝╔══╗ ╔╗╔╗╔══╗", ConsoleColor.Red);
                Util.WriteAt(7, row + 3,  "║║╔═╗╚ ╗║ ║╚╝║║╔╗║", ConsoleColor.Red);
                Util.WriteAt(7, row + 4,  "║╚╩═║║╚╝╚╗║║║║║║═╣", ConsoleColor.Red);
                Util.WriteAt(7, row + 5,  "╚═══╝╚═══╝╚╩╩╝╚══╝", ConsoleColor.Red);
                Util.WriteAt(7, row + 6,  "                  ╔══╗╔╗╔╗╔══╗╔═╗", ConsoleColor.Red);
                Util.WriteAt(7, row + 7,  "                  ║╔╗║║╚╝║║╔╗║║╔╝", ConsoleColor.Red);
                Util.WriteAt(7, row + 8,  "                  ║╚╝║╚╗╔╝║║═╣║║ ", ConsoleColor.Red);
                Util.WriteAt(7, row + 9,  "                  ╚══╝ ╚╝ ╚══╝╚╝ ", ConsoleColor.Red);
                Util.ClearLine(row + 10);
                Util.WriteAt(7, row + 11, "    Your score: " + Score.ToString().PadLeft(6, '0'), ConsoleColor.Yellow);
                Util.ClearLine(row + 12);
                Util.Wait(Constant.OneSecond / 10);
            }
            Util.Wait(Constant.OneSecond * 5);
            Level = 0;
            Score = 0;
            ClearBattleField();
        }

        private  void ShowLevelSplashScreen()
        {
            for (var row = Constant.BattleFieldBottom - 6; row > Constant.BattleFieldTop; row--)
            {

                Util.WriteAt(7, row,     "#       ######  #    #  ######  #     ", ConsoleColor.Red);
                Util.WriteAt(7, row + 1, "#       #       #    #  #       #     ", ConsoleColor.Red);
                Util.WriteAt(7, row + 2, "#       #####   #    #  #####   #     ", ConsoleColor.Red);
                Util.WriteAt(7, row + 3, "#       #       #    #  #       #     ", ConsoleColor.Red);
                Util.WriteAt(7, row + 4, "#       #        #  #   #       #     ", ConsoleColor.Red);
                Util.WriteAt(7, row + 5, "######  ######    ##    ######  ######", ConsoleColor.Red);
                Util.ClearLine(row + 6);
                Util.Wait(Constant.OneSecond / 10);
            }

            switch (Level)
            {
                case 1:

                    Util.WriteAt(23, Constant.BattleFieldTop + 8,  "  #", ConsoleColor.Yellow);
                    Util.WriteAt(23, Constant.BattleFieldTop + 9,  " ##", ConsoleColor.Yellow);
                    Util.WriteAt(23, Constant.BattleFieldTop + 10, "# #", ConsoleColor.Yellow);
                    Util.WriteAt(23, Constant.BattleFieldTop + 11, "  #", ConsoleColor.Yellow);
                    Util.WriteAt(23, Constant.BattleFieldTop + 12, "  #", ConsoleColor.Yellow);
                    Util.WriteAt(23, Constant.BattleFieldTop + 13, "  #", ConsoleColor.Yellow);
                    Util.WriteAt(23, Constant.BattleFieldTop + 14, "#####", ConsoleColor.Yellow);
                    break;
                case 2:
                    Util.WriteAt(22, Constant.BattleFieldTop + 8,  " ##### ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 9,  "#     #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 10, "      #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 11, " ##### ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 12, "#      ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 13, "#      ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 14, "#######", ConsoleColor.Yellow);
                    break;
                case 3:
                    Util.WriteAt(22, Constant.BattleFieldTop + 8,  " ##### ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 9,  "#     #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 10, "      #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 11, " ##### ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 12, "      #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 13, "#     #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 14, " ##### ", ConsoleColor.Yellow);
                    break;
                case 4:
                    Util.WriteAt(22, Constant.BattleFieldTop + 8,  "#     ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 9,  "#    #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 10, "#    #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 11, "#######", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 12, "     #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 13, "     #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 14, "     #", ConsoleColor.Yellow);
                    break;
                case 5:
                    Util.WriteAt(22, Constant.BattleFieldTop + 8,  "#######", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 9,  "#      ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 10, "#      ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 11, " ##### ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 12, "      #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 13, "#     #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 14, " ##### ", ConsoleColor.Yellow);
                    break;
                case 6:
                    Util.WriteAt(22, Constant.BattleFieldTop + 8,  " ##### ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 9,  "#     #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 10, "#      ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 11, "###### ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 12, "#     #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 13, "#     #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 14, " ##### ", ConsoleColor.Yellow);
                    break;
                case 7:
                    Util.WriteAt(22, Constant.BattleFieldTop + 8,  "#######", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 9,  "#    # ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 10, "    #  ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 11, "   #   ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 12, "  #    ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 13, "  #    ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 14, "  #    ", ConsoleColor.Yellow);
                    break;
                case 8:
                    Util.WriteAt(22, Constant.BattleFieldTop + 8,  " ##### ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 9,  "#     #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 10, "#     #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 11, " ##### ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 12, "#     #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 13, "#     #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 14, " ##### ", ConsoleColor.Yellow);
                    break;
                case 9:
                    Util.WriteAt(22, Constant.BattleFieldTop + 8,  " ##### ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 9,  "#     #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 10, "#     #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 11, " ######", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 12, "      #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 13, "#     #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 14, " ##### ", ConsoleColor.Yellow);
                    break;
            }
            Util.Wait(Constant.OneSecond * 2);
            ClearBattleField();
        }

        private  void ClearBattleField()
        {
            for (var row = Constant.BattleFieldBottom; row >= Constant.BattleFieldTop; row--)
            {
                Util.ClearLine(row);
                Util.Wait(Constant.OneSecond / 50);
            }
        }
        private  void Finish()
        {
            Console.Clear();
            Console.CursorVisible = true;
            Console.WriteLine("Bye...");
        }

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

        private  void ConfirmExit()
        {
            // Clear the battlefield
            ClearBattleField();
            Util.WriteAt(4, 10, "Press Y to exit or any other key to continue", ConsoleColor.Red);
            if (Console.ReadKey().Key.Equals(ConsoleKey.Y))
            {
                keepRunning = false;
            }
            Util.ClearLine(10);
            Update();
        }

        private  void Pause()
        {
            // Clear the battlefield
            ClearBattleField();
            Util.WriteAt(4, 10, "Press any key to continue", ConsoleColor.Red);
            Console.ReadKey();
            Util.ClearLine(10);
            Update();
        }

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
                        Pause();
                        break;
                    case ConsoleKey.Escape:
                        ConfirmExit();
                        break;
                }
            }
        }

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
                        GameOver();
                        return;
                    }
                    if (enemies[row, col].Visible)
                    {
                        if (enemies[row, col].Position.X == Constant.BattleFieldLeft)
                        {
                            goLeft = false;
                            goDown = true;
                        }
                        if (enemies[row, col].Position.Y == Constant.BattleFieldWidth - 2)
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

        void UpdateStatusBar()
        {
            Util.WriteAt(Constant.BattleFieldSoundStatusCol, Constant.BattleFieldStatusBar, PlaySound ? "On " : "Off");
            Util.WriteAt(Constant.BattleFieldLevelCol, Constant.BattleFieldStatusBar, Level.ToString());
            Util.WriteAt(Constant.BattleFieldScoreCol, Constant.BattleFieldStatusBar, Score.ToString().PadLeft(6, '0'));
            Util.WriteAt(Constant.BattleFieldBestScoreCol, Constant.BattleFieldStatusBar, _bestScore.ToString().PadLeft(6, '0'));
        }

        void Update()      
         {
            if (clockTicking++ == 59)
            {
                clockTicking = 0;
            }
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
            UpdateStatusBar();
        }
    }
}
