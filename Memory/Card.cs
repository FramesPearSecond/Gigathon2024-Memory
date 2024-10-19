
namespace Memory
{
    internal class Card
    {
        public static int numOfCards;
        public int id;
        public int shape;
        public bool uncoverd;

        public Card(int id, int shape)
        {
            this.id = id;
            this.shape = shape;
            uncoverd = false;
            numOfCards++;
        }
    }
}
