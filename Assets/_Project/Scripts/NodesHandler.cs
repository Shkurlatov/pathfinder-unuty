using System.Collections.Generic;
using UnityEngine;

namespace DijkstrasAlgorithm
{
    public class NodesHandler
    {
        private const float NODES_MIN_DISTANCE = 2.0f;

        private readonly Node _nodePrefab;
        private readonly List<Node> _nodes;

        public NodesHandler(Node nodePrefab)
        {
            _nodePrefab = nodePrefab;
            _nodes = new List<Node>();
        }

        public Node FindNearNode(Vector2 position)
        {
            Node nearNode = null;
            float minDistance = NODES_MIN_DISTANCE;

            foreach (Node node in _nodes)
            {
                float distance = Vector2.Distance(node.Position, position);

                if (distance < minDistance)
                {
                    nearNode = node;
                    minDistance = distance;
                }
            }

            return nearNode;
        }

        public void InstantiateNode(Vector2 clickPosition)
        {
            Node node = Object.Instantiate(_nodePrefab, clickPosition, Quaternion.identity);
            node.Initialize(_nodes.Count + 1);

            _nodes.Add(node);
        }

        public void ConnectNodes(Node firstNode, Node secondNode)
        {
            firstNode.Connect(secondNode);
            secondNode.Connect(firstNode);
        }

        public void Clear()
        {
            foreach (Node node in _nodes)
            {
                node.Destroy();
            }

            _nodes.Clear();
        }
    }
}