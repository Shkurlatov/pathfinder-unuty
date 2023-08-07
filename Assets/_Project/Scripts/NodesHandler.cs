using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GraphPathfinder
{
    public class NodesHandler
    {
        private const float NODES_MIN_DISTANCE = 2.0f;

        private readonly Node _nodePrefab;
        private readonly UIRoot _uIRoot;

        public List<Node> Nodes { get; }

        public NodesHandler(Node nodePrefab, UIRoot uIRoot)
        {
            _nodePrefab = nodePrefab;
            _uIRoot = uIRoot;

            Nodes = new List<Node>();
        }

        public Node FindNearNode(Vector2 position)
        {
            Node nearNode = null;
            float minDistance = NODES_MIN_DISTANCE;

            foreach (Node node in Nodes)
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
            node.Initialize(Nodes.Count);

            _uIRoot.OnNodeCreated();
            Nodes.Add(node);
        }

        public void ConnectNodes(Node startNode, Node endNode, Edge edge)
        {
            startNode.Connect(new Connection(endNode, edge));
            endNode.Connect(new Connection(startNode, edge));

            if (Nodes.All(x => x.IsActive))
            {
                _uIRoot.OnNodesConnected();
            }
        }

        public void SkipNodes()
        {
            foreach (Node node in Nodes)
            {
                node.Skip();
            }
        }

        public void Clear()
        {
            foreach (Node node in Nodes)
            {
                node.Destroy();
            }

            Nodes.Clear();
        }
    }
}