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
            displayPlayer(false, active);
            displayBoard();
            displayPlayer(true, active);
        }

        public void displayPlayer(bool p, bool a)
        {
            string name;
            ConsoleColor color;
            ConsoleColor bgColor;
            int padding;

            if (!p)
            {
                name = string.Format("[{1}]{0}", player1.name, player1.points);
                padding = 0;
                color = ConsoleColor.Red;
                bgColor = (a) ? ConsoleColor.Black : ConsoleColor.Yellow;
            }
            else
            {
                name = string.Format("{0}[{1}]", player2.name, player2.points);
                padding = size*3;
                color = ConsoleColor.Cyan;
                bgColor = (a) ? ConsoleColor.Blue : ConsoleColor.Black;
            }



            string hr = new string('=', size*3);

            Console.ForegroundColor = color;

            Console.WriteLine(hr);

            Console.BackgroundColor = bgColor;
            Console.WriteLine(name.PadLeft(padding));
            Console.BackgroundColor = ConsoleColor.Black;

            Console.WriteLine(hr);

            Console.ResetColor();
        }

        public void displayBoard()
        {

            int linesCounter = 0;

            foreach (Card card in board)
            {
               if(linesCounter % size == 0) { Console.Write("\n"); }

                displayCard(card);

                linesCounter++;
            }
            Console.WriteLine("\n");

            Console.ResetColor();

        }

        public void displayCardSelection(Cursor point)
        {
            resetState();
            
            Card selectedCard = board[point.X, point.Y];

            selectedCard.state = selectCard(selectedCard.state);

            displayBoard();
        }

        private void displayCard(Card card)
        {
            char thumbnail = '\u0023';

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
            Console.Write("[{0}]", thumbnail);

            Console.ResetColor();
        }

        private State selectCard(State card)
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
        private void resetState()
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
    }
}
