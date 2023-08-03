using System;
using UnityEngine;
using UnityEngine.UI;

public class UIRoot : MonoBehaviour
{
    [SerializeField] private GameObject _resetButton;
    [SerializeField] private GameObject _buildGraphButton;

    [SerializeField] private Color _greenColor;
    [SerializeField] private Color _redColor;

    private Action _reset;
    private Action _buildGraph;

    private void Awake()
    {
        _resetButton.GetComponent<Button>().onClick.AddListener(Restart);
        _buildGraphButton.GetComponent<Button>().onClick.AddListener(BuildGraph);

        HideButtons();
    }

    public void Initialize(Action reset, Action buildGraph)
    {
        _reset = reset;
        _buildGraph = buildGraph;
    }

    public void OnNodeCreated()
    {
        _resetButton.SetActive(true);
        ToggleGraphBuildButton(false);
    }

    public void OnNodesConnected()
    {
        _buildGraphButton.SetActive(true);
        ToggleGraphBuildButton(true);
    }

    private void HideButtons()
    {
        _resetButton.SetActive(false);
        _buildGraphButton.SetActive(false);
    }

    private void ToggleGraphBuildButton(bool turnOn)
    {
        _buildGraphButton.GetComponent<Button>().enabled = turnOn;
        _buildGraphButton.GetComponent<Image>().color = turnOn ? _greenColor : _redColor;
    }

    private void Restart()
    {
        HideButtons();

        _reset?.Invoke();
    }

    private void BuildGraph()
    {
        _buildGraphButton.SetActive(false);

        _buildGraph?.Invoke();
    }
}
