using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Praktiline_töö_Madu
{
    internal class MainClass
    {
        // Muudatud tsükkel ja funktsionaalsust.
        static void Main(string[] args)
        {
            Console.SetWindowSize(81, 26);
            Console.OutputEncoding = Encoding.UTF8;

            bool playAgain = true;
            int maxScore = 0;
            int maxSpeed = 0;

            Sounds soundTheme = new Sounds();
            Sounds sounds = new Sounds();

            while (playAgain)
            {
                Console.Clear();
                bool gameRunning = true;

                Walls walls = new Walls();
                walls.Draw();

                Point start = new Point(4, 5, ' ');

                Snake snake = new Snake(start, 4, Direction.RIGHT);
                snake.Draw();

                Score score = new Score(25, 0); // score / time

                Food food = new Food(80, 25);

                Thread.Sleep(500);

                soundTheme.PlayTheme();

                while (gameRunning)
                {
                    if (maxScore < score.GetScore()) { maxScore = score.GetScore(); }  // result table
                    if (maxSpeed < (int)snake.CalculateSpeed()) { maxSpeed = (int)snake.CalculateSpeed(); } // result table
                    score.UpdateDisplay();

                    if (walls.IsHit(snake) || snake.IsHitTail() || snake.CheckLength()) // kontrollib tabamust seintele, sabadele ja pikkadele madudele
                    {
                        soundTheme.Stop();
                        sounds.GameOver();
                        gameRunning = false;
                        break;
                    }

                    List<Point> foodItems = food.GetFoodItems(); // food
                    foreach (var foodItem in foodItems.ToList())
                    {
                        if (snake.Eat(foodItem, score, sounds))
                        {
                            food.RemoveFood(foodItem);
                            food.CreateFood();
                        }
                    }

                    if (snake.CheckLength() == true) { soundTheme.Stop(); sounds.GameOver(); gameRunning = false; break;} // exception avoid

                    Thread.Sleep(snake.GetDelay());
                    snake.Move();

                    food.Draw();

                    Console.SetCursorPosition(80, 25);

                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo key = Console.ReadKey();
                        snake.HandleKey(key.Key);
                    }
                }
                playAgain = Game.AskPlayAgain(score, maxScore, maxSpeed);
            }
        }
    }
}
