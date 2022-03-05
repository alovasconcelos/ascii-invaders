using System;
using System.Media;

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

        public static void PlaySound(string file)
        {
            // Create new SoundPlayer in the using statement.
            using (SoundPlayer player = new SoundPlayer($@"{exePath}\{file}"))
            {
                // Use PlaySync to load and then play the sound.
                player.Play();
            }
        }
    }
}
