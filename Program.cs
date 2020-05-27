using System;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            var island = new IslandGen(32, 32, 16, 1, false);
            Console.Write(island.gameOfLife.ToStr());

        }
    }
}
