using TMPro;
using UnityEngine;

namespace GraphPathfinder.Drawing
{
    public class NodeView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _nodeImage;
        [SerializeField] private SpriteRenderer _backLightImage;
        [SerializeField] private TextMeshProUGUI _nodeText;

        [SerializeField] private Color _inactiveColor;
        [SerializeField] private Color _activeColor;
        [SerializeField] private Color _darkColor;
        [SerializeField] private Color _lightColor;

        private void Awake()
        {
            _nodeImage.color = _inactiveColor;
        }

        public void SetNodeText(string nodeText)
        {
            _nodeText.text = nodeText;
        }

        public void ShowConnectedNotPicked()
        {
            _nodeImage.color = _activeColor;
            _backLightImage.color = _darkColor;
        }

        public void Pick()
        {
            _backLightImage.color = _lightColor;
        }
    }
}