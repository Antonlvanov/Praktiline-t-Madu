using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktiline_töö_Madu
{
    internal class MainClass
    {
        static void Main(string[] args)
        {
            Point p1 = new Point();
            p1.x = 1;
            p1.x = 3;
            p1.sym = '*';

            p1.Draw();

            Point p2 = new Point();

            p2.x = 4;
            p2.y = 5;
            p2.sym =  '#';

            p1.Draw();

            Console.ReadLine();
        }
    }
}
