using System;

namespace Memory
{
    internal class Card
    {
        public static int numOfCards;
        public int id;
        public char shape;
        public ConsoleColor color;
        public State state;

        public Card(int id, char shape, ConsoleColor color)
        {
            this.id = id;
            this.shape = shape;
            this.color = color;
            this.state = State.Covered;
            numOfCards++;
        }
    }
}
