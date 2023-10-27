using System;
using System.Linq;

namespace ASCII_Invaders
{
    /// <summary>
    /// Class <c>BattleField</c> refers to the battle field
    /// </summary>
    /// <author>
    /// </author>
    class BattleField
    {
        /// <summary>
        /// Method <c>Draw</c> draws the game screen
        /// </summary>
        public void Draw()
        {
            Util.WriteAt(0, 0, "┍━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┑");
            Util.WriteAt(0, 1, "│                  ASCII INVADERS                  │");
            Util.WriteAt(0, 2, "├━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┤");
            for (int row = Constant.BattleFieldTop; row <= Constant.BattleFieldBottom; row++)
            {
                Util.WriteAt(0, row, "│                                                  │");
            }
            Util.WriteAt(0, Constant.BattleFieldBottom + 1, "├━━━━━━━━━┮━━━━━━━┮━━━━━━━━━━━━━┮━━━━━━━━━━━━━━━━━━┤");
            Util.WriteAt(0, Constant.BattleFieldStatusBar,  "│Sound:on │Level: │Score:       │Best Score:       │");
            Util.WriteAt(0, Constant.BattleFieldStatusBar + 1, "┖━━━━━━━━━┶━━━━━━━┶━━━━━━━━━━━━━┶━━━━━━━━━━━━━━━━━━┙");
        }                                                                                                          

        /// <summary>
        /// Clears an entire line of the battlefield (except the borders)
        /// </summary>
        /// <param name="row">Row number</param>
        public void ClearLine(int row)
        {
            Util.WriteAt(1, row, string.Concat(Enumerable.Repeat(" ", 50)));
        }

        /// <summary>
        /// Shows congratulations message
        /// </summary>
        public void Congratulations()
        {
            for (var row = Constant.BattleFieldBottom - 8; row > Constant.BattleFieldTop; row--)
            {
                Util.WriteAt(7, row, "╔═══╗                     ╔╗     ", ConsoleColor.Red);
                Util.WriteAt(7, row + 1, "║╔═╗║                    ╔╝╚╗    ", ConsoleColor.Red);
                Util.WriteAt(7, row + 2, "║║ ╚╝╔══╗╔═╗ ╔══╗╔═╗╔══╗ ╚╗╔╝╔══╗", ConsoleColor.Red);
                Util.WriteAt(7, row + 3, "║║ ╔╗║╔╗║║╔╗╗║╔╗║║╔╝╚ ╗║  ║║ ║══╣", ConsoleColor.Red);
                Util.WriteAt(7, row + 4, "║╚═╝║║╚╝║║║║║║╚╝║║║ ║╚╝╚╗ ║╚╗╠══║", ConsoleColor.Red);
                Util.WriteAt(7, row + 5, "╚═══╝╚══╝╚╝╚╝╚═╗║╚╝ ╚═══╝ ╚═╝╚══╝", ConsoleColor.Red);
                Util.WriteAt(7, row + 6, "             ╔═╝║                ", ConsoleColor.Red);
                Util.WriteAt(7, row + 7, "             ╚══╝                ", ConsoleColor.Red);
                ClearLine(row + 8);
                Util.Wait(Constant.OneSecond / 10);
            }
            Util.Wait(Constant.OneSecond * 3);
            ClearBattleField();
        }

        /// <summary>
        /// Shows the splash screen
        /// </summary>
        public void ShowSplashScreen()
        {
            for (var row = Constant.BattleFieldBottom - 13; row > Constant.BattleFieldTop; row--)
            {
                Util.WriteAt(7, row, "╔═══╗        ╔══╗╔══╗", ConsoleColor.Red);
                Util.WriteAt(7, row + 1, "║╔═╗║        ╚╣╠╝╚╣╠╝", ConsoleColor.Red);
                Util.WriteAt(7, row + 2, "║║ ║║╔══╗╔══╗ ║║  ║║ ", ConsoleColor.Red);
                Util.WriteAt(7, row + 3, "║╚═╝║║══╣║╔═╝ ║║  ║║ ", ConsoleColor.Red);
                Util.WriteAt(7, row + 4, "║╔═╗║╠══║║╚═╗╔╣╠╗╔╣╠╗", ConsoleColor.Red);
                Util.WriteAt(7, row + 5, "╚╝ ╚╝╚══╝╚══╝╚══╝╚══╝", ConsoleColor.Red);
                Util.WriteAt(7, row + 6, "       ╔══╗               ╔╗           ", ConsoleColor.Red);
                Util.WriteAt(7, row + 7, "       ╚╣╠╝               ║║           ", ConsoleColor.Red);
                Util.WriteAt(7, row + 8, "        ║║ ╔═╗ ╔╗╔╗╔══╗ ╔═╝║╔══╗╔═╗╔══╗", ConsoleColor.Red);
                Util.WriteAt(7, row + 9, "        ║║ ║╔╗╗║╚╝║╚ ╗║ ║╔╗║║╔╗║║╔╝║══╣", ConsoleColor.Red);
                Util.WriteAt(7, row + 10, "       ╔╣╠╗║║║║╚╗╔╝║╚╝╚╗║╚╝║║║═╣║║ ╠══║", ConsoleColor.Red);
                Util.WriteAt(7, row + 11, "       ╚══╝╚╝╚╝ ╚╝ ╚═══╝╚══╝╚══╝╚╝ ╚══╝", ConsoleColor.Red);
                Util.WriteAt(7, row + 12, "               alovasconcelos.github.io", ConsoleColor.DarkBlue);
                ClearLine(row + 13);
                Util.Wait(Constant.OneSecond / 10);
            }
            Util.Wait(Constant.OneSecond * 3);
            ClearBattleField();
        }

