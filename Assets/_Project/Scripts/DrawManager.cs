using UnityEngine;

namespace DijkstrasAlgorithm
{
    public class DrawManager : MonoBehaviour
    {
        [SerializeField] private NodesHandler _nodesHandler;

        [SerializeField] private Node _nodePrefab;
        [SerializeField] private Edge _edgePrefab;

        private Camera _camera;

        private Vector2 _clickPosition;
        private Edge _currentEdge;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {

            if (Input.GetMouseButtonDown(0))
            {
                _clickPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                Vector2 releasePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

                if (releasePosition == _clickPosition)
                {
                    if (_nodesHandler.CheckDistance(_clickPosition))
                    {
                        Node node = Instantiate(_nodePrefab, _clickPosition, Quaternion.identity);
                        _nodesHandler.AddNode(node);
                    }
                }
            }

            //if (Input.GetMouseButtonDown(0))
            //{
            //    _currentEdge = Instantiate(_edgePrefab, mousePosition, Quaternion.identity);
            //    _currentEdge.SetPosition(mousePosition, 0);
            //}

            //if (Input.GetMouseButton(0))
            //{
            //    _currentEdge.SetPosition(mousePosition, 1);
            //}
        }
    }
}