using GraphPathfinder.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GraphPathfinder.Drawing
{
    public class NodesHandler
    {
        private const float NODES_MIN_DISTANCE = 2.0f;

        private readonly NodeObserver _nodePrefab;
        private readonly UIRoot _uIRoot;
        private readonly Transform _nodesSceneContainer;

        public List<Node> Nodes { get; }

        public NodesHandler(NodeObserver nodePrefab, UIRoot uIRoot)
        {
            _nodePrefab = nodePrefab;
            _uIRoot = uIRoot;
            _nodesSceneContainer = new GameObject("Nodes").transform;

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
            Node node = new Node(Nodes.Count, clickPosition);

            NodeObserver nodeObserver = Object.Instantiate(_nodePrefab, node.Position, Quaternion.identity, _nodesSceneContainer);
            nodeObserver.Initialize(node);

            _uIRoot.OnNodeCreated();
            Nodes.Add(node);
        }

        public void ConnectNodes(Node startNode, Node endNode, Edge edge)
        {
            startNode.Connect(new Connection(endNode, edge));
            endNode.Connect(new Connection(startNode, edge));

            if (Nodes.All(x => x.State == NodeState.Connected))
            {
                _uIRoot.OnNodesConnected();
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