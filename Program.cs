using System;

namespace ASCII_Invaders
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            
            new Game().Run();
        }
    }
}
