using System.Collections.Generic;

namespace GameOfLife
{
    class IslandGen
    {
        public GameOfLife gameOfLife;
        public IslandGen(
            int sizeX, int sizeY,
            int smoothness = 4, int finalAdjustmentMode = 1,
            bool surroundByLand = false)
        {
            /// generates the map
            /// 
            /// smoothness sets how smooth the coastlines will be
            /// surroundByLand sets if land will be on sidesc of the map (good for wrapping)
            /// checkDiagona; sets if you should be checking in diagonals or not
            /// finalAdjustmentMode changes how the map will be smoothed out on the final iteration
            ///   0: none - doesn't smooth out
            ///   1: land - connects islands
            ///   2: water - seperates islands
            ///   

            // initialize
            gameOfLife = new GameOfLife(sizeX, sizeY, .5f)
            {
                adjacentCellsMode = 1,
                shouldBeBorn = new HashSet<int> { 3, 4 },
                shouldDie = new HashSet<int> { 0, 1 },

                gridOverflow = surroundByLand ? 1 : 0
            };

            // generate
            for (int i = 0; i < smoothness; i++)
            {
                gameOfLife.NextGeneration();
            }
            if (finalAdjustmentMode != 0)
            {
                if (finalAdjustmentMode == 1)
                    gameOfLife.shouldDie = new HashSet<int> { }; // no more water generation
                else if (finalAdjustmentMode == 2)
                    gameOfLife.shouldBeBorn = new HashSet<int> { }; // no more land generation
                gameOfLife.NextGeneration();
            }
            gameOfLife.shouldBeBorn = new HashSet<int> { 3, 4 };
            gameOfLife.shouldDie = new HashSet<int> { 0, 1 };
        }
    }
}
