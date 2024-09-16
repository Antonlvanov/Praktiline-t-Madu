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
        public static bool AskPlayAgain(Score score, Leaderboard leaderboard, int maxScore, int maxSpeed)
        {
            Console.Clear();
            Walls walls = new Walls();
            walls.Draw();
            string leader;

            Game.GameOver(maxScore, maxSpeed, score.GetTime());

            Console.SetCursorPosition(30, 14);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Введите имя: ");
            leader = Console.ReadLine();
            leaderboard.
            Console.Write("Сыграть снова? (Y/N): ");
            Console.ForegroundColor = ConsoleColor.White;
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
                else
                {
                    Thread.Sleep(500);
                }
            }
        }

        public static void GameOver(int maxscore, int maxspeed, string time)
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

            Thread.Sleep(1000);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(33, 17);
            Console.WriteLine($"Лучший счет: {maxscore}");
            Console.SetCursorPosition(32, 19);
            Console.WriteLine($"Макс. скорость: {maxspeed}");
            Console.SetCursorPosition(31, 21);
            Console.WriteLine($"Время жизни: {time}");
            Console.ResetColor();

        }
    }
}
