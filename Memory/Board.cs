using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory
{
    internal class Board
    {
        public Card[,] cards;
        public int size;

        public Board(int size)
        {
            cards = new Card[size, size];

            this.size = size;

            int cardId = 0;

            for (int j = 0; j < size; j++)
            {    
                for (int i = 0; i < size; i++)
                {
                    cards[j,i] = new Card(cardId, (cardId) / 2);

                    cardId++;
                }
            }

            shuffling();

            //foreach (Card card in cards)
            //{
            //    Console.WriteLine(card.id);
            //}

            //Console.WriteLine(Card.numOfCards);

        }

        private void shuffling()
        {
            Card[,] shuffeld = new Card[size, size];

            Random r = new Random();
            bool[,] positions = new bool[size,size];

            int newRow;
            int newCol;
            

            foreach(Card card in this.cards)
            {
                //Console.WriteLine(card.id);
                do
                {
                    newCol = r.Next(0, size);
                    newRow = r.Next(0, size);
                }
                while (positions[newRow, newCol]);

                shuffeld[newRow, newCol] = card;
                positions[newRow, newCol] = true;
                //Console.WriteLine("[{0},{1}] {2}", newRow, newCol, shuffeld[newRow, newCol].id);
            }

            this.cards = shuffeld;
        }
    }
}
