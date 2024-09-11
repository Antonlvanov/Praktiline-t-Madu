using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Praktiline_töö_Madu
{
    internal class MainClass
    {
        // Muudatud tsükkel et parandada funktsionaalsust Game classiga.
        static void Main(string[] args)
        {
            Console.SetWindowSize(81, 26);
            Console.OutputEncoding = Encoding.UTF8;
            bool playAgain = true;
            while (playAgain)
            {
                Console.Clear();
                bool gameRunning = true;

                Walls walls = new Walls();
                walls.Draw();

                Point start = new Point(4, 5, '═');
                Snake snake = new Snake(start, 4, Direction.RIGHT);
                snake.Draw();

                FoodCreator foodCreator = new FoodCreator(80, 25, '$');
                Point food = foodCreator.CreateFood();
                food.Draw();

                while (gameRunning)
                {
                    if (walls.IsHit(snake) || snake.IsHitTail())
                    {
                        gameRunning = false;
                        break;
                    }
                    if (snake.Eat(food))
                    {
                        food = foodCreator.CreateFood();
                        food.Draw();
                    }
                    else
                    {
                        snake.Move();
                    }
                    Thread.Sleep(snake.GetMovementDelay());

                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo key = Console.ReadKey();
                        snake.HandleKey(key.Key);
                    }
                }
                playAgain = Game.AskPlayAgain();
            }
        }
    }
}
