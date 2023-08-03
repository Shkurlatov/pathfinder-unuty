using System.Collections.Generic;
using UnityEngine;

namespace DijkstrasAlgorithm
{
    public class NodesHandler : MonoBehaviour
    {
        private const float NODES_MIN_DISTANCE = 2.0f;

        private List<Node> _nodes;

        private void Awake()
        {
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

        public void AddNode(Node node)
        {
            _nodes.Add(node);
            node.Initialize(_nodes.Count);
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
                Destroy(node.gameObject);
            }

            _nodes.Clear();
        }
    }
}