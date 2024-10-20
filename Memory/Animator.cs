using System;
using System.Collections.Generic;
using System.Linq;
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

        int[][] viewBoard;

        int[] checkedCards = {-1, -1};

        char[] shapes = { '\u25FB', '\u25A7', '\u25B3', '\u25EF', '\u25CA', '\u25BD', '\u25C8', '\u25a3' };
        ConsoleColor[] colors = { ConsoleColor.White, ConsoleColor.Green, ConsoleColor.DarkCyan, ConsoleColor.Magenta, ConsoleColor.DarkYellow, ConsoleColor.DarkRed, ConsoleColor.Yellow };

        public Animator(Card[,] board, int size, Player p1, Player p2)
        {
            Console.OutputEncoding = Encoding.UTF8;

            this.board = board;
            this.size = size;
            player1 = p1;
            player2 = p2;

            viewBoard = new int[board.Length][];

            int idTracker = 0;
            foreach(Card card in board)
            {
                viewBoard[idTracker] = new int[3] {card.id, card.shape, 0};
                idTracker++;
            }   
        }

        public void displayPlayer(bool p)
        {
            string name;
            ConsoleColor color;
            int padding;

            if (!p)
            {
                name = string.Format("{0} [{1}]", player1.name, player1.points);
                padding = 0;
                color = ConsoleColor.Red;
            }
            else
            {
                name = string.Format("[{1}] {0}", player2.name, player2.points);
                padding = size*3;
                color = ConsoleColor.Cyan;
            }

            string hr = new string('=', size*3);

            Console.ForegroundColor = color;

            Console.WriteLine(hr);
            Console.WriteLine(name.PadLeft(padding));
            Console.WriteLine(hr);

            Console.ResetColor();
        }

        public void displayBoard()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;

            int linesCounter = 0;

            foreach (int[] card in viewBoard)
            {
               if(linesCounter % size == 0) { Console.Write("\n"); }

                displayCard(card[1], card[2]);

                linesCounter++;
            }
            Console.WriteLine();

            Console.ResetColor();

        }

        public void displayCardSelection(int id, bool isSelected, bool whichCard)
        {
            resetState();
            if (isSelected)
            {
                uncover(id, whichCard);
            }
            else
            {
                viewBoard[id][2] += 1;
            }
            displayBoard();
        }

        public void uncover(int id, bool whichCard)
        {
            if (!whichCard)
            {//first card
                viewBoard[id][2] = 3;
                checkedCards[0] = id;
            }
            else
            {//second card
                viewBoard[id][2] = 3;
                checkedCards[1] = id;
            }
        }

        public void cover()
        {
            viewBoard[checkedCards[0]][2] = 0;
            viewBoard[checkedCards[1]][2] = 0;

            checkedCards[0] = -1;
            checkedCards[0] = -1;
        }

        private void displayCard(int shape, int state)
        {
            char thumbnail = '\u0023';
            Console.ForegroundColor = ConsoleColor.DarkGray;

            switch (state)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    break;
                case 2:
                    thumbnail = shapes[shape % (shapes.Length - 1)];
                    break;
                case 3:
                    thumbnail = shapes[shape % shapes.Length];
                    Console.ForegroundColor = colors[shape % colors.Length];
                    Console.BackgroundColor = ConsoleColor.Gray;
                    break;
            }
            Console.Write("[{0}]", thumbnail);

            Console.ResetColor();
        }

        private void resetState()
        {
            int indexTracker = 0;

            foreach(int[] card in viewBoard){
                if (Array.IndexOf(checkedCards, indexTracker) > -1){
                    card[2] = 3;
                }
                else
                {
                    switch (card[2])
                    {
                        case 1:
                            card[2] = 0; break;
                        case 3:
                            card[2] = 2; break;
                    }
                }
                indexTracker++;
            }
        }
    }
}
