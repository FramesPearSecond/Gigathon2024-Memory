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

        char[] shapes = { '\u25FB', '\u25A7', '\u25B3', '\u25EF', '\u25C8' };

        public Animator(Card[,] board, int size, Player p1, Player p2)
        {
            this.board = board;
            this.size = size;
            player1 = p1;
            player2 = p2;

            viewBoard = new int[board.Length][];

            foreach(Card card in board)
            {
                viewBoard[card.id] = new int[3] {card.id, card.shape, 0};
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
            foreach (int[] card in viewBoard)
            {
                if (card[0] % size == 0 && card[0] != 0)
                {
                    Console.Write("\n");
                }
                Console.Write("[#]");
            }

            Console.WriteLine();
            
        }

        public void displayBoard(int id)
        {
            

            Console.ResetColor();
        }

        private void displayCard(Card card, int id)
        {
            char thumbnail = (card.uncoverd) ? shapes[card.shape] : '#' ;

            if (card.id == id)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("[{0:c}]", thumbnail);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("[{0:c}]", thumbnail);
            }
        }
    }
}
