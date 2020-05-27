namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            var island = new IslandGen(); // create an island class
            island.InteractiveMapCreation(32, 32, .5f, 5, 2, false);
        }
    }
}
