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
            bool canBeInstantiated = true;

            foreach (Node node in _nodes)
            {
                if (Vector2.Distance(node.Position, position) < NODES_MIN_DISTANCE)
                {
                    canBeInstantiated = false;
                    node.ToggleBorder(true);

                    continue;
                }

                node.ToggleBorder(false);
            }

            return canBeInstantiated;
        }

        public void AddNode(Node node)
        {
            _nodes.Add(node);
            node.Initialize(_nodes.Count);
        }

        public void Clear()
        {
            _nodes.Clear();
        }
    }
}