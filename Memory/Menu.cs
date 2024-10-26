using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory
{
    internal class Menu
    {
        int SelectedOption;

        string[] mainMenu { get; }
        string[] newGameMenu { get; }

        public Menu()
        {
            mainMenu = new string[] { "Nowa gra", "Wczytaj gre", "Wyjdź" };
            newGameMenu = new string[] { "Gracz 1:", "Gracz 2:", "Rozmiar" };

            SelectedOption = 0;
        }

        public void mainMenu_display()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            while (true)
            {
                Console.Clear();
                Animator.displayMainMenu(mainMenu, SelectedOption);
                ConsoleKey key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        SelectedOption = (SelectedOption > 0) ? SelectedOption - 1 : mainMenu.Length - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        SelectedOption = (SelectedOption < mainMenu.Length - 1) ? SelectedOption + 1 : 0;
                        break;
                }
            }
        }
    }
}
