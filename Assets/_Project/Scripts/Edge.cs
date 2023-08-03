using UnityEngine;

namespace DijkstrasAlgorithm
{
    public class Edge : MonoBehaviour
    {
        [SerializeField] private LineRenderer _renderer;

        public Node StartNode;
        public Node EndNode;

        public void SetPosition(Vector2 position, int point)
        {
            _renderer.SetPosition(point, position);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}