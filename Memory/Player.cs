
namespace Memory
{
    internal class Player
    {
        public string name { get; }
        public int points { get; set; }


        public Player(string name, int points)
        {
            this.name = name;
            this.points = points;
        }
    }
}
