using UnityEngine;

namespace GraphPathfinder.Core
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