using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory
{
    internal class Game
    {
        static void Main(string[] args)
        {

            Player player1 = new Player("Stachu", 12);
            Player player2 = new Player("Aniela", 0);

            Board board = new Board(10);

            Animator display = new Animator(board, player1, player2);

            display.displayPlayer(false);

            display.displayBoard();

            display.displayPlayer(true);

            Console.ReadLine();
        }
    }
}
