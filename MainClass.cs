﻿using System;
using System.Text;

namespace Praktiline_töö_Madu
{
    internal class MainClass
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(81, 26);
            Console.OutputEncoding = Encoding.UTF8;

            bool mainmenu = true;
            Leaderboard leaderboard = new Leaderboard();
            Sounds soundMenu = new Sounds();
            Menu menu = new Menu();

            while (mainmenu) // mainmenu
            {
                Console.Clear();

                string Option = menu.MainMenu(soundMenu);
                switch (Option)
                {
                    case "START":
                        bool playAgain = true;
                        while (playAgain)
                        {
                            playAgain = StartGame(leaderboard);
                        }
                        break;
                    case "LEADERBOARD":
                        leaderboard.DisplayLeaders();
                        Console.ReadKey();
                        break;
                    case "QUIT":
                        mainmenu = false;
                        break;
                }
            }
        }

        static bool StartGame(Leaderboard leaderboard)
        {
            int maxScore = 0;
            int maxSpeed = 0;
            bool gameRunning = true;

            Console.Clear();

            Walls walls = new Walls();
            walls.Draw();

            Point start = new Point(4, 5, ' ');

            Snake snake = new Snake(start, 4, Direction.RIGHT);
            snake.Draw();

            Score score = new Score(25, 0); // score / time

            Food food = new Food(80, 25);

            Sounds soundTheme = new Sounds();
            Sounds sounds = new Sounds();

            sounds.PlayStart();

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

                if (snake.CheckLength() == true) { soundTheme.Stop(); sounds.GameOver(); gameRunning = false; break; } // exception avoid

                Thread.Sleep(snake.GetDelay());
                snake.Move();

                food.DrawWithColor();

                Console.SetCursorPosition(80, 25);

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    snake.HandleKey(key.Key);
                }
            }
            return Game.AskPlayAgain(score, leaderboard, maxScore, maxSpeed);
        }
    }
}
