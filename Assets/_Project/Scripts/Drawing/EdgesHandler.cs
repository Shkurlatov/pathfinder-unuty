using System.Collections.Generic;
using UnityEngine;

namespace GraphPathfinder.Drawing
{
    public class EdgesHandler
    {
        private readonly EdgeObserver _edgePrefab;
        private readonly List<Edge> _edges;
        private readonly Transform _edgesSceneContainer;

        public EdgesHandler(EdgeObserver edgePrefab)
        {
            _edgePrefab = edgePrefab;
            _edges = new List<Edge>();
            _edgesSceneContainer = new GameObject("Edges").transform;
        }

        public Edge InstantiateEdge(Node startNode)
        {
            Edge edge = new Edge(startNode);

            EdgeObserver edgeObserver = Object.Instantiate(_edgePrefab, _edgesSceneContainer);
            edgeObserver.Initialize(edge);

            return edge;
        }

        public void CompleteEdge(Edge edge, Node endNode)
        {
            edge.SetEndPosition(endNode.Position);
            edge.Complete();

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