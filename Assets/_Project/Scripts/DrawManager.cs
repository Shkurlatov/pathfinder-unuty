using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DijkstrasAlgorithm
{
    public class DrawManager
    {
        private readonly NodesHandler _nodesHandler;
        private readonly EdgesHandler _edgesHandler;
        private readonly Action _onDrawInputComplete;

        private readonly Camera _camera;

        private Vector2 _clickPosition;
        private Node _currentNode;
        private Edge _currentEdge;

        public DrawManager(NodesHandler nodesHandler, EdgesHandler edgesHandler, Action onDrawInputComplete)
        {
            _nodesHandler = nodesHandler;
            _edgesHandler = edgesHandler;
            _onDrawInputComplete = onDrawInputComplete;

            _camera = Camera.main;
        }

        public void Update()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                LaunchDrawInput();
            }

            if (_currentEdge != null)
            {
                RedrawEdge();
            }

            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                CompleteDrawInput();
            }
        }

        private void LaunchDrawInput()
        {
            _clickPosition = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
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
            _currentEdge.SetPosition(_camera.ScreenToWorldPoint(Mouse.current.position.ReadValue()), 1);
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
            Node nearNode = _nodesHandler.FindNearNode(_camera.ScreenToWorldPoint(Mouse.current.position.ReadValue()));

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