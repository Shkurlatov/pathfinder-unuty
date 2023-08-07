using System.Collections.Generic;

namespace GraphPathfinder
{
    public class GraphBuilder
    {
        public static int[,] BuildGraph(List<Node> nodes)
        {
            int[,] graph = new int[nodes.Count, nodes.Count];

            for (int i = 0; i < nodes.Count; i++)
            {
                for (int j = 0; j < nodes.Count; j++)
                {
                    Connection connection = nodes[i].Connections.Find(x => x.ConnectedNode.Number == j);

                    if (connection != null)
                    {
                        graph[i, j] = connection.Edge.RelativeDistance;

                        continue;
                    }

                    graph[i, j] = 0;
                }
            }

            return graph;
        }
    }
}