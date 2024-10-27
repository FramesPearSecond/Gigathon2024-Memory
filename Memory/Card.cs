using System;

namespace Memory
{
    internal class Card
    {
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
        }
        public Card(int id, char shape, ConsoleColor color, State state)
        {
            this.id = id;
            this.shape = shape;
            this.color = color;
            this.state = state;
        }
    }
}
