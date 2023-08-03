using UnityEngine;

namespace DijkstrasAlgorithm
{
    public class Application : MonoBehaviour
    {
        [SerializeField] private UIRoot _uiRootPrefab;
        [SerializeField] private Node _nodePrefab;
        [SerializeField] private Edge _edgePrefab;

        private State _state;

        private NodesHandler _nodesHandler;
        private EdgesHandler _edgesHandler;
        private DrawManager _drawManager;
        private UIRoot _uiRoot;

        private Node[] _nodes;
        private float[,] _graph;

        private void Awake()
        {
            _state = State.Init;
        }

        private void Start()
        {
            _uiRoot = Instantiate(_uiRootPrefab);
            _uiRoot.Initialize(Restart, BuildGraph);

            _nodesHandler = new NodesHandler(_nodePrefab, _uiRoot);
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

        public void Restart()
        {
            _state = State.Restart;

            _edgesHandler.Clear();
            _nodesHandler.Clear();

            _state = State.Draw;
        }

        public void BuildGraph()
        {
            _state = State.Build;

            _nodes = _nodesHandler.GetNodes();
            _graph = new float[_nodes.Length, _nodes.Length];

            FillGraph();

            _state = State.FindPath;
        }

        private void FillGraph()
        {
            for (int i = 0; i < _nodes.Length; i++)
            {
                for (int j = 0; j < _nodes.Length; j++)
                {
                    if (_nodes[i].Connections.Contains(_nodes[j]))
                    {
                        _graph[i, j] = Vector2.Distance(_nodes[i].Position, _nodes[j].Position);

                        continue;
                    }

                    _graph[i, j] = 0.0f;
                }
            }
        }
    }

    public enum State
    {
        Init,
        Draw,
        Build,
        FindPath,
        Restart
    }
}