using System;
using UnityEngine;

namespace DijkstrasAlgorithm
{
    public class DrawManager
    {
        private readonly IInput _input;
        private readonly NodesHandler _nodesHandler;
        private readonly EdgesHandler _edgesHandler;
        private readonly Action _onDrawInputComplete;

        private Vector2 _clickPosition;
        private Node _currentNode;
        private Edge _currentEdge;

        public DrawManager(IInput input, NodesHandler nodesHandler, EdgesHandler edgesHandler, Action onDrawInputComplete)
        {
            _input = input;
            _nodesHandler = nodesHandler;
            _edgesHandler = edgesHandler;
            _onDrawInputComplete = onDrawInputComplete;
        }

        public void Update()
        {
            if (_input.ButtonWasPressed())
            {
                LaunchDrawInput();
            }

            if (_currentEdge != null)
            {
                RedrawEdge();
            }

            if (_input.ButtonWasReleased())
            {
                CompleteDrawInput();
            }
        }

        private void LaunchDrawInput()
        {
            _clickPosition = _input.PointerPosition();
            _currentNode = _nodesHandler.FindNearNode(_clickPosition);
            _currentEdge = null;

            if (_currentNode == null)
            {
                _nodesHandler.InstantiateNode(_clickPosition);

                return;
            }

            _currentEdge = _edgesHandler.InstantiateEdge(_currentNode);
        }

        private void RedrawEdge()
        {
            _currentEdge.SetPosition(_input.PointerPosition(), 1);
        }

        private void CompleteDrawInput()
        {
            if (_currentEdge != null)
            {
                TryConnectNode();
            }

            _onDrawInputComplete.Invoke();
        }

        private void TryConnectNode()
        {
            Node nearNode = _nodesHandler.FindNearNode(_input.PointerPosition());

            if (nearNode != null && nearNode != _currentNode)
            {
                if (!_currentNode.Connections.Contains(nearNode))
                {
                    _edgesHandler.CompleteEdge(_currentEdge, nearNode);
                    _nodesHandler.ConnectNodes(_currentNode, nearNode);

                    return;
                }
            }

            _currentEdge.Destroy();

            return;
        }
    }
}