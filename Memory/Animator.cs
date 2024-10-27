using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Memory
{
    internal class Animator
    {
        Card[,] board;
        Player player1;
        Player player2;

        int size;

        public Animator(Card[,] board, int size, Player p1, Player p2)
        {
            Console.OutputEncoding = Encoding.UTF8;

            this.board = board;
            this.size = size;
            player1 = p1;
            player2 = p2;
        }

        public void uiDisplay(bool active)
        {
            Console.Clear();
            string tilte = displayTitle();
            displayPlayer(false, active);
            displayBoard(tilte.Length/8);
            displayPlayer(true, active);
        }

        void displayPlayer(bool p, bool a)
        {
            string name;
            ConsoleColor color;
            ConsoleColor bgColor;
            int padding;

            if (!p)
            {
                name = string.Format("[{1}]{0}", player1.name, player1.points);
                padding = 0;
                color = ConsoleColor.Red;
                bgColor = (a) ? ConsoleColor.Black : ConsoleColor.Yellow;
            }
            else
            {
                name = string.Format("{0}[{1}]", player2.name, player2.points);
                padding = size*3;
                color = ConsoleColor.Cyan;
                bgColor = (a) ? ConsoleColor.Blue : ConsoleColor.Black;
            }



            string upHr = new string('\u2550', (size * 3)-2);
            upHr = '\u2552' + upHr + '\u2555';

            string boHr = new string('\u2550', (size * 3) - 2);
            boHr = '\u2558' + boHr + '\u255B';

            Console.ForegroundColor = color;

            Console.WriteLine(upHr);

            Console.BackgroundColor = bgColor;
            Console.WriteLine(name.PadLeft(padding));
            Console.BackgroundColor = ConsoleColor.Black;

            Console.WriteLine(boHr);

            Console.ResetColor();
        }

        void displayBoard(int padding)
        {

            int linesCounter = 0;

            foreach (Card card in board)
            {
               if(linesCounter % size == 0) { Console.Write("\n"); }

                displayCard(card);

                linesCounter++;
            }
            Console.WriteLine("\n");

            Console.ResetColor();

        }

        public static string displayTitle()
        {
            string title = 
            " __    __     ______     __    __     ______     ______     __  __    \n" +
            "/\\ \"-./  \\   /\\  ___\\   /\\ \"-./  \\   /\\  __ \\   /\\  == \\   /\\ \\_\\ \\   \n" +
            "\\ \\ \\-./\\ \\  \\ \\  __\\   \\ \\ \\-./\\ \\  \\ \\ \\/\\ \\  \\ \\  __<   \\ \\____ \\  \n" +
            " \\ \\_\\ \\ \\_\\  \\ \\_____\\  \\ \\_\\ \\ \\_\\  \\ \\_____\\  \\ \\_\\ \\_\\  \\/\\_____\\ \n" +
            "  \\/_/  \\/_/   \\/_____/   \\/_/  \\/_/   \\/_____/   \\/_/ /_/   \\/_____/ ";
            
            Console.WriteLine(title+"\n");

            return title;
        }

        public void displayCardSelection(Cursor point)
        {
            resetState();
            
            Card selectedCard = board[point.X, point.Y];

            selectedCard.state = selectCard(selectedCard.state);

            displayBoard();
        }

        void displayCard(Card card)
        {
            char thumbnail = '\u2573';

            switch (card.state)
            {
                case State.Covered:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case State.CoveredSelected:
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Gray;
                    break;
                case State.Uncovered:
                    thumbnail = card.shape;
                    Console.ForegroundColor =card.color;
                    break;
                case State.UncoveredSelected:
                    thumbnail = card.shape;
                    Console.ForegroundColor = card.color;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    break;
                case State.Choosed:
                    thumbnail = card.shape;
                    Console.ForegroundColor = card.color;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    break;
                case State.ChoosedSelected:
                    thumbnail = card.shape;
                    Console.ForegroundColor = card.color;
                    Console.BackgroundColor = ConsoleColor.Gray;
                    break;
            }
            Console.Write("[{0}]", thumbnail);

            Console.ResetColor();
        }

        State selectCard(State card)
        {
            switch (card)
            {
                case State.Uncovered:
                    return State.UncoveredSelected;
                    break;
                case State.Choosed:
                    return State.ChoosedSelected;
                    break;
                default:
                    return State.CoveredSelected;
                    break;
            }
        }
        void resetState()
        {
            foreach(Card card in board){
                switch (card.state)
                {
                    case State.CoveredSelected:
                        card.state = State.Covered;
                        break;
                    case State.UncoveredSelected:
                        card.state = State.Uncovered;
                        break;
                    case State.ChoosedSelected:
                        card.state = State.Choosed;
                        break;
                }   
            }
        }
        
        public static void displayMainMenu(string[] options, int selected)
        {
            
            string title = displayTitle();
            int size = title.Length / 16;
            int padding = size * 2;

            string up = '\u2565' + new string('\u2500', size - 2) + '\u2565';
            string bottom = '\u2568' + new string('\u2500', size - 2) + '\u2568';

            Console.WriteLine(up.PadLeft(padding));

            for(int i = 0; i < options.Length; i++)
            {
                int optionPadding = (size - options[i].Length - 1) / 2;
                string padLeft = new string(' ', optionPadding);
                string padRight = (options[i].Length % 2 == 0) ? padLeft : new string(' ', optionPadding-1);
                string option = padLeft + options[i] + padRight;

                if (i == selected)
                {
                    Console.Write("\u255F".PadLeft(padding-option.Length-1));
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(option);
                    Console.ResetColor();
                    Console.Write("\u2562\n");
                }
                else
                {
                    option = "\u2551" + option + "\u2551";
                    Console.WriteLine(option.PadLeft(padding));
                }
                

            }

            Console.WriteLine(bottom.PadLeft(padding));
        }

        public static void displayNewGameMenu(string[] options, string[] input, int stage)
        {
            string title = displayTitle();
            int padding = title.Length / 9;

            string up = '\u2565' + new string('\u2500', 20);
            string bottom = '\u2568' + new string('\u2500', 20);

            Console.WriteLine(up.PadLeft(padding+3));

            for (int i = 0; i < options.Length; i++)
            {
                int addPadding = (input[i] != null) ? input[i].Length : 0; 

                if (i == stage)
                {
                    string option = string.Format("{0} {1}", options[i], input[i]);
                    Console.Write("\u255F".PadLeft(padding - option.Length - options[i].Length + addPadding));
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(option+"\n");
                    Console.ResetColor();
                }
                else
                {
                    string option = string.Format("{2}{0} {1}", options[i], input[i], "\u2551");
                    Console.WriteLine(option.PadLeft(padding - options[i].Length + addPadding));
                }
            }

            Console.WriteLine(bottom.PadLeft(padding+3));

            //Console.WriteLine("1.{0} {3}\n1.{1} {4}\n1.{2} {5}\n",
            //    options[0], options[1], options[2], input[0], input[1], input[2]);
        }
    }
}
