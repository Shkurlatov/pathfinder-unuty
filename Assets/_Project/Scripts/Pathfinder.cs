using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DijkstrasAlgorithm
{
    public class Pathfinder
    {
        private const string BUTTON_TAG = "Button";

        private readonly NodesHandler _nodesHandler;
        private readonly EdgesHandler _edgesHandler;

        private readonly Camera _camera;

        private float[,] _graph;

        private Node[] _nodes;
        private Node _sourceNode;
        private Node _destinationNode;
        private bool _isComplete;

        public Pathfinder(NodesHandler nodesHandler, EdgesHandler edgesHandler)
        {
            _nodesHandler = nodesHandler;
            _edgesHandler = edgesHandler;

            _camera = Camera.main;
        }

        public void SetGraph(float[,] graph)
        {
            _graph = graph;

            _nodes = _nodesHandler.GetNodes();
            _sourceNode = null;
            _destinationNode = null;
            _isComplete = false;
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (AboveButton())
                {
                    return;
                }

                if (_isComplete)
                {
                    _nodesHandler.SkipNodes();
                    _edgesHandler.SkipEdges();

                    _isComplete = false;
                    _sourceNode = null;
                    _destinationNode = null;
                }

                Vector2 releasePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
                Node nearNode = _nodesHandler.FindNearNode(releasePosition);

                if (nearNode != null)
                {
                    if (_sourceNode == null)
                    {
                        _sourceNode = nearNode;
                        _sourceNode.Pick();

                        return;
                    }

                    if (_sourceNode != nearNode)
                    {
                        _destinationNode = nearNode;
                        _destinationNode.Pick();

                        _isComplete = true;

                        FindPath();
                    }
                }
            }
        }

        private bool AboveButton()
        {
            GameObject current = EventSystem.current.currentSelectedGameObject;

            if (current != null && current.CompareTag(BUTTON_TAG))
            {
                return true;
            }

            return false;
        }

        public void FindPath()
        {
            Dijkstra(_graph, _sourceNode.Number - 1, _graph.GetLength(0));
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
                currentIndex = _nodes[previousShortestIndex[currentIndex]].Number - 1;
                _nodes[currentIndex].Pick();
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