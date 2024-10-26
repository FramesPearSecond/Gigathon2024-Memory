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
        Cursor position;

        public Game()
        {
            size = 6;

            int points = (size * size)/2;

            createBoard(size);
            createPlayers("superMario64", "Jack123");

            display = new Animator(table.cards, 6, player1, player2);

            position = new Cursor();
            position.X = 0;
            position.Y = 0;

            bool player = false;

            while (true)
            {
                playerSelecting();
            }
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

        void playerSelecting()
        {

            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

            int x = position.X;
            int y = position.Y;

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    x -=1;
                    break;
                case ConsoleKey.DownArrow:
                    x += 1;
                    break;
                case ConsoleKey.LeftArrow:
                    y -= 1;
                    break;
                case ConsoleKey.RightArrow:
                    y += 1;
                    break;
            }

            Console.WriteLine("X:{0} Y:{1}", x, y);
            position.X = (x > size-1 || x < 0) ? position.X : x;
            position.Y = (y > size-1 || y < 0) ? position.Y : y;

            display.displayCardSelection(position);

        }
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
