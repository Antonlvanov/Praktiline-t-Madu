using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Praktiline_töö_Madu
{
    public class Menu
    {
        public string MainMenu(Sounds soundMenu)
        {
            string[] menuItems = { "START", "LEADERBOARD", "QUIT" };
            int selectedItem = 0;
            bool menuActive = true;

            while (menuActive)
            {
                DisplayMenu(menuItems, selectedItem);

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    soundMenu.PlaySelect();
                    if (selectedItem != 0) { selectedItem--; }
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    soundMenu.PlaySelect();
                    if (selectedItem != menuItems.Length - 1) { selectedItem++; }
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    soundMenu.PlaySelect();
                    return menuItems[selectedItem];
                }
            }

            return "QUIT";
        }

        // menu display
        private void DisplayMenu(string[] menuItems, int selectedItem)
        {
            string[] gameTitle = new string[]
            {
                "  ____              _        ",
                " / ___| _ __   __ _| | _____ ",
                " \\___ \\| '_ \\ / _` | |/ / _ \\",
                "  ___) | | | | (_| |   <  __/ ",
                " |____/|_| |_|\\__,_|_|\\_\\___|"
            };

            Console.Clear();

            int d = 0;
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (var line in gameTitle)
            {
                Console.SetCursorPosition(25, 5 + d);
                Console.WriteLine(line);
                d++;
            }

            for (int i = 0; i < menuItems.Length; i++)
            {
                if (i == selectedItem)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(32, 13+i);
                    Console.WriteLine($"> {menuItems[i]}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(31, 13+i);
                    Console.WriteLine($"  {menuItems[i]}");
                }
            }
            Console.ResetColor();
        }
    }
}
