using System;
using System.IO;
using System.Media;
using System.Threading;

namespace ASCII_Invaders
{
    public static class Util
    {
        public static string exePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        public static void WriteAt(int col, int row, string content)
        {
            Console.SetCursorPosition(Program.screenLeft + col, Program.screenTop + row);
            Console.Write(content);
        }

        public static void PlaySound(Stream file)
        {
            if (!Program.PlaySound)
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

        public static void WriteBestScore()
        {
            using (StreamWriter outputFile = new StreamWriter(Constant.ScoreFile))
            {
                outputFile.WriteLine(Program.BestScore);
            }
        }

        public static void Wait(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }
    }
}
