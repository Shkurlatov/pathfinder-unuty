# Pathfinder

![Header](docs/media/project_demo.gif)

The repository contains an implementation of the Test task.

## Task conditions

Create an application in Unity that will create a simulation of building a graph
and finding the shortest path in it using Dijkstra's algorithm.

## Task requirements

Application design is at the discretion of the developer. The main thing is that
the application meets the following requirements:

* The application must be adapted to be controlled using a mobile device

* The user should be able to manually create an arbitrary graph on the screen and
run a procedure for finding the shortest path between two selected graph nodes

* Finding the shortest path between graph nodes should be implemented using
Dijkstra's algorithm

## Implementation details

Description of the application implementation with user manual:

* To create a node in the simulation, tap the screen at the point where you want
to place the node

* A node will not be created if you tap the screen too close to an existing node

* Each node is numbered in the order in which it was created

* The maximum number of nodes is limited only by screen size

* To connect two nodes to each other, touch the screen next to the desired node
and swipe, drawing a line to the next node

* As a result, a connection is formed, which will display information about the
distance between the nodes

* Nodes can be connected to each other in any arbitrary order.

* You cannot create a connection between nodes that are already connected to each other

* The "Build Graph" interface button will only be active if every node on the screen has at least one connection

* After touching the "Build Graph" button, the application enters the path search state

* To find the shortest path, touch any two nodes in turn, and the shortest path between them along the corresponding connections will be highlighted on the screen

* To return the simulation to its initial state, tap the "Restart" button

## Pathfinder algorithm

To calculate the shortest path between nodes, the program uses an almost entirely
original implementation of Dijkstra's algorithm, with one small change. Another local
variable `int[] shortestPath` has been introduced into the algorithm, which helps not
only to calculate the shortest distance, but also contains information about the
sequence of node numbers that make up the shortest path. This is what allows elements
of this path to be highlighted on the user’s screen.

```csharp
   public class DijkstraAlgorithm : IPathFindingAlgorithm
   {
       public int[] FindShortestPath(int[,] graph, int source, int verticesCount)
       {
           int[] distances = new int[verticesCount];
           bool[] shortestPathTreeSet = new bool[verticesCount];
           int[] shortestPath = new int[verticesCount];

           for (int i = 0; i < verticesCount; i++)
           {
               distances[i] = int.MaxValue;
               shortestPathTreeSet[i] = false;
           }

           distances[source] = 0;

           for (int count = 0; count < verticesCount - 1; count++)
           {
               int u = MinimumDistance(distances, shortestPathTreeSet, verticesCount);
               shortestPathTreeSet[u] = true;

               for (int v = 0; v < verticesCount; v++)
               {
                   if (!shortestPathTreeSet[v] && Convert.ToBoolean(graph[u, v]) &&
                       distances[u] != int.MaxValue && distances[u] + graph[u, v] < distances[v])
                   {
                       distances[v] = distances[u] + graph[u, v];
                       shortestPath[v] = u;
                   }
               }
           }

           return shortestPath;
       }

       private int MinimumDistance(int[] distance, bool[] shortestPathTreeSet, int verticesCount)
       {
           int min = int.MaxValue;
           int minIndex = 0;

           for (int v = 0; v < verticesCount; v++)
           {
               if (shortestPathTreeSet[v] == false && distance[v] <= min)
               {
                   min = distance[v];
                   minIndex = v;
               }
           }

           return minIndex;
       }
   }
```

## Usage

* Clone the repo as usual via cmd/terminal or in your favorite Git GUI software

* Open the project folder in Unity Hub using 2021.3.9f1 or a more recent of 2021.3
editor versions

* Open scene "Assets/_Project/Scenes/Main.unity"

## License

    The MIT License (MIT)

    Copyright (c) 2023 Alexander Shkurlatov

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.