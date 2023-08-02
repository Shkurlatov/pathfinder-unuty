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

        public bool CheckDistance(Vector2 position)
        {
            foreach (Node node in _nodes)
            {
                if (Vector2.Distance(node.Position, position) < NODES_MIN_DISTANCE)
                {
                    return false;
                }
            }

            return true;
        }

        public void AddNode(Node node)
        {
            _nodes.Add(node);
        }

        public void Clear()
        {
            _nodes.Clear();
        }
    }
}