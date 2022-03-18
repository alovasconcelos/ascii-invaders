using System;
using System.IO;
using System.Media;
using System.Threading;

namespace ASCII_Invaders
{
    public static class Util
    {
        public static string exePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        public static void WriteAt(int col, int row, string content, ConsoleColor color = ConsoleColor.White)
        {
            Console.SetCursorPosition(Constant.ScreenLeft + col, Constant.ScreenTop + row);

            Console.ForegroundColor = color;
            Console.Write(content);
            Console.ResetColor();
        }

        public static int ReadBestScore()
        {
            if (File.Exists("score.dat"))
            {
                using (TextReader tr = File.OpenText(Constant.ScoreFile))
                {
                    var fileContent = tr.ReadToEnd();
                    return int.Parse(fileContent);
                }
            }        
            return 0;
        }

        public static void WriteBestScore(int score)
        {
            using (StreamWriter outputFile = new StreamWriter(Constant.ScoreFile))
            {
                outputFile.WriteLine(score);
            }
        }

        public static void Wait(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }
    }
}
