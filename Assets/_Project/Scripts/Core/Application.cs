using GraphPathfinder.Drawing;
using GraphPathfinder.Pathfinding;
using GraphPathfinder.UI;
using UnityEngine;

namespace GraphPathfinder.Core
{
    public class Application : MonoBehaviour
    {
        [SerializeField] private UIRoot _uiRootPrefab;
        [SerializeField] private NodeObserver _nodePrefab;
        [SerializeField] private Edge _edgePrefab;

        [SerializeField] private Material _blueSkybox;
        [SerializeField] private Material _orangeSkybox;

        private State _state;

        private UIRoot _uiRoot;
        private IInput _input;
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
            InitializeUI();

            InitializeSystem();

            _state = State.ConstructGraph;
        }

        private void InitializeUI()
        {
            _uiRoot = Instantiate(_uiRootPrefab);
            _uiRoot.OnRestartButtonClick += Restart;
            _uiRoot.OnBuildGraphButtonClick += BuildGraph;
        }

        private void InitializeSystem()
        {
            _input = new Input();
            _nodesHandler = new NodesHandler(_nodePrefab, _uiRoot);
            _edgesHandler = new EdgesHandler(_edgePrefab);
            _drawManager = new DrawManager(_input, _nodesHandler, _edgesHandler, OnDrawInputComplete);
            _pathfinder = new Pathfinder(_nodesHandler, new DijkstraAlgorithm());
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
                _uiRoot.ProcessUIClick(_input.SelectedElementTag());

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

        private void Restart()
        {
            _state = State.Restart;

            _edgesHandler.Clear();
            _nodesHandler.Clear();

            RenderSettings.skybox = _blueSkybox;

            _state = State.ConstructGraph;
        }

        private void BuildGraph()
        {
            _state = State.BuildGraph;

            _pathfinder.SetNewGraph(GraphBuilder.BuildGraph(_nodesHandler.Nodes));

            RenderSettings.skybox = _orangeSkybox;

            _state = State.ChooseNodes;
        }

        private void OnDisable()
        {
            _uiRoot.OnRestartButtonClick -= Restart;
            _uiRoot.OnBuildGraphButtonClick -= BuildGraph;
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