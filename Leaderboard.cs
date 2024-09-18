using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktiline_töö_Madu
{
    public class Leaderboard
    {
        private string filePath;

        public Leaderboard()
        {
            var ind = Directory.GetCurrentDirectory().ToString().IndexOf("bin", StringComparison.Ordinal);
            string binFolder = Directory.GetCurrentDirectory().Substring(0, ind).ToString();

            filePath = Path.Combine(binFolder, "resources", "leaderboard.txt");
        }

        public class Leader
        {
            public string Name { get; set; }
            public int Score { get; set; }
            public int Speed { get; set; }
            public string Time { get; set; }

            public Leader(string name, int score, int speed, string time)
            {
                Name = name;
                Score = score;
                Speed = speed;
                Time = time;
            }

        }

        public void SaveLeaders(List<Leader> leaders)
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                foreach (var leader in leaders)
                {
                    sw.WriteLine($"{leader.Name},{leader.Score},{leader.Speed},{leader.Time}");
                }
                sw.Close();
            }
        }
        public List<Leader> LoadLeaders()
        {
            var leaders = new List<Leader>();
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] data = line.Split(',');

                        string name = data[0];
                        int score = int.Parse(data[1]);
                        int speed = int.Parse(data[2]); ;
                        string time = data[3];

                        leaders.Add(new Leader(name, score, speed, time));
                    }
                    sr.Close();
                }
                return leaders;
            }
            catch (Exception e) { ; Console.WriteLine(e); return leaders; }
            
        }

        public void AddLeader(string name, int score, int speed, string time)
        {
            var leaders = LoadLeaders();
            leaders.Add(new Leader(name, score, speed, time));

            leaders = leaders.OrderByDescending(l => l.Score).ToList();

            SaveLeaders(leaders);
        }

        public void DisplayLeaders()
        {
            var leaders = LoadLeaders();

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(35, 5);

            Console.WriteLine("LEADERBOARD");
            int i=0;
            foreach (var leader in leaders)
            {
                Console.SetCursorPosition(13, 7 + i);
                Console.WriteLine($"{i + 1}. {leader.Name} | Счет: {leader.Score} | Cкорость: {leader.Speed} | Время: {leader.Time}");
                i += 1;
            }
        }
    }
}
