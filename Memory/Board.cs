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

            cards[0,0].uncoverd = true;
            cards[1,1].uncoverd = true;

            Console.WriteLine(Card.numOfCards);

        }
    }
}
