using GraphPathfinder.Drawing;
using System.Collections.Generic;
using UnityEngine;

namespace GraphPathfinder.Pathfinding
{
    public class Pathfinder
    {
        private readonly NodesHandler _nodesHandler;
        private readonly IPathFindingAlgorithm _pathFindingAlgorithm;
        private readonly List<Connection> _pathConnections;

        private int[,] _graph;

        private Node _sourceNode;
        private Node _destinationNode;
        private bool _isComplete;

        public Pathfinder(NodesHandler nodesHandler, IPathFindingAlgorithm pathFindingAlgorithm)
        {
            _nodesHandler = nodesHandler;
            _pathFindingAlgorithm = pathFindingAlgorithm;
            _pathConnections = new List<Connection>();
        }

        public void SetNewGraph(int[,] graph)
        {
            _graph = graph;

            Reset();
        }

        private void Reset()
        {
            _pathConnections.Clear();

            _isComplete = false;
            _sourceNode = null;
            _destinationNode = null;
        }

        public void PickNodeOnPosition(Vector2 pointerPosition)
        {
            if (_isComplete)
            {
                ClearPath();
            }

            Node nearNode = _nodesHandler.FindNearNode(pointerPosition);

            if (nearNode == null)
            {
                return;
            }

            if (_sourceNode == null)
            {
                SetSourceNode(nearNode);

                return;
            }

            if (_sourceNode != nearNode)
            {
                CompletePath(nearNode);
            }
        }

        private void SetSourceNode(Node nearNode)
        {
            _sourceNode = nearNode;
            _sourceNode.Pick();
        }

        private void CompletePath(Node nearNode)
        {
            _destinationNode = nearNode;
            _destinationNode.Pick();

            _isComplete = true;

            ShowShortestPath();
        }

        private void ShowShortestPath()
        {
            int[] shortestPath = _pathFindingAlgorithm.FindShortestPath(_graph, _sourceNode.Number, _graph.GetLength(0));

            Node currentNode = _destinationNode;

            while (shortestPath[currentNode.Number] != _sourceNode.Number)
            {
                int previousNumber = shortestPath[currentNode.Number];

                Connection connection = currentNode.Connections.Find(x => x.ConnectedNode.Number == previousNumber);
                connection.ConnectedNode.Pick();
                connection.Edge.LightUp();
                _pathConnections.Add(connection);

                currentNode = connection.ConnectedNode;
            }

            Connection lastConnection = currentNode.Connections.Find(x => x.ConnectedNode == _sourceNode);
            lastConnection.Edge.LightUp();
            _pathConnections.Add(lastConnection);
        }

        private void ClearPath()
        {
            foreach (Connection connection in _pathConnections)
            {
                connection.ConnectedNode.Skip();
                connection.Edge.Dim();
            }

            _destinationNode.Skip();

            Reset();
        }
    }
}