using TMPro;
using UnityEngine;

namespace GraphPathfinder.Drawing
{
    public class EdgeView : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private GameObject _distanceMarker;
        [SerializeField] private TextMeshProUGUI _markerText;

        [SerializeField] private Material _darkMaterial;
        [SerializeField] private Material _lightMaterial;

        public void SetPosition(int pointIndex, Vector2 position)
        {
            _lineRenderer.SetPosition(pointIndex, position);
        }

        public void ActivateDistanceMarker(string markerText, Vector2 markerPosition)
        {
            _markerText.text = markerText;
            _distanceMarker.transform.position = markerPosition;

            _distanceMarker.SetActive(true);
        }

        public void ToggleLight(bool turnOn)
        {
            if (turnOn)
            {
                _lineRenderer.material = _lightMaterial;

                return;
            }

            _lineRenderer.material = _darkMaterial;
        }
    }
}