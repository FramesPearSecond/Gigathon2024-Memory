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

        public Board(int size)
        {
            cards = new Card[size][];

            int idCards = 0;

            for (int j = 0; j < size; j++)
            {
                cards[j] = new Card[size];
                
                for (int i = 0; i < cards.Length; i++)
                {

                    cards[j][i] = new Card(idCards, (idCards) / 2);

                    ++idCards;

                    Console.WriteLine("Card {0}; Shape: {1}", cards[j][i].id, cards[j][i].shape);
                }
            }

            Console.WriteLine(Card.numOfCards);

        }
    }
}
