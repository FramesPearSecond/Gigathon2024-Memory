using System;
using System.Media;
using System.Threading;

namespace Memory
{
    internal class Game
    {
        Player player1;
        Player player2;

        Board table;
        Animator display;

        Menu menu;

        int size;
        Cursor position;

        Card[] hand = new Card[2];

        bool activePlayer;

        bool exit = false;

        public Game()
        {
            menu = new Menu();

            while (true)
            {
                displayMenu();

                display = new Animator(table.cards, size, player1, player2);

                position = new Cursor();
                position.X = 0;
                position.Y = 0;

                while (player1.points + player2.points < (size * size) / 2 && !exit)
                {
                    round();
                }
                if (exit)
                {
                    continue;
                }
                display.displayEndScreen();
                Console.ReadKey();
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

        void createNewGame()
        {
            string[] gameData = menu.newGameMenu();
            activePlayer = false;
            createPlayers(gameData[0], gameData[1]);
            size = int.Parse(gameData[2]);
            createBoard(size);
        }

        void loadGame()
        {
            SavesMenager.Load(menu.loadMenu(SavesMenager.MenuLoad()), ref table, ref player1, ref player2, ref activePlayer);
            size = table.size;
        }

        void displayMenu()
        {
            menu.mainMenu();

            switch (menu.selectedOption)
            {
                case 0:
                    createNewGame();
                    break;
                case 1:
                    loadGame();
                    break;
                case 2:
                    Environment.Exit(0);
                    break;
            }
        }
        void round()
        {
            Player player  = (activePlayer) ? player2 : player1;
            int points = player.points;

            display.uiDisplay(activePlayer);
            playerSelecting();

        }

        void playerSelecting()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

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

            if (keyInfo.Key == ConsoleKey.Spacebar || keyInfo.Key == ConsoleKey.Enter)
            {
                check();
            }
            if (keyInfo.Modifiers == ConsoleModifiers.Shift && keyInfo.Key == ConsoleKey.S)
            {
                SavesMenager.Save(table.cards, player1, player2, activePlayer);
            }
            if(keyInfo.Key == ConsoleKey.Escape)
            {
                exit = true;
            }

            position.X = (x > size - 1 || x < 0) ? position.X : x;
            position.Y = (y > size - 1 || y < 0) ? position.Y : y;

            display.cardSelection(position);

        }

        void check()
        {
            Player player = (activePlayer) ? player2 : player1;

            if (hand[0] == null)
            {
                hand[0] = table.uncover(position);
            }
            else if (hand[0] != null)
            {
                hand[1] = table.uncover(position);

                if (hand[0].id == hand[1].id)
                {
                    SystemSounds.Beep.Play();
                    hand[1].state = State.Covered;
                }
                else
                if (hand[0].shape == hand[1].shape && hand[0].color == hand[1].color)
                {
                    SystemSounds.Hand.Play();
                    player.points++;

                    hand[0].state = hand[1].state = State.Uncovered;
                }
                else
                {
                    SystemSounds.Beep.Play();
                    display.uiDisplay(activePlayer);
                    Thread.Sleep(1000);

                    activePlayer = !activePlayer;


                    hand[0].state = hand[1].state = State.Covered;
                    

                }
                hand[0] = hand[1] = null;
            }
        }
    }
}
