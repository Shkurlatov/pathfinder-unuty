using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DijkstrasAlgorithm
{
    public class NodesHandler
    {
        private const float NODES_MIN_DISTANCE = 2.0f;

        private readonly Node _nodePrefab;
        private readonly UIRoot _uIRoot;
        private readonly List<Node> _nodes;

        public NodesHandler(Node nodePrefab, UIRoot uIRoot)
        {
            _nodePrefab = nodePrefab;
            _uIRoot = uIRoot;
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

            _uIRoot.OnNodeCreated();
            _nodes.Add(node);
        }

        public void ConnectNodes(Node firstNode, Node secondNode)
        {
            firstNode.Connect(secondNode);
            secondNode.Connect(firstNode);

            if (_nodes.All(x => x.IsActive))
            {
                _uIRoot.OnNodesConnected();
            }
        }

        public Node[] GetNodes()
        {
            return _nodes.ToArray();
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