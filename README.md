# IslandGeneration
My first complicated c# script.
It generates islands using conway's game of life (cellural automata). This allows for some smooth terrain generation without using noises.
# where I got the idea
I once implemented conway's game of life in python and decided, that I could now do it in c#.
I played with the code a bit and added some useless features. When I played with them a bit, a coherent thing started generating.
# how to use it
To generate islands using cellural automata you can use these snippets:
```cs
// this code generates a matrix
// access the matrix with variable newMap
var generator = new GenIsland();
int[,] newMap = generator.GetRandomMap();
```
```cs
// this code updates the class directly
// access the matrix with generator.grid
var generator = new GenIsland();
generator.GenerateNewMap();
```
```cs
// this code is interactive
// go through each stage by pressing Enter
var generator = new GenIsland();
int[,] newMap = generator.InteractiveMapCreation();
```
# can I use this?
Yes, I am more than grateful that someone found a use for this ~~piece of shit~~ cool code.
