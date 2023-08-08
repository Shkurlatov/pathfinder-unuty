using UnityEngine;

namespace GraphPathfinder.Drawing
{
    public class NodeObserver : MonoBehaviour
    {
        [SerializeField] private NodeView _view;

        private Node _model;

        public void Initialize(Node node)
        {
            _model = node;
            _model.OnStateChanged += OnStateChanged;

            _view.SetNodeText((_model.Number + 1).ToString());

            if (_model.Number == 0)
            {
                _view.ShowConnectedNotPicked();
            }
        }

        private void OnDisable()
        {
            _model.OnStateChanged -= OnStateChanged;
        }

        private void OnStateChanged(NodeState state)
        {
            switch (state)
            {
                case NodeState.Connected:
                    _view.ShowConnectedNotPicked();
                    break;
                case NodeState.Picked:
                    _view.Pick();
                    break;
                case NodeState.Destroyed:
                    Destroy(gameObject);
                    break;
            }
        }
    }
}