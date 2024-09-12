using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;

namespace Praktiline_töö_Madu
{
    class Snake : Figure
    {
        Direction direction;
        Direction prevDirection; //

        int delay = 80; //delay ms
        int growth = 1; // rost

        public Snake(Point tail, int length , Direction _direction)
        {
            direction = _direction;
            prevDirection = _direction; //
            pList = new List<Point>();
            for (int i = 0; i < length; i++)
            {
                Point p = new Point(tail);
                p.Move(i, direction);
                pList.Add(p);
            }
        }
        // muudatud madu välimus 
        internal void Move()
        {
            Point tail = pList.First();
            pList.Remove(tail);
            Console.SetCursorPosition(80, 25);
            Point head = GetNextPoint();
            pList.Add(head);

            tail.Clear();

            Point prevHead = pList[pList.Count - 2]; // prev head

            // defining direction change
            if (prevDirection == Direction.UP && direction == Direction.LEFT 
                || prevDirection == Direction.RIGHT && direction == Direction.DOWN)
            {
                prevHead.sym = '╗';
            }
            else if (prevDirection == Direction.UP && direction == Direction.RIGHT 
                || prevDirection == Direction.LEFT && direction == Direction.DOWN)
            {
                prevHead.sym = '╔';
            }
            else if (prevDirection == Direction.DOWN && direction == Direction.LEFT 
                || prevDirection == Direction.RIGHT && direction == Direction.UP)
            {
                prevHead.sym = '╝';
            }
            else if (prevDirection == Direction.DOWN && direction == Direction.RIGHT 
                || prevDirection == Direction.LEFT && direction == Direction.UP)
            {
                prevHead.sym = '╚';
            }

            // if not turn
            else
            {
                if (prevDirection == Direction.LEFT || prevDirection == Direction.RIGHT)
                {
                    prevHead.sym = '═';
                }
                else if (prevDirection == Direction.UP || prevDirection == Direction.DOWN)
                {
                    prevHead.sym = '║';
                }
            }

            head.sym = '☺';

            // upd prev direction
            prevDirection = direction;

            Console.ForegroundColor = ConsoleColor.Green;
            foreach (var point in pList)
            {
                point.Draw();
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        public Point GetNextPoint()
        {
            Point head = pList.Last();
            Point nextPoint = new Point(head);
            nextPoint.Move (1, direction);
            return nextPoint;
        }

        internal bool IsHitTail()
        {
            var head = pList.Last();
            for(int i=0; i < pList.Count-2; i++) 
            {
                if ( head.IsHit(pList[i]))
                    return true;
            }
            return false;
        }

        internal bool CheckLength()
        {
            if (pList.Count < 2)
                return true;
            else
                return false;
        }


        // chtobi ne ubivat' sebya
        public void HandleKey(ConsoleKey key)
        {
            if (key == ConsoleKey.LeftArrow && direction != Direction.RIGHT)
            {
                direction = Direction.LEFT;
            }
            else if (key == ConsoleKey.RightArrow && direction != Direction.LEFT)
            {
                direction = Direction.RIGHT;
            }
            else if (key == ConsoleKey.DownArrow && direction != Direction.UP)
            {
                direction = Direction.DOWN;
            }
            else if (key == ConsoleKey.UpArrow && direction != Direction.DOWN)
            {
                direction = Direction.UP;
            }

        }

        // est edu
        internal bool Eat(Point food, Score score)
        {
            Point head = GetNextPoint();
            if (head.IsHit(food))
            {
                switch (food.sym)
                {
                    case '♥':
                        Point tailtail = new Point(pList.First());
                        pList.Insert(0, tailtail);
                        score.ChangeScore(1);
                        break;
                    case '♠':
                        pList.RemoveAt(pList.Count - 1);
                        score.ChangeScore(-1);
                        break;
                    case '+':
                        delay -= 20;
                        if (delay < 0) 
                        {
                            return false;
                        }
                        break;
                    case '-':
                        delay += 20;
                        if (delay < 0) 
                        {
                            return false;
                        }
                        break;
                }
                return true;
            }
            return false;
        }

        // new
        public int GetDelay()
        {
            if (direction == Direction.UP || direction == Direction.DOWN)
            {
                return delay + 40;
            }
            return delay;
        }
    }
}
