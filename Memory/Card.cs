
namespace Memory
{
    internal class Card
    {
        public static int numOfCards;
        public int id;
        public int shape;

        public Card(int id, int shape)
        {
            this.id = id;
            this.shape = shape;
            numOfCards++;
        }

        public void Remove()
        {
            numOfCards--;
        }
    }
}
