using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory
{
    internal class Menu
    {
        public int selectedOption;

        string[] mainMenuOptions { get; }
        string[] newGameMenuOptions { get; }

        public Menu()
        {
            mainMenuOptions = new string[] { "Nowa gra", "Wczytaj grę", "Zamknij" };
            newGameMenuOptions = new string[] { "Gracz 1:", "Gracz 2:", "Rozmiar:" };

            selectedOption = 0;
        }

        public void mainMenu()
        {
            bool notSelected = true;

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            while (notSelected)
            {
                Console.Clear();
                Animator.displayMainMenu(mainMenuOptions, selectedOption);
                ConsoleKey key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        selectedOption = (selectedOption > 0) ? selectedOption - 1 : mainMenuOptions.Length - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedOption = (selectedOption < mainMenuOptions.Length - 1) ? selectedOption + 1 : 0;
                        break;
                    case ConsoleKey.Enter:
                    case ConsoleKey.Spacebar:
                        notSelected = false;
                        break;
                }
            }
        }

        public string[] newGameMenu()
        {
            int stage = 0;
            string[] newGamedata = new string[3];
            string input = "";
            newGamedata[2] = "2";


            while (stage != 3)
            {
                Console.Clear();
                Animator.displayNewGameMenu(newGameMenuOptions, newGamedata, stage);
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);


                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    if(input.Length > 0)
                    {
                        newGamedata[stage] = input;
                        input = "";
                    }
                    else if(stage != 2)
                    {
                        newGamedata[stage] = $"Gracz{stage+1}";
                    }
                    stage++;
                }
                else if (stage == 2)
                {
                    int size = int.Parse(newGamedata[2]);
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            size = (size > 2) ? size - 2 : 10;
                            break;
                        case ConsoleKey.RightArrow:
                            size = (size < 10) ? size + 2 : 2;
                            break;
                    }
                    newGamedata[2] = size.ToString();
                }
                else
                {
                    if (keyInfo.Key == ConsoleKey.Backspace && input.Length > 0)
                    {
                        input = input.Remove(input.Length - 1);
                    }
                    else if(char.IsLetter(keyInfo.KeyChar) && input.Length <= 12)
                    {
                        input += keyInfo.KeyChar;
                    }
                    newGamedata[stage] = input;

                }

            }
            return newGamedata;
        }
    }
}
