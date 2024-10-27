using System;
using System.Text;

namespace Memory
{
    internal class Animator
    {
        Card[,] board;
        Player player1;
        Player player2;

        int size;

        static int width = 34;

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
            displayTitle();
            displayPlayer(false, active);
            displayBoard();
            displayPlayer(true, active);
            displayFooter();
        }

        void displayPlayer(bool p, bool a)
        {
            string name;
            ConsoleColor color;
            ConsoleColor fontColor;
            ConsoleColor bgColor;
            int padding;

            string hr = new string('\u2550', 32);
            string upHr = '\u2552' + hr + '\u2555';
            string boHr = '\u2558' + hr + '\u255B';

            if (!p)
            {
                name = string.Format("[{1}] {0}", player1.name, player1.points);
                padding = 0;
                color = ConsoleColor.Red;
                if (!a)
                {
                    fontColor = ConsoleColor.Black;
                    bgColor = ConsoleColor.Yellow;
                }
                else
                {
                    fontColor = ConsoleColor.Red;
                    bgColor = ConsoleColor.Black;
                }
            }
            else
            {
                name = string.Format("{0} [{1}]", player2.name, player2.points);
                padding = 68;
                color = ConsoleColor.Cyan;
                if (a)
                {
                    fontColor = ConsoleColor.Black;
                    bgColor = ConsoleColor.Yellow;
                }
                else
                {
                    fontColor = ConsoleColor.Cyan;
                    bgColor = ConsoleColor.Black;
                }
            }

            Console.ForegroundColor = color;

            Console.WriteLine(upHr.PadLeft(padding));
            Console.Write(" ".PadLeft((padding > 0)? padding - name.Length - 1 : 0));

            Console.BackgroundColor = bgColor;
            Console.ForegroundColor = fontColor;
            Console.Write(name+"\n");

            Console.ResetColor();

            Console.ForegroundColor = color;
            Console.WriteLine(boHr.PadLeft(padding));

            Console.ResetColor();
        }

        void displayBoard()
        {
            int linesCounter = 0;

            foreach (Card card in board)
            {
                if(linesCounter % size == 0){
                    Console.Write("\n");
                    Console.Write("".PadLeft((width - size)-3));
                }

                Console.Write(displayCard(card));

                Console.ResetColor();

                linesCounter++;
            }
            Console.WriteLine("\n");

            Console.ResetColor();

        }

        public static void displayTitle()
        {
            string title = 
            " __    __     ______     __    __     ______     ______     __  __    \n" +
            "/\\ \"-./  \\   /\\  ___\\   /\\ \"-./  \\   /\\  __ \\   /\\  == \\   /\\ \\_\\ \\   \n" +
            "\\ \\ \\-./\\ \\  \\ \\  __\\   \\ \\ \\-./\\ \\  \\ \\ \\/\\ \\  \\ \\  __<   \\ \\____ \\  \n" +
            " \\ \\_\\ \\ \\_\\  \\ \\_____\\  \\ \\_\\ \\ \\_\\  \\ \\_____\\  \\ \\_\\ \\_\\  \\/\\_____\\ \n" +
            "  \\/_/  \\/_/   \\/_____/   \\/_/  \\/_/   \\/_____/   \\/_/ /_/   \\/_____/ ";
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(title);
            Console.ResetColor();
            Console.WriteLine(("Gigathon 2024").PadLeft(width*2) + "\n");
        }

        void displayFooter()
        {
            string save = "Zapis [Shift+S]";
            string exit = "Wyjście [Esc]";


            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(save + exit.PadLeft((width*2)-save.Length));
            Console.ResetColor();
        }
        public void cardSelection(Cursor point)
        {
            resetState();
            
            Card selectedCard = board[point.X, point.Y];

            selectedCard.state = selectCard(selectedCard.state);

        }

        string displayCard(Card card)
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

            return string.Format("[{0}]", thumbnail);
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
        
        public static void DisplayMenu(string[] options, int selected)
        {
            displayTitle();
            int padding = (int)(width * 1.5);

            string up = '\u2565' + new string('\u2500', width - 2) + '\u2565';
            string bottom = '\u2568' + new string('\u2500', width - 2) + '\u2568';

            Console.WriteLine(up.PadLeft(padding));

            for(int i = 0; i < options.Length; i++)
            {
                int optionPadding = (width - options[i].Length - 1) / 2;
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
            displayTitle();

            int hrPadding = width + 18;
            string hrUp = '\u2565' + new string('\u2500', 34) + '\u2565';
            string hrBottom = '\u2568' + new string('\u2500', 34) + '\u2568';

            Console.WriteLine(hrUp.PadLeft(hrPadding));

            for (int i = 0; i < options.Length; i++)
            {
                int addPadding = (input[i] != null) ? input[i].Length : 0;
                string rightPadding = new string(' ', width - addPadding - options[i].Length - 1);

                if (i == stage)
                {
                    string option = string.Format("{0} {1}", options[i], input[i]);
                    Console.Write("\u255F".PadLeft(width - option.Length - options[i].Length + addPadding));

                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;

                    Console.Write(option + rightPadding);
                    Console.ResetColor();
                    Console.Write("\u2562\n");
                }
                else
                {
                    string option = string.Format("{2}{0} {1}", options[i], input[i], "\u2551");
                    Console.Write(option.PadLeft(width - options[i].Length + addPadding) + rightPadding);
                    Console.ResetColor();
                    Console.Write("\u2551\n");
                }
            }

            Console.WriteLine(hrBottom.PadLeft(hrPadding));
        }

        public void displayEndScreen()
        {
            Console.Clear();
            displayTitle();

            string winner;

            if(player1.points > player2.points)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                winner = player1.name;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                winner = player2.name;
            }

            string text = string.Format("Wygrywa {0}!", winner);

            Console.WriteLine(text.PadLeft(width+(text.Length)/2));
            
        }
    }
}
