using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktiline_töö_Madu
{
    // Uus class
    class Game
    {
        public static bool AskPlayAgain()
        {
            Console.Clear();
            Walls walls = new Walls();
            walls.Draw();

            Game.GameOver();
            Console.SetCursorPosition(30, 14);
            Console.Write("Сыграть снова? (Y/N): ");
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Y)
                {
                    return true;
                }
                else if (key.Key == ConsoleKey.N)
                {
                    return false;
                }
            }
        }

        public static void GameOver()
        {
            string[] gameOver = new string[]
                {
                "   ______                        ____                  ",
                "  / ____/___ _____ ___  ___     / __ \\_   _____  _____",
                " / / __/ __ `/ __ `__ \\/ _ \\   / / / / | / / _ \\/ ___/",
                "/ /_/ / /_/ / / / / / /  __/  / /_/ /| |/ /  __/ /    ",
                "\\____/\\__,_/_/ /_/ /_/\\___/   \\____/ |___/\\___/_/     ",
                "                                                       "
                };

            int i = 0;
            Console.ForegroundColor = ConsoleColor.Red;
            foreach (var line in gameOver)
            {
                Console.SetCursorPosition(15, 7 + i);
                Console.WriteLine(line);
                i++;
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
