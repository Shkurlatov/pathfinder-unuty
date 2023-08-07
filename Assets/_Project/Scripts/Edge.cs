using TMPro;
using UnityEngine;

namespace GraphPathfinder
{
    public class Edge : MonoBehaviour
    {
        [SerializeField] private LineRenderer _renderer;
        [SerializeField] private GameObject _distanceMarker;
        [SerializeField] private TextMeshProUGUI _markerText;

        [SerializeField] private Material _darkMaterial;
        [SerializeField] private Material _lightMaterial;

        public Node StartNode;
        public Node EndNode;

        public int RelativeDistance { get; private set; }

        public void SetPosition(Vector2 position, int point)
        {
            _renderer.SetPosition(point, position);
        }

        public void Complete(Node endNode)
        {
            EndNode = endNode;
            RelativeDistance = (int)(Vector2.Distance(StartNode.Position, EndNode.Position) * 10);

            SetPosition(EndNode.Position, 1);

            ActivateDistanceMarker();
        }

        private void ActivateDistanceMarker()
        {
            Vector2 markerPosition = StartNode.Position + ((EndNode.Position - StartNode.Position) / 2);

            _markerText.text = ((float)RelativeDistance / 10).ToString();
            _distanceMarker.transform.position = markerPosition;

            _distanceMarker.SetActive(true);
        }

        public void ToggleLight(bool turnOn)
        {
            if (turnOn)
            {
                _renderer.material = _lightMaterial;

                return;
            }

            _renderer.material = _darkMaterial;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}