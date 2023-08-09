using UnityEngine;

namespace GraphPathfinder.Drawing
{
    public class EdgeObserver : MonoBehaviour
    {
        [SerializeField] private EdgeView _view;

        private Edge _model;

        public void Initialize(Edge edge)
        {
            _model = edge;
            _model.OnEdgeChanged += OnEdgeChanged;

            _view.SetPosition(0, _model.StartPosition);
        }

        private void OnDisable()
        {
            _model.OnEdgeChanged -= OnEdgeChanged;
        }

        private void ActivateDistanceMarker()
        {
            Vector2 markerPosition = _model.StartPosition + ((_model.EndPosition - _model.StartPosition) / 2);
            string markerText = ((float)_model.RelativeDistance / 10).ToString();

            _view.ActivateDistanceMarker(markerText, markerPosition);
        }

        private void OnEdgeChanged(EdgeState state)
        {
            switch (state)
            {
                case EdgeState.NotComplete:
                    _view.SetPosition(1, _model.EndPosition);
                    break;
                case EdgeState.Complete:
                    ActivateDistanceMarker();
                    break;
                case EdgeState.Highlighted:
                    _view.ToggleLight(true);
                    break;
                case EdgeState.Dimmed:
                    _view.ToggleLight(false);
                    break;
                case EdgeState.Destroyed:
                    Destroy(gameObject);
                    break;
            }
        }
    }
}