        /// <summary>
        /// Game over screen, showing the score achieved
        /// </summary>
        /// <param name="score"></param>
        public void GameOver(int score)
        {
            ClearBattleField();
            for (var row = Constant.BattleFieldBottom - 12; row > Constant.BattleFieldTop; row--)
            {
                Util.WriteAt(7, row, "╔═══╗             ", ConsoleColor.Red);
                Util.WriteAt(7, row + 1, "║╔═╗║             ", ConsoleColor.Red);
                Util.WriteAt(7, row + 2, "║║ ╚╝╔══╗ ╔╗╔╗╔══╗", ConsoleColor.Red);
                Util.WriteAt(7, row + 3, "║║╔═╗╚ ╗║ ║╚╝║║╔╗║", ConsoleColor.Red);
                Util.WriteAt(7, row + 4, "║╚╩═║║╚╝╚╗║║║║║║═╣", ConsoleColor.Red);
                Util.WriteAt(7, row + 5, "╚═══╝╚═══╝╚╩╩╝╚══╝", ConsoleColor.Red);
                Util.WriteAt(7, row + 6, "                  ╔══╗╔╗╔╗╔══╗╔═╗", ConsoleColor.Red);
                Util.WriteAt(7, row + 7, "                  ║╔╗║║╚╝║║╔╗║║╔╝", ConsoleColor.Red);
                Util.WriteAt(7, row + 8, "                  ║╚╝║╚╗╔╝║║═╣║║ ", ConsoleColor.Red);
                Util.WriteAt(7, row + 9, "                  ╚══╝ ╚╝ ╚══╝╚╝ ", ConsoleColor.Red);
                ClearLine(row + 10);
                Util.WriteAt(7, row + 11, "    Your score: " + score.ToString().PadLeft(6, '0'), ConsoleColor.Yellow);
                ClearLine(row + 12);
                Util.Wait(Constant.OneSecond / 10);
            }
            Util.Wait(Constant.OneSecond * 5);

            ClearBattleField();
        }

