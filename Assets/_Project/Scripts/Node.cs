using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GraphPathfinder
{
    public class Node : MonoBehaviour
    {
        [SerializeField] private Material _inactiveMaterial;
        [SerializeField] private Material _activeMaterial;

        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private SpriteRenderer _borderImage;
        [SerializeField] private SpriteRenderer _backLightImage;
        [SerializeField] private TextMeshProUGUI _numberText;

        [SerializeField] private Color _darkColor;
        [SerializeField] private Color _lightColor;

        public Vector2 Position { get; private set; }
        public List<Connection> Connections { get; private set; }
        public int Number { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsPicked { get; private set; }

        private void Awake()
        {
            Connections = new List<Connection>();
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
            _numberText.text = (number + 1).ToString();

            if (number == 0)
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

            foreach (Connection connection in Connections)
            {
                connection.ConnectedNode.Activate();
            }
        }

        public void ToggleBorder(bool isActive)
        {
            _borderImage.enabled = isActive;
        }

        public void Connect(Connection connection)
        {
            Connections.Add(connection);

            if (IsActive)
            {
                connection.ConnectedNode.Activate();
            }
        }

        public void Pick()
        {
            IsPicked = true;

            _backLightImage.color = _lightColor;
        }

        public void Skip()
        {
            IsPicked = false;

            _backLightImage.color = _darkColor;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}