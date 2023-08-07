using UnityEngine;

namespace GraphPathfinder
{
    public class Application : MonoBehaviour
    {
        [SerializeField] private UIRoot _uiRootPrefab;
        [SerializeField] private Node _nodePrefab;
        [SerializeField] private Edge _edgePrefab;

        private State _state;

        private IInput _input;
        private UIRoot _uiRoot;
        private NodesHandler _nodesHandler;
        private EdgesHandler _edgesHandler;
        private DrawManager _drawManager;
        private Pathfinder _pathfinder;

        private void Awake()
        {
            _state = State.Init;
        }

        private void Start()
        {
            _uiRoot = Instantiate(_uiRootPrefab);
            _uiRoot.Initialize(OnRestartPressed, OnBuildGraphPressed);

            _input = new Input();
            _nodesHandler = new NodesHandler(_nodePrefab, _uiRoot);
            _edgesHandler = new EdgesHandler(_edgePrefab);
            _drawManager = new DrawManager(_input, _nodesHandler, _edgesHandler, OnDrawInputComplete);
            _pathfinder = new Pathfinder(_nodesHandler, new DijkstraAlgorithm());

            _state = State.ConstructGraph;
        }

        private void Update()
        {
            if (_input.ButtonWasPressed())
            {
                ProcessInput();
            }

            if (_state == State.Draw)
            {
                _drawManager.Update();
            }
        }

        private void ProcessInput()
        {
            if (_input.IsPointerOverUI())
            {
                return;
            }

            if (_state == State.ConstructGraph)
            {
                _state = State.Draw;

                return;
            }

            if (_state == State.ChooseNodes)
            {
                _pathfinder.PickNodeOnPosition(_input.PointerPosition());
            }
        }

        private void OnDrawInputComplete()
        {
            _state = State.ConstructGraph;
        }

        private void OnRestartPressed()
        {
            _state = State.Restart;

            _edgesHandler.Clear();
            _nodesHandler.Clear();

            _state = State.ConstructGraph;
        }

        private void OnBuildGraphPressed()
        {
            _state = State.BuildGraph;

            _pathfinder.SetNewGraph(GraphBuilder.BuildGraph(_nodesHandler.Nodes));

            _state = State.ChooseNodes;
        }

        private enum State
        {
            Init = 0,
            ConstructGraph = 1,
            Draw = 2,
            BuildGraph = 3,
            ChooseNodes = 4,
            Restart = 5,
        }
    }
}