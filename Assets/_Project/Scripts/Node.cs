using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GraphPathfinder
{
    public class Node : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _nodeImage;
        [SerializeField] private SpriteRenderer _backLightImage;
        [SerializeField] private TextMeshProUGUI _numberText;

        [SerializeField] private Color _activeColor;
        [SerializeField] private Color _inactiveColor;
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

            _nodeImage.color = _inactiveColor;
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
            _nodeImage.color = _activeColor;

            foreach (Connection connection in Connections)
            {
                connection.ConnectedNode.Activate();
            }
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