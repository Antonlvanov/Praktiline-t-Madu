using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktiline_töö_Madu
{
    class Food
    {
        int mapWidth;
        int mapHeight;
        char[] foodSymbols = { '♥', '+', '-', '♠' }; // food
        List<Point> foodItems = new List<Point>();
        Random random = new Random();

        public Food(int mapWidth, int mapHeight)
        {
            this.mapWidth = mapWidth;
            this.mapHeight = mapHeight;
            CreateInitialFood();
        }

        private void CreateInitialFood()
        {
            while (foodItems.Count < 5)
            {
                CreateFood();
            }
        }

        public void CreateFood() // random food
        {
            foreach (char food in foodSymbols)
            {
                if (!foodItems.Any(foods => foods.sym == food))
                {
                    int x = random.Next(2, mapWidth - 2);
                    int y = random.Next(2, mapHeight - 2);
                    foodItems.Add(new Point(x, y, food));
                    return;
                }
            }
            int x_ = random.Next(2, mapWidth - 2);
            int y_ = random.Next(2, mapHeight - 2);
            char nextSymbol = foodSymbols[foodItems.Count % foodSymbols.Length];
            foodItems.Add(new Point(x_, y_, nextSymbol));
        }

        public List<Point> GetFoodItems()
        {
            return foodItems;
        }

        public void RemoveFood(Point food)
        {
            foodItems.Remove(food);
        }

        public void Draw()
        {
            foreach (var food in foodItems)
            {
                food.Draw();
            }
        }
    }
}
