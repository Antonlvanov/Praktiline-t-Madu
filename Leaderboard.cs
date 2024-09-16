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

            public Leader(string name, int score)
            {
                Name = name;
                Score = score;
            }

            public override string ToString()
            {
                return $"{Name}: {Score}";
            }
        }

        public void SaveLeaders(List<Leader> leaders)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var leader in leaders)
                {
                    writer.WriteLine($"{leader.Name},{leader.Score}");
                }
            }
        }
        public List<Leader> LoadLeaders()
        {
            var leaders = new List<Leader>();

            if (!File.Exists(filePath))
            {
                return leaders;
            }

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int score))
                    {
                        leaders.Add(new Leader(parts[0], score));
                    }
                }
            }

            return leaders.OrderByDescending(l => l.Score).ToList();
        }

        public void AddLeader(string name, int score)
        {
            var leaders = LoadLeaders();
            leaders.Add(new Leader(name, score));

            leaders = leaders.OrderByDescending(l => l.Score).Take(10).ToList();

            SaveLeaders(leaders);
        }

        public void DisplayLeaders()
        {
            var leaders = LoadLeaders();

            Console.WriteLine("Leaderboard:");
            foreach (var leader in leaders)
            {
                Console.WriteLine(leader);
            }
        }
    }
    //StreamWriter to_file = new StreamWriter("Vastused.txt", true);
    //for (int i = 0; i<v.Length; i++)
    //{
    //    to_file.WriteLine(v[i]);
    //}
    //to_file.Close();
    //StreamReader from_file = new StreamReader("Vastused.txt");
    //string text = from_file.ReadToEnd();
    //Console.WriteLine(text);
    //from_file.Close();
    //Console.ReadLine();
}
