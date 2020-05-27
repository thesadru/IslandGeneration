using System;
using System.Collections.Generic;

namespace GameOfLife
{
    public class GameOfLife
    {
        /// <summary>
        /// this is a simulation of conway's game of life
        /// all public variables can are used as settings, since I have no idea what I'm doing
        /// to preview how the function works, try .Run()
        /// </summary>

        public int sizeX, sizeY;
        public int[,] grid;
        public string charTable = " #";
        public HashSet<int> shouldBeBorn = new HashSet<int> { 2, 3 };
        public HashSet<int> shouldDie = null;
        public int adjacentCellsMode = 0;
        public int gridOverflow = 0;

        public GameOfLife(
            int sizeX = 16, int sizeY = 16,
            float aliveCellChance = 0)
        {
            /// choose grid size and chance of a cell to be alive
            this.sizeX = sizeX; this.sizeY = sizeY;
            if (aliveCellChance == 0)
            {
                this.grid = ResetGrid();

            }
            else
            {
                this.grid = CreateRandomGrid(aliveCellChance);
            }
        }

        public void Run(int cycles = 2048, bool pretty = true)
        {
            /// preview how a conways game of life would play out
            void print()
            {
                /// print the board and wait for input
                Console.Write(pretty ? ToPrettyStr() : ToStr());
                Console.ReadLine();
            }
            print();
            for (int cycle = 0; cycle < cycles; cycle++)
            {
                NextGeneration();
                print();
            }
        }

        public int[,] ResetGrid()
        {
            /// reset the grid
            return new int[sizeY, sizeX];
        }

        public int[,] CreateRandomGrid(float chance)
        {
            /// generate a random board
            /// every cell has a chace of being alive stated by the variable chance
            int[,] final = ResetGrid();
            Random random = new Random();

            for (int x = 0; x < sizeY; x++)
            {
                for (int y = 0; y < sizeX; y++)
                {
                    // chance that cell will be alive
                    final[x, y] = chance > random.NextDouble() ? 1 : 0;
                }
            }
            return final;
        }

        public string ToStr(
            string strStart = "", string lineEnd = "\n",
            string charStart = "", string charEnd = " ", string strEnd = "")
        {
            /// converts the board to string
            string output = strStart;
            for (int x = 0; x < sizeY; x++)
            {
                for (int y = 0; y < sizeX; y++)
                {
                    output += charStart + charTable[grid[x, y]] + charEnd;
                }
                output += lineEnd;
            }
            return output + strEnd;
        }

        public string ToPrettyStr(char horizontalSep = '-', char verticalSep = '|')
        {
            /// converts class to string, but it's decorated with a pretty box
            string basic = new string(horizontalSep, sizeX * 4) + verticalSep + "\n";
            string lineEnd = verticalSep + "\n" + basic;
            return ToStr(strStart: basic, lineEnd: lineEnd, charStart: "| ");
        }

        public void NextGeneration()
        {
            /// does a generation cycle
            int[,] future = ResetGrid();

            // Loop through every cell 
            for (int x = 1; x < sizeY - 1; x++)
            {
                for (int y = 1; y < sizeX - 1; y++)
                {
                    // make cell alive if it has the right amount of neighbors
                    int aliveNeighbors = AliveNeighbors(x, y);
                    future[x, y] = SetCellByNeigbor(x, y, aliveNeighbors);
                }
            }
            this.grid = future;
        }
        private int SetCellByNeigbor(int x, int y, int aliveNeighbors)
        {
            if (shouldDie == null)
                return shouldBeBorn.Contains(aliveNeighbors) ? 1 : 0;

            else
                if (grid[x, y] == 0)
                return shouldBeBorn.Contains(aliveNeighbors) ? 1 : 0;
            else
                return shouldDie.Contains(aliveNeighbors) ? 0 : 1;

        }
        public int AliveNeighbors(int x, int y)
        {
            /// returns the number of alive neighbors
            /// 
            /// you can change the mode by changing adjacentCellsMode
            /// modes:
            ///  0: default - checks all 8 neighbors around itself
            ///  1: 4-way - checks only 4 neighbors around itself
            ///  2: 2 far: detects all cells in a 5x5 grid around the cell
            ///  
            int aliveNeighbors = 0;
            if (adjacentCellsMode == 0)
            {
                for (int i = -1; i <= 1; i++)
                    for (int j = -1; j <= 1; j++)
                    {
                        i += x; j += y;
                        if (
                            0 <= i && i < sizeY &&
                            0 <= j && j < sizeX
                        )
                            aliveNeighbors += grid[i, j]; // doesn't overflow
                        else
                            aliveNeighbors += gridOverflow; // does overflow
                    }
                aliveNeighbors -= grid[x, y]; // remove itself

            }
            else if (adjacentCellsMode == 1)
            {
                int[] d = new int[2] { -1, 1 };
                int i, j;

                for (int a = 0; a <= 1; a++)
                    for (int b = 0; b <= 1; b++)
                    {
                        i = x + d[a] * (0 ^ b);
                        j = y + d[a] * (1 ^ b);
                        if (
                            0 <= i && i < sizeY &&
                            0 <= j && j < sizeX
                        )
                            aliveNeighbors += grid[i, j]; // doesn't overflow
                        else
                            aliveNeighbors += gridOverflow; // does overflow
                    }

            }
            else if (adjacentCellsMode == 2)
            {
                for (int i = -2; i <= 2; i++)
                    for (int j = -2; j <= 2; j++)
                    {
                        i += x; j += y;
                        if (
                            0 <= i && i < sizeY &&
                            0 <= j && j < sizeX
                        )
                            aliveNeighbors += grid[i, j]; // doesn't overflow
                        else
                            aliveNeighbors += gridOverflow; // does overflow
                    }
                aliveNeighbors -= grid[x, y]; // remove itself

            }
            else
            {
                throw new Exception("selected mode unavalible");
            }
            return aliveNeighbors;
        }
    }
}