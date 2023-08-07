using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace GraphPathfinder
{
    public class Input : IInput
    {
        private readonly Camera _camera;

        public Input()
        {
            _camera = Camera.main;
        }

        public bool IsPointerOverUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }

        public bool ButtonWasPressed()
        {
            return Mouse.current.leftButton.wasPressedThisFrame;
        }

        public bool ButtonWasReleased()
        {
            return Mouse.current.leftButton.wasReleasedThisFrame;
        }

        public Vector2 PointerPosition()
        {
            return _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        }
    }
}