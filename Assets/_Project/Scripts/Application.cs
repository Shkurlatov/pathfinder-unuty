using UnityEngine;

namespace DijkstrasAlgorithm
{
    public class Application : MonoBehaviour
    {
        private NodesHandler _nodesHandler;
        private EdgesHandler _edgesHandler;
        private DrawManager _drawManager;

        [SerializeField] private Node _nodePrefab;
        [SerializeField] private Edge _edgePrefab;

        private void Start()
        {
            _nodesHandler = new NodesHandler(_nodePrefab);
            _edgesHandler = new EdgesHandler(_edgePrefab);
            _drawManager = new DrawManager(_nodesHandler, _edgesHandler);
        }

        private void Update()
        {
            _drawManager.Update();
        }
    }
}