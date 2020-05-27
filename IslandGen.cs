using System;
using System.Collections.Generic;

namespace GameOfLife
{
    class IslandGen : GameOfLife
    {
        /// <summary>
        /// Generates an island map using Conway's game of life
        /// </summary>


        private void ResetDefaultSettings()
        {
            shouldBeBorn = new HashSet<int> { 3, 4 };
            shouldDie = new HashSet<int> { 0, 1 };
            adjacentCellsMode = 1;
        }

        public void GenerateNewMap(
            int sizeX = 16, int sizeY = 16,
            float landRatio = .5f,
            int smoothness = 4, int finalAdjustmentMode = 1,
            bool surroundByLand = false, int seed = -1)
        {
            /// generates the map
            /// 
            /// sizeX and sizeY set the map size
            /// landRation is the ration between land and water
            /// smoothness sets how smooth the coastlines will be
            /// surroundByLand sets if land will be on sidesc of the map (good for wrapping)
            /// checkDiagona; sets if you should be checking in diagonals or not
            /// finalAdjustmentMode changes how the map will be smoothed out on the final iteration
            ///   0: none - doesn't smooth out
            ///   1: land - connects islands
            ///   2: water - seperates islands

            this.sizeX = sizeX; this.sizeY = sizeY;
            grid = CreateRandomGrid(landRatio, seed);
            gridOverflow = surroundByLand ? 1 : 0;
            ResetDefaultSettings();

            // iterate smoothness amount of times, to smooth out the land
            for (int i = 0; i < smoothness; i++)
            {
                NextGeneration();
            }

            // adjust the island to remove ugly parts
            if (finalAdjustmentMode != 0)
            {
                if (finalAdjustmentMode == 1)
                    shouldDie = new HashSet<int> { }; // no more water generation
                else if (finalAdjustmentMode == 2)
                    shouldBeBorn = new HashSet<int> { }; // no more land generation
                NextGeneration();
            }
            ResetDefaultSettings();
        }

        public int[,] GetRandomMap(
            int sizeX = 16, int sizeY = 16,
            float landRatio = .5f,
            int smoothness = 4, int finalAdjustmentMode = 1,
            bool surroundByLand = false, int seed = -1)
        {
            /// works like GenerateNewMap, but returns a map matrix
            /// can be used if you want multiple maps

            this.sizeX = sizeX; this.sizeY = sizeY;
            grid = CreateRandomGrid(landRatio, seed);
            gridOverflow = surroundByLand ? 1 : 0;
            ResetDefaultSettings();

            // iterate smoothness amount of times, to smooth out the land
            for (int i = 0; i < smoothness; i++)
            {
                NextGeneration();
            }

            // adjust the island to remove ugly parts
            if (finalAdjustmentMode != 0)
            {
                if (finalAdjustmentMode == 1)
                    shouldDie = new HashSet<int> { }; // no more water generation
                else if (finalAdjustmentMode == 2)
                    shouldBeBorn = new HashSet<int> { }; // no more land generation
                NextGeneration();
            }
            ResetDefaultSettings();
            try
            {
                return this.grid;
            }
            finally
            {
                this.grid = CreateEmptyGrid();
            }
        }
        public int[,] InteractiveMapCreation(
            int sizeX = 16, int sizeY = 16,
            float landRatio = .5f,
            int smoothness = 4, int finalAdjustmentMode = 1,
            bool surroundByLand = false, int seed = -1)
        {
            /// works like GetRandomMap, but it's interactive and you can see the map being created

            this.sizeX = sizeX; this.sizeY = sizeY;
            grid = CreateRandomGrid(landRatio, seed);
            gridOverflow = surroundByLand ? 1 : 0;
            ResetDefaultSettings();

            Console.WriteLine(ToStr());
            Console.ReadLine();
            // iterate smoothness amount of times, to smooth out the land
            for (int i = 0; i < smoothness; i++)
            {
                NextGeneration();
                Console.WriteLine(ToStr());
                Console.ReadLine();
            }

            // adjust the island to remove ugly parts
            if (finalAdjustmentMode != 0)
            {
                if (finalAdjustmentMode == 1)
                    shouldDie = new HashSet<int> { }; // no more water generation
                else if (finalAdjustmentMode == 2)
                    shouldBeBorn = new HashSet<int> { }; // no more land generation
                NextGeneration();
                Console.WriteLine(ToStr());
            }
            ResetDefaultSettings();

            try
            {
                return this.grid;
            }
            finally
            {
                this.grid = CreateEmptyGrid();
            }
        }
    }
}
