using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Input
{
    public class InputReader : IDisposable
    {
        public event Action OnClick;

        private InputSystem_Actions _inputSystem;
        private InputAction _fireAction;
        private InputAction _positionAction;
        private bool _isFire;

        public InputReader()
        {
            _inputSystem = new InputSystem_Actions();
            _inputSystem.Player.Fire.performed += ButtonClickExist;
        }

        public void Dispose()
        {
            _inputSystem.Player.Fire.performed -= ButtonClickExist;
        }

        public void EnableInputs(bool isOn)
        {
            if (isOn)
            {
                _inputSystem.Enable();
            }
            else
            {
                _inputSystem.Disable();
            }
        }

        public Vector3 Position() => _inputSystem.Player.Select.ReadValue<Vector2>();

        private void ButtonClickExist(InputAction.CallbackContext context)
        {
            OnClick?.Invoke();
        }
    }
}
