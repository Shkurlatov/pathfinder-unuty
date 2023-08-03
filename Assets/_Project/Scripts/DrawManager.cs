using UnityEngine;
using UnityEngine.EventSystems;

namespace DijkstrasAlgorithm
{
    public class DrawManager
    {
        private const float CLICK_HOLD_TIME = 0.2f;

        private readonly NodesHandler _nodesHandler;
        private readonly EdgesHandler _edgesHandler;

        private readonly Camera _camera;

        private float _clickTimer;
        private Vector2 _clickPosition;
        private Node _currentNode;
        private Edge _currentEdge;

        public DrawManager(NodesHandler nodesHandler, EdgesHandler edgesHandler)
        {
            _nodesHandler = nodesHandler;
            _edgesHandler = edgesHandler;

            _camera = Camera.main;
        }

        public void Update()
        {
            if (AboveButton())
            {
                Reset();

                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                OnClickDown();
            }

            if (Input.GetMouseButtonUp(0))
            {
                Vector2 releasePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
                Node nearNode = _nodesHandler.FindNearNode(releasePosition);

                if (_clickTimer < CLICK_HOLD_TIME)
                {
                    TryInstantiateNewNode(nearNode);

                    return;
                }

                if (_currentEdge != null)
                {
                    TryConnectNode(nearNode);

                    return;
                }
            }

            if (Input.GetMouseButton(0))
            {
                _clickTimer += Time.deltaTime;

                if (_currentNode == null || _clickTimer < CLICK_HOLD_TIME)
                {
                    return;
                }

                DrawEdge();
            }
        }

        private bool AboveButton()
        {
            GameObject current = EventSystem.current.currentSelectedGameObject;

            if (current != null && current.CompareTag("Button"))
            {
                return true;
            }

            return false;
        }

        private void OnClickDown()
        {
            _clickTimer = 0.0f;

            if (_currentNode != null)
            {
                _currentNode.ToggleBorder(false);
                _currentNode = null;
            }

            _currentEdge = null;

            _clickPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            _currentNode = _nodesHandler.FindNearNode(_clickPosition);
        }

        private void TryInstantiateNewNode(Node nearNode)
        {
            if (nearNode != null)
            {
                nearNode.ToggleBorder(true);
                _currentNode = nearNode;

                return;
            }

            _nodesHandler.InstantiateNode(_clickPosition);

            return;
        }

        private void TryConnectNode(Node nearNode)
        {
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

        private void DrawEdge()
        {
            if (_currentEdge == null)
            {
                _currentEdge = _edgesHandler.InstantiateEdge(_currentNode);
            }

            _currentEdge.SetPosition(_camera.ScreenToWorldPoint(Input.mousePosition), 1);
        }

        private void Reset()
        {
            _currentNode = null;
            _currentEdge = null;
            _clickPosition = Vector2.zero;
            _clickTimer = 0;
        }
    }
}