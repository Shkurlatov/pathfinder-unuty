using UnityEngine;

namespace DijkstrasAlgorithm
{
    public class DrawManager : MonoBehaviour
    {
        private const float CLICK_HOLD_TIME = 0.5f;

        [SerializeField] private NodesHandler _nodesHandler;

        [SerializeField] private Node _nodePrefab;
        [SerializeField] private Edge _edgePrefab;

        private Camera _camera;

        private float _clickTimer;
        private Vector2 _clickPosition;
        private Node _currentNode;
        private Edge _currentEdge;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
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

            Node node = Instantiate(_nodePrefab, _clickPosition, Quaternion.identity);
            _nodesHandler.AddNode(node);

            return;
        }

        private void TryConnectNode(Node nearNode)
        {
            if (nearNode != null && nearNode != _currentNode)
            {
                if (!_currentNode.Connections.Contains(nearNode))
                {
                    _currentEdge.SetPosition(nearNode.Position, 1);
                    _nodesHandler.ConnectNodes(_currentNode, nearNode);

                    return;
                }
            }

            Destroy(_currentEdge.gameObject);

            return;
        }

        private void DrawEdge()
        {
            if (_currentEdge == null)
            {
                _currentEdge = Instantiate(_edgePrefab, _clickPosition, Quaternion.identity);
                _currentEdge.SetPosition(_currentNode.Position, 0);
            }

            _currentEdge.SetPosition(_camera.ScreenToWorldPoint(Input.mousePosition), 1);
        }
    }
}