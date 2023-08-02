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

        private void Start()
        {
            Position = transform.position;
            Connections = new List<Node>();
        }

        public void Initialize(int number)
        {
            Number = number;
            IsActive = false;

            _meshRenderer.material = _inactiveMaterial;
            _borderImage.enabled = false;
            _numberText.text = number.ToString();
        }

        public void ToggleBorder(bool isActive)
        {
            _borderImage.enabled = isActive;
        }

        public void Activate()
        {
            IsActive = true;

            _meshRenderer.material = _activeMaterial;
        }

        public void Pick()
        {
            _backLightImage.color = Color.white;
        }
    }
}