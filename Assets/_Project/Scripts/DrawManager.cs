using UnityEngine;

public class DrawManager : MonoBehaviour
{
    [SerializeField] private Edge _edgePrefab;

    private Camera _camera;

    private Edge _currentEdge;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            _currentEdge = Instantiate(_edgePrefab, mousePosition, Quaternion.identity);
            _currentEdge.SetPosition(mousePosition, 0);
        }

        if (Input.GetMouseButton(0))
        {
            _currentEdge.SetPosition(mousePosition, 1);
        }
    }
}
