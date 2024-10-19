using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory
{
    internal class Board
    {
        public Card[][] cards;
        public int size;

        public Board(int size)
        {
            cards = new Card[size][];

            this.size = size;

            int idCards = 0;

            for (int j = 0; j < cards.Length; j++)
            {
                cards[j] = new Card[this.size];
                
                for (int i = 0; i < cards.Length; i++)
                {

                    cards[j][i] = new Card(idCards, (idCards) / 2);

                    ++idCards;
                }
            }

            cards[0][0].uncoverd = true;
            cards[1][1].uncoverd = true;

            Console.WriteLine(Card.numOfCards);

        }
    }
}
