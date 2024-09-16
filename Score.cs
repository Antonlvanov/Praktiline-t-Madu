using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// Uus klass mängu skoori kuvamiseks

namespace Praktiline_töö_Madu
{
    class Score
    {
        private int speed;
        private int score; // == speed * length
        private Stopwatch stopwatch; 
        private int x; // pos
        private int y;
        

        public Score(int startX, int startY)
        {
            speed = 5;
            score = 0;
            stopwatch = new Stopwatch();
            x = startX;
            y = startY;
            stopwatch.Start();
            UpdateDisplay();
        }

        public void ChangeScore(int length, double speed)
        {
            if (length * (int)speed - 15 >= 0)
            {
                this.speed = Convert.ToInt32(speed);
                score = length * (int)speed - 15;
                UpdateDisplay();
            }

        }

        public string GetTime()
        {
            return stopwatch.Elapsed.ToString(@"mm\:ss");
        }

        public int GetSpeed() { return speed; }
        public int GetScore()
        {
            return score;
        }

        public void StopTimer()
        {
            stopwatch.Stop(); 
            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"Score: {score} Speed: {speed} Time: {stopwatch.Elapsed:mm\\:ss}");
            Console.SetCursorPosition(80, 25);
            Console.ResetColor();
        }

    }
}
