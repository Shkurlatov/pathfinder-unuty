using System.Collections.Generic;
using UnityEngine;

namespace GraphPathfinder.Drawing
{
    public class EdgesHandler
    {
        private readonly Edge _edgePrefab;
        private readonly List<Edge> _edges;

        public EdgesHandler(Edge nodePrefab)
        {
            _edgePrefab = nodePrefab;
            _edges = new List<Edge>();
        }

        public Edge InstantiateEdge(Node startNode)
        {
            Edge edge = Object.Instantiate(_edgePrefab);
            edge.SetPosition(startNode.Position, 0);
            edge.StartNode = startNode;

            return edge;
        }

        public void CompleteEdge(Edge edge, Node endNode)
        {
            edge.Complete(endNode);

            _edges.Add(edge);
        }

        public void Clear()
        {
            foreach (Edge edge in _edges)
            {
                edge.Destroy();
            }

            _edges.Clear();
        }
    }
}