        /// <summary>
        /// Splash screen for each level
        /// </summary>
        /// <param name="level"></param>
        public void ShowLevelSplashScreen(int level)
        {
            for (var row = Constant.BattleFieldBottom - 6; row > Constant.BattleFieldTop; row--)
            {
                Util.WriteAt(7, row, "#       ######  #    #  ######  #     ", ConsoleColor.Red);
                Util.WriteAt(7, row + 1, "#       #       #    #  #       #     ", ConsoleColor.Red);
                Util.WriteAt(7, row + 2, "#       #####   #    #  #####   #     ", ConsoleColor.Red);
                Util.WriteAt(7, row + 3, "#       #       #    #  #       #     ", ConsoleColor.Red);
                Util.WriteAt(7, row + 4, "#       #        #  #   #       #     ", ConsoleColor.Red);
                Util.WriteAt(7, row + 5, "######  ######    ##    ######  ######", ConsoleColor.Red);
                ClearLine(row + 6);
                Util.Wait(Constant.OneSecond / 10);
            }

            switch (level)
            {
                case 1:
                    Util.WriteAt(23, Constant.BattleFieldTop + 8, "  #", ConsoleColor.Yellow);
                    Util.WriteAt(23, Constant.BattleFieldTop + 9, " ##", ConsoleColor.Yellow);
                    Util.WriteAt(23, Constant.BattleFieldTop + 10, "# #", ConsoleColor.Yellow);
                    Util.WriteAt(23, Constant.BattleFieldTop + 11, "  #", ConsoleColor.Yellow);
                    Util.WriteAt(23, Constant.BattleFieldTop + 12, "  #", ConsoleColor.Yellow);
                    Util.WriteAt(23, Constant.BattleFieldTop + 13, "  #", ConsoleColor.Yellow);
                    Util.WriteAt(23, Constant.BattleFieldTop + 14, "#####", ConsoleColor.Yellow);
                    break;
                case 2:
                    Util.WriteAt(22, Constant.BattleFieldTop + 8, " ##### ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 9, "#     #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 10, "      #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 11, " ##### ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 12, "#      ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 13, "#      ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 14, "#######", ConsoleColor.Yellow);
                    break;
                case 3:
                    Util.WriteAt(22, Constant.BattleFieldTop + 8, " ##### ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 9, "#     #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 10, "      #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 11, " ##### ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 12, "      #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 13, "#     #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 14, " ##### ", ConsoleColor.Yellow);
                    break;
                case 4:
                    Util.WriteAt(22, Constant.BattleFieldTop + 8, "#     ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 9, "#    #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 10, "#    #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 11, "#######", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 12, "     #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 13, "     #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 14, "     #", ConsoleColor.Yellow);
                    break;
                case 5:
                    Util.WriteAt(22, Constant.BattleFieldTop + 8, "#######", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 9, "#      ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 10, "#      ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 11, " ##### ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 12, "      #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 13, "#     #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 14, " ##### ", ConsoleColor.Yellow);
                    break;
                case 6:
                    Util.WriteAt(22, Constant.BattleFieldTop + 8, " ##### ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 9, "#     #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 10, "#      ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 11, "###### ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 12, "#     #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 13, "#     #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 14, " ##### ", ConsoleColor.Yellow);
                    break;
                case 7:
                    Util.WriteAt(22, Constant.BattleFieldTop + 8, "#######", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 9, "#    # ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 10, "    #  ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 11, "   #   ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 12, "  #    ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 13, "  #    ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 14, "  #    ", ConsoleColor.Yellow);
                    break;
                case 8:
                    Util.WriteAt(22, Constant.BattleFieldTop + 8, " ##### ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 9, "#     #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 10, "#     #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 11, " ##### ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 12, "#     #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 13, "#     #", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 14, " ##### ", ConsoleColor.Yellow);
                    break;
                case 9:
                    Util.WriteAt(22, Constant.BattleFieldTop + 8, " ##### ", ConsoleColor.Yellow);
                    Util.WriteAt(22, Constant.BattleFieldTop + 9, "#     #", ConsoleColor.Yellow);
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

        /// <summary>
        /// Clears the battle field
        /// </summary>
        private void ClearBattleField()
        {
            for (var row = Constant.BattleFieldBottom; row >= Constant.BattleFieldTop; row--)
            {
                ClearLine(row);
                Util.Wait(Constant.OneSecond / 20);
            }
        }

        /// <summary>
        /// Shows confirm dialog
        /// </summary>
        /// <param name="message"></param>
        /// <returns>True when confirmed, false otherwise</returns>
        public bool Confirm(string message)
        {
            // Clears the battlefield
            ClearBattleField();
            Util.WriteAt(1, 11, Util.PadCenter(message), ConsoleColor.Red);

            var ret = Console.ReadKey(true).Key.Equals(ConsoleKey.Y);
            ClearLine(11);
            return ret;
        }

        /// <summary>
        /// Pauses the game, waiting for the player to press any key
        /// </summary>
        public void Pause()
        {
            // Clears the battlefield
            ClearBattleField();
            Util.WriteAt(1, 10, Util.PadCenter("Press any key to continue"), ConsoleColor.Red);
            Console.ReadKey(true);
            ClearLine(10);
        }

        /// <summary>
        /// Shows information about the game on the status bar
        /// </summary>
        /// <param name="soundOn"></param>
        /// <param name="level"></param>
        /// <param name="score"></param>
        /// <param name="bestScore"></param>
        public void UpdateStatusBar(bool soundOn, int level, int score, int bestScore)
        {
            Util.WriteAt(Constant.BattleFieldSoundStatusCol, Constant.BattleFieldStatusBar, soundOn ? "On " : "Off");
            Util.WriteAt(Constant.BattleFieldLevelCol, Constant.BattleFieldStatusBar, level.ToString());
            Util.WriteAt(Constant.BattleFieldScoreCol, Constant.BattleFieldStatusBar, score.ToString().PadLeft(6, '0'));
            Util.WriteAt(Constant.BattleFieldBestScoreCol, Constant.BattleFieldStatusBar, bestScore.ToString().PadLeft(6, '0'));
        }

    }
}
