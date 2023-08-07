using UnityEngine;

namespace GraphPathfinder
{
    public interface IInput
    {
        bool IsPointerOverUI();
        string SelectedElementTag();
        bool ButtonWasPressed();
        bool ButtonWasReleased();
        Vector2 PointerPosition();
    }
}