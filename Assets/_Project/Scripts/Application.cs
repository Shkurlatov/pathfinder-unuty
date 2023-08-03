using UnityEngine;

namespace DijkstrasAlgorithm
{
    public class Application : MonoBehaviour
    {
        private State _state;

        private NodesHandler _nodesHandler;
        private EdgesHandler _edgesHandler;
        private DrawManager _drawManager;

        [SerializeField] private Node _nodePrefab;
        [SerializeField] private Edge _edgePrefab;

        private void Awake()
        {
            _state = State.Init;
        }

        private void Start()
        {
            _nodesHandler = new NodesHandler(_nodePrefab);
            _edgesHandler = new EdgesHandler(_edgePrefab);
            _drawManager = new DrawManager(_nodesHandler, _edgesHandler);

            _state = State.Draw;
        }

        private void Update()
        {
            if (_state == State.Draw)
            {
                _drawManager.Update();
            }
        }
    }

    public enum State
    {
        Init,
        Draw,
        FindPath
    }
}