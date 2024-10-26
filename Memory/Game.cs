using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory
{
    internal class Game
    {
        Player player1;
        Player player2;

        Board table;
        Animator display;

        int size;
        int position;

        public Game()
        {
            size = 6;

            int points = (size * size)/2;

            createBoard(size);
            createPlayers("superMario64", "Jack123");

            display = new Animator(table.cards, 6, player1, player2);

            bool player = false;

            display.uiDisplay();
            Console.ReadKey();
        }
        void createBoard(int size)
        {
            table = new Board(size);
        }
        void createPlayers(string p1, string p2)
        {
            player1 = new Player(p1, 0);
            player2 = new Player(p2, 0);
        }

        void round(bool player)
        {
            Player activePlayer = (player) ? player2 : player1;

            

        }

        //void playerSelecting()
        //{

        //    ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

        //    int select = 0;

        //    switch (keyInfo.Key)
        //    {
        //        case ConsoleKey.UpArrow:
        //            select = position - size;
        //            break;
        //        case ConsoleKey.DownArrow:
        //            select = position + size;
        //            break;
        //        case ConsoleKey.LeftArrow:
        //            select = position - 1;
        //            break;
        //        case ConsoleKey.RightArrow:
        //            select = position + 1;
        //            break;
        //    }

        //    position = (select < 0 || select > (size * size)-1) ? position : select;

        //    if(keyInfo.Key == ConsoleKey.Spacebar)
        //    {
        //        if (positions[0] != -1)
        //        {
        //            positions[0] = position;
        //        }
        //        else if (positions[0] != position)
        //        {
        //            positions[1] = position;
        //        }
        //    }

        //}

    }
}
