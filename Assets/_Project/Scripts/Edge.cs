using TMPro;
using UnityEngine;

namespace DijkstrasAlgorithm
{
    public class Edge : MonoBehaviour
    {
        [SerializeField] private LineRenderer _renderer;
        [SerializeField] private GameObject _distanceMarker;
        [SerializeField] private TextMeshProUGUI _distanceText;

        [SerializeField] private Material _darkMaterial;
        [SerializeField] private Material _lightMaterial;

        public Node StartNode;
        public Node EndNode;

        public float Distance { get; private set; }

        public void SetPosition(Vector2 position, int point)
        {
            _renderer.SetPosition(point, position);
        }

        public void SetDistance()
        {
            Distance = Vector2.Distance(StartNode.Position, EndNode.Position);
            _distanceText.text = string.Format("{0:0.#}", Distance);
            _distanceMarker.transform.position = StartNode.Position + ((EndNode.Position - StartNode.Position) / 2);
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