using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory
{
    internal class Animator
    {
        Board board;
        Player player1;
        Player player2;

        public Animator(Board board, Player p1, Player p2)
        {
            this.board = board;
            player1 = p1;
            player2 = p2;
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
                padding = board.size * 3;
                color = ConsoleColor.Cyan;
            }

            string hr = new string('=', board.size*3);

            Console.ForegroundColor = color;

            Console.WriteLine(hr, "\n");
            Console.WriteLine(name.PadLeft(padding));
            Console.WriteLine(hr, "\n");

            Console.ResetColor();
        }

        public void displayBoard()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.White;

            foreach (Card[] cards in board.cards)
            {
                foreach (Card card in cards)
                {
                    Console.Write("[]");
                }
                Console.Write("\n");
            }
        }
    }
}
