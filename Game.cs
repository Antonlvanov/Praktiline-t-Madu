using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Praktiline_töö_Madu
{
    // mänguvälise suhtluse akna kuvamine
    class Game
    {
        public static bool AskPlayAgain(Score score)
        {
            Console.Clear();
            Walls walls = new Walls();
            walls.Draw();

            Game.GameOver(score.GetScore(), score.GetTime());

            Console.SetCursorPosition(30, 14);
            Console.Write("Сыграть снова? (Y/N): ");
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Y)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static void GameOver(int score, string time)
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

            Console.SetCursorPosition(28, 18);
            Console.WriteLine($"Счет: {score}");
            Console.SetCursorPosition(38, 18);
            Console.WriteLine($"Время жизни: {time}");
        }
    }
}
