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

        public void uiDisplay()
        {
            Console.Clear();
            displayPlayer(false);
            displayBoard();
            displayPlayer(true);

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

            int linesCounter = 0;

            foreach (Card card in board)
            {
               if(linesCounter % size == 0) { Console.Write("\n"); }

                displayCard(card);

                linesCounter++;
            }
            Console.WriteLine();

            Console.ResetColor();

        }

        public void displayCardSelection(Cursor point)
        {
            resetState();
            
            Card selectedCard = board[point.X, point.Y];

            selectCard(selectedCard.state);

            displayBoard();
        }

        //public void uncover(int id, bool whichCard)
        //{
        //    if (!whichCard)
        //    {//first card
        //        viewBoard[id][2] = 3;
        //        checkedCards[0] = id;
        //    }
        //    else
        //    {//second card
        //        viewBoard[id][2] = 3;
        //        checkedCards[1] = id;
        //    }
        //}

        //public void cover()
        //{
        //    viewBoard[checkedCards[0]][2] = 0;
        //    viewBoard[checkedCards[1]][2] = 0;

        //    checkedCards[0] = -1;
        //    checkedCards[0] = -1;
        //}

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
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    break;
                case State.Uncovered:
                    thumbnail = card.shape;
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case State.UncoveredSelected:
                    thumbnail = card.shape;
                    Console.ForegroundColor = card.color;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    break;
                case State.Choosed:
                    thumbnail = card.shape;
                    Console.ForegroundColor = card.color;
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
