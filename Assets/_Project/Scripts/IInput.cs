using UnityEngine;

namespace GraphPathfinder
{
    public interface IInput
    {
        bool ButtonWasPressed();
        bool ButtonWasReleased();
        bool IsPointerOverUI();
        Vector2 PointerPosition();
    }
}