﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Memory
{
    internal class Board
    {
        public Card[,] cards;
        public int size;

        public Card card1;
        public Card card2;

        char[] shapes = { '\u25FB', '\u25A7', '\u25B3', '\u25EF', '\u25CA', '\u25BD', '\u25C8', '\u25a3' };
        ConsoleColor[] colors = {
            ConsoleColor.White,
            ConsoleColor.Green,
            ConsoleColor.DarkCyan,
            ConsoleColor.Magenta,
            ConsoleColor.DarkYellow,
            ConsoleColor.DarkRed,
            ConsoleColor.Yellow
        };

        public Board(int size)
        {
            cards = new Card[size, size];

            this.size = size;

            int cardId = 0;

            for (int j = 0; j < size; j++)
            {    
                for (int i = 0; i < size; i++)
                {

                    int id = cardId;
                    int image = (cardId / 2);
                    char shape = shapes[image % shapes.Length];
                    ConsoleColor color = colors[image % colors.Length];

                    cards[j, i] = new Card(id, shape, color);

                    cardId++;
                }
            }

            shuffling();

        }

        void shuffling()
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
    
        public void isPair(int position)
        {
            if(card1 == null)
            {
                foreach (Card card in cards)
                {
                    if(card.id == position)
                    {
                        card1 = card;
                    }
                }
            }
            else
            {
                foreach (Card card in cards)
                {
                    if (card.id == position)
                    {
                        card2 = card;
                    }
                }

                if(card1.shape == card2.shape)
                {

                }
            }
        }
    }
}
