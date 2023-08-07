using System;
using UnityEngine;
using UnityEngine.UI;

public class UIRoot : MonoBehaviour
{
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private GameObject _buildGraphButton;

    [SerializeField] private Color _greenColor;
    [SerializeField] private Color _redColor;

    public event Action OnRestartButtonClick;
    public event Action OnBuildGraphButtonClick;

    private void Start()
    {
        HideButtons();
    }

    public void ProcessUIClick(string elementTag)
    {
        if (_restartButton.CompareTag(elementTag))
        {
            HideButtons();

            OnRestartButtonClick?.Invoke();
        }

        if (_buildGraphButton.CompareTag(elementTag))
        {
            _buildGraphButton.SetActive(false);

            OnBuildGraphButtonClick?.Invoke();
        }
    }

    public void OnNodeCreated()
    {
        _restartButton.SetActive(true);
        ToggleGraphBuildButton(false);
    }

    public void OnNodesConnected()
    {
        _buildGraphButton.SetActive(true);
        ToggleGraphBuildButton(true);
    }

    private void HideButtons()
    {
        _restartButton.SetActive(false);
        _buildGraphButton.SetActive(false);
    }

    private void ToggleGraphBuildButton(bool turnOn)
    {
        _buildGraphButton.GetComponent<Button>().enabled = turnOn;
        _buildGraphButton.GetComponent<Image>().color = turnOn ? _greenColor : _redColor;
    }
}
