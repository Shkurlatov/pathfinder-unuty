using System;
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

            FindPath();
        }

        //public void Update()
        //{
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        if (AboveButton())
        //        {
        //            return;
        //        }

        //        if (_isComplete)
        //        {
        //            _nodesHandler.SkipNodes();
        //            _edgesHandler.SkipEdges();

        //            _isComplete = false;
        //            _sourceNode = null;
        //            _destinationNode = null;
        //        }

        //        Vector2 releasePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        //        Node nearNode = _nodesHandler.FindNearNode(releasePosition);

        //        if (nearNode != null)
        //        {
        //            if (_sourceNode == null)
        //            {
        //                _sourceNode = nearNode;
        //                _sourceNode.Pick();

        //                return;
        //            }

        //            if (_sourceNode != nearNode)
        //            {
        //                _destinationNode = nearNode;
        //                _destinationNode.Pick();

        //                _isComplete = true;

        //                FindPath();
        //            }
        //        }
        //    }
        //}

        //private bool AboveButton()
        //{
        //    GameObject current = EventSystem.current.currentSelectedGameObject;

        //    if (current != null && current.CompareTag(BUTTON_TAG))
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        public void FindPath()
        {
            //Dijkstra(_graph, _sourceNode.Number - 1, _graph.GetLength(0));
        }

        private void Dijkstra(float[,] graph, int source, int verticesCount)
        {
            float[] distance = new float[verticesCount];
            bool[] shortestPathTreeSet = new bool[verticesCount];
            int[] previousShortestIndex = new int[verticesCount];

            for (int i = 0; i < verticesCount; ++i)
            {
                distance[i] = float.MaxValue;
                shortestPathTreeSet[i] = false;
            }

            distance[source] = 0.0f;

            for (int count = 0; count < verticesCount - 1; ++count)
            {
                int u = MinimumDistance(distance, shortestPathTreeSet, verticesCount);
                shortestPathTreeSet[u] = true;

                for (int v = 0; v < verticesCount; ++v)
                {
                    if (!shortestPathTreeSet[v] && Convert.ToBoolean(graph[u, v]) && distance[u] != float.MaxValue && distance[u] + graph[u, v] < distance[v])
                    {
                        distance[v] = distance[u] + graph[u, v];
                        previousShortestIndex[v] = u;
                    }
                }
            }

            ShowShortestPath(previousShortestIndex);
            Print(distance, verticesCount);
        }

        private int MinimumDistance(float[] distance, bool[] shortestPathTreeSet, int verticesCount)
        {
            float min = float.MaxValue;
            int minIndex = 0;

            for (int v = 0; v < verticesCount; ++v)
            {
                if (shortestPathTreeSet[v] == false && distance[v] <= min)
                {
                    min = distance[v];
                    minIndex = v;
                }
            }

            return minIndex;
        }

        private void ShowShortestPath(int[] previousShortestIndex)
        {
            int currentIndex = _destinationNode.Number - 1;

            while (previousShortestIndex[currentIndex] != _sourceNode.Number - 1)
            {
                currentIndex = _nodesHandler.Nodes[previousShortestIndex[currentIndex]].Number - 1;
                _nodesHandler.Nodes[currentIndex].Pick();
            }

            _edgesHandler.PickEdges();
        }

        private void Print(float[] distance, int verticesCount)
        {
            Debug.Log("Vertex    Distance from source");

            for (int i = 0; i < verticesCount; ++i)
            {
                Debug.Log($"{i}\t  {distance[i]}");
            }
        }
    }
}