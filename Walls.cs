using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktiline_töö_Madu
{
    class Walls
    {
        List<Figure> wallList; 

        public Walls()
        {
            wallList = new List<Figure>();

            int mapWidth = 80;
            int mapHeight = 25;


            Console.SetWindowSize(mapWidth+1, mapHeight+1); //

            // uus väljade piirid

            HorizontalLine upLine = new HorizontalLine(0, mapWidth-2, 1, '-');
            HorizontalLine downLine = new HorizontalLine(0, mapWidth - 2, mapHeight, '-');
            VerticalLine leftLine = new VerticalLine(1, mapHeight -1, 0, '|');
            VerticalLine rightLine = new VerticalLine(1, mapHeight -1, mapWidth, '|');

            wallList.Add(upLine);
            wallList.Add(downLine);
            wallList.Add(leftLine);
            wallList.Add(rightLine);
        }

        internal bool IsHit(Figure figure )
        {
            foreach (var wall in wallList)
            {
                if (wall.IsHit(figure))
                {
                    return true;
                }
            }
            return false;
        }
        public void Draw()
        {
            foreach (var wall in wallList)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                wall.Draw();
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
