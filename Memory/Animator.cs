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
            displayPlayer(false, active);
            displayBoard();
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

        void displayBoard()
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
            
            Console.WriteLine(title);

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

            string up = '\u2554' + new string('\u2550', size - 2) + '\u2557';
            string bottom = '\u255A' + new string('\u2550', size - 2) + '\u255D';

            Console.WriteLine(up.PadLeft(padding));

            for(int i = 0; i < options.Length; i++)
            {
                if(i == selected)
                {
                    string option = "\u255F" + new string(' ', (size - options[i].Length-1) / 2) + options[i] + new string(' ', (size - options[i].Length-1) / 2) + "\u2562";

                    Console.WriteLine(option.PadLeft(padding));
                }
                else
                {
                    Console.WriteLine('\u2551' + options[i] + '\u2551');
                }
            }

            Console.WriteLine(bottom.PadLeft(padding));
        }
    }
}
