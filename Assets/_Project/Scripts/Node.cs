using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DijkstrasAlgorithm
{
    public class Node : MonoBehaviour
    {
        [SerializeField] private Material _inactiveMaterial;
        [SerializeField] private Material _activeMaterial;

        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private SpriteRenderer _borderImage;
        [SerializeField] private SpriteRenderer _backLightImage;
        [SerializeField] private TextMeshProUGUI _numberText;

        public Vector2 Position { get; private set; }
        public List<Node> Connections { get; private set; }
        public int Number { get; private set; }
        public bool IsActive { get; private set; }

        private void Awake()
        {
            Connections = new List<Node>();
        }

        private void Start()
        {
            Position = transform.position;
        }

        public void Initialize(int number)
        {
            Number = number;
            IsActive = false;

            _meshRenderer.material = _inactiveMaterial;
            _borderImage.enabled = false;
            _numberText.text = number.ToString();

            if (number == 1)
            {
                Activate();
            }
        }

        public void Activate()
        {
            if (IsActive)
            {
                return;
            }

            IsActive = true;
            _meshRenderer.material = _activeMaterial;

            foreach (Node connection in Connections)
            {
                connection.Activate();
            }
        }

        public void ToggleBorder(bool isActive)
        {
            _borderImage.enabled = isActive;
        }

        public void Connect(Node connection)
        {
            Connections.Add(connection);

            if (IsActive)
            {
                connection.Activate();
            }
        }

        public void Pick()
        {
            _backLightImage.color = Color.white;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}