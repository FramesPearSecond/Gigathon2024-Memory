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
            display.displayCardSelection(35, false, false);
            display.displayCardSelection(34, false, false);
            display.displayCardSelection(33, false, false);
            display.displayCardSelection(32, false, false);
            display.displayCardSelection(32, true, false);
            display.displayCardSelection(26, false, true);
            display.displayCardSelection(20, false, true);
            display.displayCardSelection(20, true, true);
            display.cover();
            display.displayBoard();

            //display.displayPlayer(true);



            Console.ReadLine();
        }
    }
}
