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

            Player player1 = new Player("Gracz 1", 12);
            Player player2 = new Player("Gracz 2", 0);

            Board board = new Board(6);

            Animator display = new Animator(board.cards, board.size, player1, player2);

            //display.displayPlayer(false);

            display.displayBoard();
            display.displayCardSelection(35, false);
            display.displayCardSelection(16, true);
            display.cover();
            display.displayBoard();

            //display.displayPlayer(true);



            Console.ReadLine();
        }
    }
}
