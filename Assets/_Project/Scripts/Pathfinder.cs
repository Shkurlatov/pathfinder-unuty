using UnityEngine;

namespace GraphPathfinder
{
    public class Pathfinder
    {
        private readonly NodesHandler _nodesHandler;
        private readonly EdgesHandler _edgesHandler;
        private readonly IPathFindingAlgorithm _pathFindingAlgorithm;
        private int[,] _graph;

        private Node _sourceNode;
        private Node _destinationNode;
        private bool _isComplete;

        public Pathfinder(NodesHandler nodesHandler, EdgesHandler edgesHandler, IPathFindingAlgorithm pathFindingAlgorithm)
        {
            _nodesHandler = nodesHandler;
            _edgesHandler = edgesHandler;
            _pathFindingAlgorithm = pathFindingAlgorithm;
        }

        public void SetNewGraph(int[,] graph)
        {
            _graph = graph;

            _sourceNode = null;
            _destinationNode = null;
            _isComplete = false;
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

        private void ClearPath()
        {
            _nodesHandler.SkipNodes();
            _edgesHandler.SkipEdges();

            _isComplete = false;
            _sourceNode = null;
            _destinationNode = null;
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

            int currentIndex = _destinationNode.Number;

            while (shortestPath[currentIndex] != _sourceNode.Number)
            {
                currentIndex = _nodesHandler.Nodes[shortestPath[currentIndex]].Number;
                _nodesHandler.Nodes[currentIndex].Pick();
            }

            _edgesHandler.PickEdges();
        }
    }
}