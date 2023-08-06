using UnityEngine;

namespace DijkstrasAlgorithm
{
    public interface IInput
    {
        bool ButtonWasPressed();
        bool ButtonWasReleased();
        bool IsPointerOverUI();
        Vector2 PointerPosition();
    }
}