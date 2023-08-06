using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace DijkstrasAlgorithm
{
    public class Application : MonoBehaviour
    {
        [SerializeField] private UIRoot _uiRootPrefab;
        [SerializeField] private Node _nodePrefab;
        [SerializeField] private Edge _edgePrefab;

        private State _state;

        private UIRoot _uiRoot;
        private NodesHandler _nodesHandler;
        private EdgesHandler _edgesHandler;
        private DrawManager _drawManager;
        private Pathfinder _pathfinder;

        private Node[] _nodes;
        private float[,] _graph;

        private void Awake()
        {
            _state = State.Init;
        }

        private void Start()
        {
            _uiRoot = Instantiate(_uiRootPrefab);
            _uiRoot.Initialize(OnRestartPressed, OnBuildGraphPressed);

            _nodesHandler = new NodesHandler(_nodePrefab, _uiRoot);
            _edgesHandler = new EdgesHandler(_edgePrefab);
            _drawManager = new DrawManager(_nodesHandler, _edgesHandler, OnDrawInputComplete);
            _pathfinder = new Pathfinder(_nodesHandler, _edgesHandler);

            _state = State.ConstructGraph;
        }

        private void Update()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                ProcessInput();
            }

            if (_state == State.Draw)
            {
                _drawManager.Update();
            }

            //if (_state == State.FindPath)
            //{
            //    _pathfinder.Update();
            //}

            //if (_state == State.Draw)
            //{
            //    if (Mouse.current.leftButton.wasPressedThisFrame)
            //    {
            //        if (EventSystem.current.IsPointerOverGameObject())
            //        {
            //            Debug.Log("UI");

            //            return;
            //        }

            //        Debug.Log("Free space");
            //    }
            //    //_drawManager.Update();
            //}

            //if (_state == State.FindPath)
            //{
            //    //_pathfinder.Update();
            //}
        }

        private void ProcessInput()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("UI");

                return;
            }

            if (_state == State.ConstructGraph)
            {
                _state = State.Draw;
            }
        }

        private void OnDrawInputComplete()
        {
            _state = State.ConstructGraph;
        }

        private void OnRestartPressed()
        {
            //_state = State.Restart;

            //_edgesHandler.Clear();
            //_nodesHandler.Clear();

            //_state = State.Draw;
        }

        private void OnBuildGraphPressed()
        {
            //_state = State.BuildGraph;

            //FillGraph();

            //_pathfinder.SetGraph(_graph);

            //_state = State.FindPath;
        }

        private void FillGraph()
        {
            _nodes = _nodesHandler.GetNodes();
            _graph = new float[_nodes.Length, _nodes.Length];

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

        private enum State
        {
            Init = 0,
            ConstructGraph = 1,
            Draw = 2,
            BuildGraph = 3,
            ChoosePoints = 4,
            FindPath = 5,
            Restart = 6,
        }
    }
}