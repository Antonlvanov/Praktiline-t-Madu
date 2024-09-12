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
        private int score; 
        private Stopwatch stopwatch; 
        private int x; // pos
        private int y; // pos
        

        public Score(int startX, int startY)
        {
            score = 3;
            stopwatch = new Stopwatch();
            x = startX;
            y = startY;
            stopwatch.Start();
            UpdateDisplay();
        }

        public void ChangeScore(int scoreChange)
        {
            score = score + scoreChange;
            UpdateDisplay();
        }

        public string GetTime()
        {
            return stopwatch.Elapsed.ToString(@"mm\:ss");
        }

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
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"HP: {score}  Time: {stopwatch.Elapsed:mm\\:ss}");
            Console.SetCursorPosition(80, 25);
            Console.ForegroundColor = ConsoleColor.White;
        }

    }
}
