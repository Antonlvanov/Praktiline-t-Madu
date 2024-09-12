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

        public void CreateFood()
        {
            int x = random.Next(2, mapWidth - 2);
            int y = random.Next(2, mapHeight - 2);
            char foodSym = foodSymbols[random.Next(foodSymbols.Length)];

            Point newFood = new Point(x, y, foodSym);

            foodItems.Add(newFood);
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
