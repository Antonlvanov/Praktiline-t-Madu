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

        double delay = 100; //delay ms
        int length = 3;
        double speed;

        public Snake(Point tail, int length , Direction _direction)
        {
            direction = _direction;
            prevDirection = _direction;
            pList = new List<Point>();
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < length; i++)
            {
                Point p = new Point(tail);
                p.Move(i, direction);
                pList.Add(p);
            }
            Point head = pList.Last();
            pList.Last().sym = '☺';
            Console.ForegroundColor = ConsoleColor.White;
        }

        public Point GetNextPoint()
        {
            Point head = pList.Last();
            Point nextPoint = new Point(head);
            head.sym = '☺';
            nextPoint.Move(1, direction);
            return nextPoint;
        }

        internal bool IsHitTail()
        {
            var head = pList.Last();
            for (int i = 0; i < pList.Count - 2; i++)
            {
                if (head.IsHit(pList[i]))
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

        // muudatud madu välimus 
        internal void Move()
        {
            Point tail = pList.First();
            pList.Remove(tail);
            Console.SetCursorPosition(80, 25); // fix blinking cursor point
            Point head = GetNextPoint();
            pList.Add(head);

            tail.Clear();

            Point prevHead = pList[pList.Count - 2]; // prev head

            // defining snake appearance
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

            // upd prev direction
            prevDirection = direction;

            Console.ForegroundColor = ConsoleColor.Green;
            foreach (var point in pList)
            {
                point.Draw();
            }
            Console.ForegroundColor = ConsoleColor.White;
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

        // est edu & effects
        internal bool Eat(Point food, Score score, Sounds sounds)
        {
            Point head = GetNextPoint();
            if (head.IsHit(food))
            {
                switch (food.sym)
                {
                    case '♥':
                        Point tailtail = new Point(pList.First());
                        pList.Insert(0, tailtail);
                        length++;
                        sounds.PlayEat();
                        break;
                    case '♠':
                        pList.RemoveAt(pList.Count - 1);
                        length--;
                        sounds.PlayEatPoison();
                        break;
                    case '+':
                        delay *= 0.8;
                        sounds.SpeedUp();
                        break;
                    case '-':
                        delay *= 1.2;
                        sounds.SlowDown();
                        break;
                }
                speed = CalculateSpeed();
                score.ChangeScore(length, speed);
                return true;
            }
            return false;
        }

        public double CalculateSpeed()
        {
            // low delay - high speed
            return 500 / delay;
        }

        // horisontal / vertical speed
        public int GetDelay()
        {
            if (direction == Direction.UP || direction == Direction.DOWN)
            {
                return (int)(delay * 1.33);
            }
            return (int)(delay);
        }
    }
}
