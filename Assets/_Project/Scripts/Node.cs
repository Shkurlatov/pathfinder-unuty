using UnityEngine;

namespace DijkstrasAlgorithm
{
    public class Node : MonoBehaviour
    {
        public Vector2 Position;

        private void Start()
        {
            Position = transform.position;
        }
    }
}