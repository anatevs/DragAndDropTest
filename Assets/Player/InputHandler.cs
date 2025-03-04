using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputHandler : MonoBehaviour
    {
        public event Action<Vector2> OnDragging;

        public event Action OnDropped;

        private Camera _camera;

        private PlayerInput _input;

        private InputAction _dragAction;

        private const string DRAG_ACTION = "Drag";

        private void Awake()
        {
            _camera = Camera.main;

            _input = GetComponent<PlayerInput>();

            _dragAction = _input.actions.FindAction(DRAG_ACTION);
        }

        private void OnEnable()
        {
            _input.onActionTriggered += MakeOnDrop;
        }

        private void OnDisable()
        {
            _input.onActionTriggered -= MakeOnDrop;
        }

        private void Update()
        {
            if (_dragAction.phase.IsInProgress())
            {
                MakeOnGrab();
            }
        }

        private void MakeOnGrab()
        {
            var clickPositionScreen = _dragAction.ReadValue<Vector2>();

            var clickPos = _camera.ScreenToWorldPoint(clickPositionScreen);

            OnDragging?.Invoke(clickPos);
        }

        private void MakeOnDrop(InputAction.CallbackContext context)
        {
            if (context.action == _dragAction)
            {
                if (context.canceled)
                {
                    OnDropped?.Invoke();
                }
            }
        }
    }
}